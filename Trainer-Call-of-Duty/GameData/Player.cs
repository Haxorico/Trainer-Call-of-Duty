using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            //IntPtr tmp = Memory.AddOffsetToIntPtr(Address, Offsets.ePosition03);
            //Origin = Memory.GetVector3(tmp);
            IntPtr tmp = Memory.AddOffsetToIntPtr(Offsets.MOD_BASE_ADR, Offsets.pViewMatrix);
            ViewMatrixOpenGL = Memory.GetViewMatrix_OpenGL(tmp);
            ViewMatrixDirectX = Memory.GetViewMatrix_DirectX(tmp);
            //CamHeight = Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Address, Offsets.pCamHeightOffset));
            //CamPosition = new Vector3(Origin.X, Origin.Y, (Origin.Z + Convert.ToSingle(CamHeight)));
            //Position = Memory.GetVector3(Address1);
            // Position = Memory.GetVector3(Address2 + Offsets.ePosition03);
        }
    }
}
