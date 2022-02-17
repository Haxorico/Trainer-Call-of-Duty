using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Trainer_Call_of_Duty.Helpers;
using Trainer_Call_of_Duty.GameData;

namespace Trainer_Call_of_Duty
{
    public partial class Form1 : Form
    {

        private bool isProcessRunning = false;
        private bool lastProcessState = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

            bgwProcessLooker.RunWorkerAsync();
        }

        private void bgwProcessLooker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stopwatch cycleTime = new Stopwatch();

            const int targetFps = 60;
            const int intervalMs = 1000 / targetFps;
            while (true)
            {
                cycleTime.Restart();
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
                int actionTime = (int)cycleTime.ElapsedMilliseconds;
                if (intervalMs > actionTime)
                    System.Threading.Thread.Sleep(intervalMs - actionTime);
            }
        }

        private void bgwProcessLooker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //things that run when no process is found (function ends after the if statement
            if (e.ProgressPercentage == 0)
            {
                lblGameStatus.ForeColor = Color.Red;
                lblGameStatus.Text = "Game Status: N/A";
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
                //setup the static classes (player, refdef)
                //Player.Address = Offsets.MOD_BASE_ADR + Offsets.pBase;
                //Create hook for entityList
                //hookEntityList();
            }
            //things to run every instance the game is found
            if (Offsets.ADR_MOD_ENGINE == IntPtr.Zero || Offsets.ADR_BASE == IntPtr.Zero)
                Memory.LoadGameModules();
            Player.Update();
            //refreshEnemyData();

            //check if overlay is running

            //if (TOGGLE_OPTIONS[0])
            //    Overlay.UpdateOverlayLocation();
            //if (TOGGLE_OPTIONS[1])
            //    this.healPlayer();
            //if (TOGGLE_OPTIONS[2])
            //enableInfiniteAmmo();
            //else
            //disableInfiniteAmmo();
        }
    }
}
