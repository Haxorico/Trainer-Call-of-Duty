using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.DirectX;
using Trainer_Call_of_Duty.Helpers;

namespace Trainer_Call_of_Duty.GameData
{
    public class Entity
    {
		const int SIZE = 0x360; //not sure aobut size!
		public IntPtr Address { get; set; }
		//public int Team { get; private set; }  
		//public byte IsFiring { get; private set; }  
		//public int State { get; private set; }   
		public long Base { get; private set; }
		//public byte Type { get; private set; }
		public int Team { get; private set; }
		public Vector3 Position01 { get; private set; }
		public Vector3 Position02 { get; private set; }
		public Vector3 Position03 { get; private set; }
		public Vector3 Position04 { get; private set; }
		public Vector3 Position05 { get; private set; }
		public Vector3 Position06 { get; private set; }
		public Vector3 Position07 { get; private set; }
		public Vector3 Position08 { get; private set; }
		public Vector3 Position09 { get; private set; }
		public uint Iterator { get; set; }
		public int HP { get; private set; }
		public int HP_MAX { get; private set; }
	}
}
