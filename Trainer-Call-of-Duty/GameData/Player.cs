using System;
using Microsoft.DirectX;
using Trainer_Call_of_Duty.Helpers;
namespace Trainer_Call_of_Duty.GameData
{
    public static class Player
    {
        public static Matrix ViewMatrixOpenGL { get; set; }
        public static Matrix ViewMatrixDirectX { get; set; }

        public static void Update()
        {
            
            IntPtr tmp = Memory.AddOffsetToIntPtr(Offsets.MOD_BASE_ADR, Offsets.pViewMatrix);
            ViewMatrixOpenGL = Memory.GetViewMatrix_OpenGL(tmp);
            ViewMatrixDirectX = Memory.GetViewMatrix_DirectX(tmp);
        }
    }
}
