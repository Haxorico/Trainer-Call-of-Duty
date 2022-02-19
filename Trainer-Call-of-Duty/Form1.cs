using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Microsoft.DirectX;
using Trainer_Call_of_Duty.Helpers;
using Trainer_Call_of_Duty.GameData;


namespace Trainer_Call_of_Duty
{
    public partial class Form1 : Form
    {
        

        private List<Entity> Enemies = new List<Entity>();
        private bool isProcessRunning = false;
        private bool lastProcessState = false;
        private Overlay Overlay = new Overlay();
        private const uint RECTS_PER_ENTITY = 1;
        private delegate void SafeCallDelegate(string text);

        private void WriteDebugText(string text)
        {
            if (txtDebug.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteDebugText);
                txtDebug.Invoke(d, new object[] {text});
            }
            else
            {
                txtDebug.Text = text;
            }
        }

        private void clearESPForUnit(int unitID)
        {
            for (int i = 0; i < RECTS_PER_ENTITY; i++)
            {
                Overlay.UpdateRect(unitID * (int)RECTS_PER_ENTITY + i, -1, -1, 0, 0, Color.White);
            }
        }

        private void updateESPBox(int index)
        {
            try
            {

                //float distanceToPlayer = Vector3.Distance(Player.Origin, Enemies[index].Position03);

                //#TODO remove the WH after the unit is dead...
                Color boxColor = Color.White;
                Vector2 screenSize = new Vector2(Overlay.Size.Width, Overlay.Size.Height);

                //Vector3 pos1b =  U.WorldToScreen(Player.ViewMatrixOpenGL, Enemies[index].Position01, (int)screenSize.X, (int)screenSize.Y);
                Vector3 headPos = U.WorldToScreen(Player.ViewMatrixDirectX, Enemies[index].Position03, (int)screenSize.X, (int)screenSize.Y);
                Vector3 legPos = U.WorldToScreen(Player.ViewMatrixDirectX, Enemies[index].Position01, (int)screenSize.X, (int)screenSize.Y);
                Vector3 ttt1 = U.WorldToScreen(Player.ViewMatrixDirectX, Enemies[index].Position02, (int)screenSize.X, (int)screenSize.Y);
                Vector3 ttt2 = U.WorldToScreen(Player.ViewMatrixDirectX, Enemies[index].Position04, (int)screenSize.X, (int)screenSize.Y);


                //if (!U.areVectorsOnScreen(screenSize, pos1))
                // {
                //    pos1 = pos1b;
                //}
                if (!U.areVectorsOnScreen(screenSize, headPos, legPos))
                {
                    clearESPForUnit(index);
                    return;
                }
                //#TODO NEED HEAD / LEG POS!
                //#DEBUG
                float disY = legPos.Y - headPos.Y;
                float disX = disY / 2f;//keeping the aspect ratio where Y is 2X;
                //int rX = (int)(pos1.X - (disX / 2));
                int rX = (int)legPos.X - (int)(disX / 2);
                //int rY = (int)(pos1.Y - disY); //fix this shit
                int rY = (int)legPos.Y - (int)(disY);
                //Draw the BOX around the unit
                string msg = "";
                //if (TOGGLE_OPTIONS[1])
                //msg = Enemies[index].Address.ToString("X");
                msg = $"HP: {Enemies[index].CurrentHP}";
                Overlay.UpdateRect(index, rX, rY, (int)disX, (int)disY, boxColor, 2, msg);
             
            }
            catch (ArgumentOutOfRangeException)
            {
                return;
            }
            catch (InvalidOperationException)
            {
                return;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool enemyExists(IntPtr adr)
        {
            foreach (Entity e in Enemies)
            {
                if (e.Address == adr) return true;
            }
            return false;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            bgwProcessLooker.RunWorkerAsync();
            bgwEntityListManager.RunWorkerAsync();
            bgwFeatures.RunWorkerAsync();
        }

        private void bgwProcessLooker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                isProcessRunning = Memory.IsProcessRunning(Offsets.NAME_PROCESS);
                if (isProcessRunning)
                {
                    if (lastProcessState != isProcessRunning)
                    {
                        lastProcessState = isProcessRunning;
                        bgwProcessLooker.ReportProgress(1);
                    }
                    else
                    {
                        bgwProcessLooker.ReportProgress(2);
                    }
                }
                else
                {
                    bgwProcessLooker.ReportProgress(0);
                    lastProcessState = false;
                }
                
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bgwProcessLooker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //things that run when no process is found (function ends after the if statement
            if (e.ProgressPercentage == 0)
            {
                lblGameStatus.ForeColor = Color.Red;
                lblGameStatus.Text = "Game Status: N/A";
                Enemies.Clear();
                Memory.ResetGameModules();
                //#TODO Add a disalbe all function
                return;
            }
            //things to run once the game is found 
            else if (e.ProgressPercentage == 1)
            {
                //change the label to let the user know the process is found
                lblGameStatus.ForeColor = Color.Green;
                lblGameStatus.Text = "Game Status: Running";
            }
            //things to run every instance the game is found
            if (Offsets.ADR_MOD_ENGINE == IntPtr.Zero || Offsets.ADR_BASE == IntPtr.Zero)
                Memory.LoadGameModules();

        }

        private void bgwEntityListManager_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                System.Diagnostics.Stopwatch cycleTime = new System.Diagnostics.Stopwatch();

                const int targetFps = 144;
                const int intervalMs = 1000 / targetFps;
                if (!isProcessRunning ||
                    Offsets.ADR_BASE == IntPtr.Zero ||
                    Offsets.ADR_MOD_ENGINE == IntPtr.Zero)
                {

                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
                IntPtr prev = Player.Address;
                Player.Update();

                
                if (Enemies.Count == 0 || prev != Player.Address)
                {
                    Enemies.Clear();
                }
                const uint jumper = 0x60;
                IntPtr listAdr = new IntPtr((uint)Offsets.ADR_MOD_ENGINE + Offsets.mEntityListStart);
                for (uint i = 0; i < 100; i++)
                {
                    IntPtr adr = new IntPtr(Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(listAdr, i*jumper)));
                    if (enemyExists(adr)) continue;
                    int hp = Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(adr, (uint)Offsets.eCurrentHP));
                    int team = Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(adr, (uint)Offsets.eTeam));
                    if (hp > 0 && hp <= 1000 && team == 144)
                    {
                        Enemies.Add(new Entity(adr));
                        Overlay.AddRect(0, 0, 0, 0, Color.White);
                    }
                }

                int actionTime = (int)cycleTime.ElapsedMilliseconds;
                int timeToWait = intervalMs - actionTime;
                if (timeToWait > 0)
                    System.Threading.Thread.Sleep(timeToWait);
            }
        }

        private void bgwFeatures_DoWork(object sender, DoWorkEventArgs e)
        {
                    System.Diagnostics.Stopwatch cycleTime = new System.Diagnostics.Stopwatch();
                    cycleTime.Start();
                    const int targetFps = 144;
                    const int intervalMs = 1000 / targetFps;
            while (true)
            {
                try
                {
                    if (!isProcessRunning)
                    {
                        System.Threading.Thread.Sleep(1000);
                        continue;
                    }
                    string debugText = "";

                    int i = 0;
                    while (i < Enemies.Count)
                    {
                        Enemies[i].Update();
                        if (Enemies[i].isValid() == false)
                        {
                            Enemies.RemoveAt(i);
                            clearESPForUnit(i);
                            break;
                        }
                        if (Enemies[i].CurrentHP <= 0)
                        {
                            clearESPForUnit(i);
                            i++;
                            continue;
                        }
                        debugText += $"[{i}]: {Enemies[i].Address.ToString("X")}: HP: {Enemies[i].CurrentHP} {Environment.NewLine}" +
                            $"    Pos1: ({Enemies[i].Position01.X:00}, {Enemies[i].Position01.Y:00}, {Enemies[i].Position01.Z:00}){Environment.NewLine}" +
                            $"    Pos2: ({Enemies[i].Position02.X:00}, {Enemies[i].Position02.Y:00}, {Enemies[i].Position02.Z:00}){Environment.NewLine}" +
                            $"    Pos3: ({Enemies[i].Position03.X:00}, {Enemies[i].Position03.Y:00}, {Enemies[i].Position03.Z:00}){Environment.NewLine}" +
                            $"    Pos4: ({Enemies[i].Position04.X:00}, {Enemies[i].Position04.Y:00}, {Enemies[i].Position04.Z:00}){Environment.NewLine}";
                        updateESPBox(i);
                        i++;
                    }
                    WriteDebugText(debugText);
                    Overlay.UpdateOverlayLocation();
                    int actionTime = (int)cycleTime.ElapsedMilliseconds;
                    int timeToWait = intervalMs - actionTime;
                    if (timeToWait > 0)
                    {
                        System.Threading.Thread.Sleep(timeToWait);
                    }
                        cycleTime.Reset();
                    //txtDebug.Text = debugText;
                }
                catch (Exception)
                {
                    continue;

                }
            }
        }

        private void btnCopyDebug_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtDebug.Text);
        }
    }
}
