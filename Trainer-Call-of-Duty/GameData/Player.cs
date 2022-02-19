using System;
using Microsoft.DirectX;
using Trainer_Call_of_Duty.Helpers;
namespace Trainer_Call_of_Duty.GameData
{
    public static class Player
    {
        public static Matrix ViewMatrixDirectX { get; private set; }
        public static IntPtr Address { get; private set; }
        public static int CurrentHP { get; private set; }

        public static void Update()
        {
            ViewMatrixDirectX = Memory.GetViewMatrix_DirectX(Memory.AddOffsetToIntPtr(Offsets.ADR_BASE, Offsets.pViewMatrix));
            Address = Memory.AddOffsetToIntPtr(Offsets.ADR_MOD_ENGINE, (uint)Offsets.mLocalPlayer);
            CurrentHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eCurrentHP));
        }
    }
}
