using System;
using Microsoft.DirectX;
using Trainer_Call_of_Duty.Helpers;
namespace Trainer_Call_of_Duty.GameData
{
    public static class Player
    {
        public static Matrix ViewMatrixOpenGL { get; private set; }
        public static Matrix ViewMatrixDirectX { get; private set; }
        public static IntPtr Address { get; private set; }
        public static IntPtr Part2 {  get; private set; }
        public static IntPtr NextEntity { get; private set; }   
        public static int CurrentHP { get; private set; }

        public static void Update()
        {
            
            IntPtr tmp = Memory.AddOffsetToIntPtr(Offsets.ADR_BASE, Offsets.pViewMatrix);
            ViewMatrixOpenGL = Memory.GetViewMatrix_OpenGL(tmp);
            ViewMatrixDirectX = Memory.GetViewMatrix_DirectX(tmp);

            Address = Memory.AddOffsetToIntPtr(Offsets.ADR_MOD_ENGINE, Offsets.eFirstEntityAdr);
            CurrentHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eCurrentHP));
            Part2 = new IntPtr(Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Address, Offsets.ePart2)));
            NextEntity = new IntPtr(Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Part2, Offsets.eNextEntity)));


        }
    }
}
