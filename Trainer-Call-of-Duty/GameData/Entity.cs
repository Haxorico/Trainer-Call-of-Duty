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
		private const uint SIZE = 0x200;//not sure about size yet
		public IntPtr Address { get; private set; }
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
		public uint Iterator { get; set; }
		public int CurrentHP { get; private set; }
		public int MaximumHP { get; private set; }

		public Entity(IntPtr address)
        {
			Address = address;
        }
		
		public bool isValid()
        {
			return Address != IntPtr.Zero;
        }

		public void Update()
        {
			byte[] data = Memory.GetBytesFromAddress(Address, SIZE);
			Team = BitConverter.ToInt32(data, Offsets.eTeam);
			CurrentHP = BitConverter.ToInt32(data, Offsets.eCurrentHP);
			MaximumHP = BitConverter.ToInt32(data, Offsets.eMaxHP);
			Position01 = U.ByteToVector3(data, Offsets.ePos1);
			Position02 = U.ByteToVector3(data, Offsets.ePos2);
			Position03 = U.ByteToVector3(data, Offsets.ePos3);
			Position04 = U.ByteToVector3(data, Offsets.ePos4);
			/*
			Team = Memory.GetIntFromAddress(Memory.AddOffsetToIntPtr(Address, Offsets.eTeam));
			CurrentHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eCurrentHP));
			MaximumHP = Memory.GetIntFromAddress(new IntPtr((uint)Address + Offsets.eMaxHP));
			Position01 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos1a));
			Position02 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos2a));
			Position03 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos3a));
			Position04 = Memory.GetVector3(new IntPtr((uint)Address + Offsets.ePos4a));
			*/
		}
	}
}
