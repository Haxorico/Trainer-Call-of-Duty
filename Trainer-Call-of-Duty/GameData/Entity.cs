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
		public IntPtr Address { get; private set; }
		public IntPtr Part2 { get;private set; }
		public IntPtr NextEntity { get;private set; }
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
		public int CurrentHP { get; private set; }
		public int MaximumHP { get; private set; }

		public Entity(IntPtr address)
        {
			Address = address;
        }

		public void Update()
        {
			//P2 = gameProcess.Process.Read<IntPtr>(Address + Offsets.ePart2);
			Part2 = new IntPtr(Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Address, Offsets.ePart2)));
			CurrentHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eCurrentHP));
			MaximumHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eMaxHP));
			NextEntity = new IntPtr(Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Part2, Offsets.eNextEntity)));

			Position01 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos1a));
			Position02 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos2a));
			Position03 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos3a));
			Position04 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos4a));

			Position05 = Memory.GetVector3(new IntPtr((uint)Part2 + Offsets.ePos4a));
			Position06 = Memory.GetVector3(new IntPtr((uint)Part2 + Offsets.ePos4a));
			Position07 = Memory.GetVector3(new IntPtr((uint)Part2 + Offsets.ePos4a));
			Position08 = Memory.GetVector3(new IntPtr((uint)Part2 + Offsets.ePos4a));
			Position09 = Memory.GetVector3(new IntPtr((uint)Part2 + Offsets.ePos4a));
		}
	}
}
