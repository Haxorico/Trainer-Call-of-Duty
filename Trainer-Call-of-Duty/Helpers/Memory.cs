using System;
using Microsoft.DirectX;
using System.Collections.Generic;
using System.Diagnostics;
using Trainer_Call_of_Duty.lib;




namespace Trainer_Call_of_Duty.Helpers
{
    public static class Memory
    {
        public static Process CurrentProcess { get; private set; }
        public static Dictionary<string, IntPtr> Modules = new Dictionary<string, IntPtr>();
   
        public static void ResetGameModules()
        {
            Offsets.ADR_BASE = IntPtr.Zero;
            Offsets.ADR_MOD_ENGINE = IntPtr.Zero;
        }

        public static void LoadGameModules()
        {
            //load the modules into the library
            LoadModules();
            if (Offsets.ADR_BASE == IntPtr.Zero)
                Offsets.ADR_BASE = GetModuleAddress(Offsets.NAME_BASE);
            if (Offsets.ADR_MOD_ENGINE == IntPtr.Zero)
                Offsets.ADR_MOD_ENGINE = GetModuleAddress(Offsets.NAME_MODULE_ENGINE);
        }

        public static void LoadModules()
        {
            Modules.Clear();
            foreach (ProcessModule m in CurrentProcess.Modules)
            {
                Modules.Add(m.ModuleName, m.BaseAddress);
            }
        }

        public static bool IsProcessRunning(Process p = null)
        {
            if (p == null)
                p = CurrentProcess;
            return GetProcessByName(p.ProcessName) != null;
        }
        public static bool IsProcessRunning(string processName)
        {
            return GetProcessByName(processName) != null;
        }

        private static IntPtr open(Process p)
        {
            Kernel32.ProcessAccessType access = Kernel32.ProcessAccessType.PROCESS_VM_READ
            | Kernel32.ProcessAccessType.PROCESS_VM_WRITE
            | Kernel32.ProcessAccessType.PROCESS_VM_OPERATION;
            return Kernel32.OpenProcess((uint)access, 1, (uint)p.Id);
        }
        private static void close(IntPtr hProcess)
        {
            int iRetValue;
            iRetValue = Kernel32.CloseHandle(hProcess);
            if (iRetValue == 0)
            {
                throw new Exception("CloseHandle Failed");
            }
        }
        private static byte[] read(IntPtr MemoryAddress, uint bytesToRead, IntPtr hProcess)
        {
            byte[] buffer = new byte[bytesToRead];
            IntPtr ptrBytesRead;
            Kernel32.ReadProcessMemory(hProcess, MemoryAddress, buffer, bytesToRead, out ptrBytesRead);
            return buffer;
        }
        private static byte[] write(IntPtr MemoryAddress, byte[] bytesToWrite, IntPtr hProcess)
        {
            IntPtr ptrBytesWritten;
            Kernel32.WriteProcessMemory(hProcess, MemoryAddress, bytesToWrite, (uint)bytesToWrite.Length, out ptrBytesWritten);
            if (bytesToWrite.Length > 4)
                return BitConverter.GetBytes(ptrBytesWritten.ToInt64());
            return BitConverter.GetBytes(ptrBytesWritten.ToInt32());
        }


        public static Process GetProcessByName(string processName)
        {
            Process[] p = Process.GetProcessesByName(processName);
            if (p.Length != 0)
            {
                CurrentProcess = p[0];
                return p[0];
            }
            return null;
        }
        public static IntPtr GetModuleAddress(string moduleName)
        {
            try
            {
                return Modules[moduleName];
            }
            catch
            {
                return IntPtr.Zero;
            }
              
        }
        public static byte GetByteFromAddress(IntPtr address)
        {
            byte[] buffer = GetBytesFromAddress(address, 1);
            return buffer[0];
        }
        public static byte[] GetBytesFromAddress(IntPtr address, uint byteSize = 4)
        {
            IntPtr hProcess = open(CurrentProcess);
            byte[] outputVal = read(address, byteSize, hProcess);
            close(hProcess);
            return outputVal;
        }
        public static int GetIntFromAddress(IntPtr address)
        {
            return BitConverter.ToInt32(GetBytesFromAddress(address), 0);
        }
        public static uint GetUIntFromAddress(IntPtr address)
        {
            return BitConverter.ToUInt32(GetBytesFromAddress(address), 0);
        }
        public static long GetLongFromAddress(IntPtr address)
        {
            return BitConverter.ToInt64(GetBytesFromAddress(address, 8), 0);
        }
        public static float GetFloatFromAddress(IntPtr address)
        {
            return BitConverter.ToSingle(GetBytesFromAddress(address), 0);
        }
        public static double GetDoubleFromAddress(IntPtr address)
        {
            return BitConverter.ToDouble(GetBytesFromAddress(address, 8), 0);
        }

        public static Vector2 GetVector2(IntPtr address)
        {
            byte[] matData = GetBytesFromAddress(address, 8);
            Vector2 ret = new Vector2();
            ret.X = BitConverter.ToSingle(matData, 0);
            ret.Y = BitConverter.ToSingle(matData, 4);
            return ret;
        }
        public static Vector3 GetVector3(IntPtr address)
        {
            byte[] matData = GetBytesFromAddress(address, 12);
            Vector3 ret = new Vector3();
            ret.X = BitConverter.ToSingle(matData, 0);
            ret.Y = BitConverter.ToSingle(matData, 4);
            ret.Z = BitConverter.ToSingle(matData, 8);
            return ret;
        }

        public static Matrix GetViewMatrix_OpenGL(IntPtr address)
        {
            byte[] matData = GetBytesFromAddress(address, 4 * 16);
            float[] ret = new float[16];
            for (int i = 0; i < 16; i++)
            {
                ret[i] = BitConverter.ToSingle(matData, i * 4);
            }
            Matrix matrix = new Matrix();
         
            matrix.M11 = ret[0];
            matrix.M12 = ret[1];
            matrix.M13 = ret[2];
            matrix.M14 = ret[3];
            matrix.M21 = ret[4];
            matrix.M22 = ret[5];
            matrix.M23 = ret[6];
            matrix.M24 = ret[7];
            matrix.M31 = ret[8];
            matrix.M32 = ret[9];
            matrix.M33 = ret[10];
            matrix.M34 = ret[11];
            matrix.M41 = ret[12];
            matrix.M42 = ret[13];
            matrix.M43 = ret[14];
            matrix.M44 = ret[15];
            return matrix;
            //return new Matrix(ret[0], ret[1], ret[2], ret[3], ret[4], ret[5], ret[6], ret[7], ret[8], ret[9], ret[10], ret[11], ret[12], ret[13], ret[14], ret[15]);
        }

        public static Matrix GetViewMatrix_DirectX(IntPtr address)
        {
            byte[] matData = GetBytesFromAddress(address, 4 * 16);
            float[] mm = new float[16];
            for (int i = 0; i < 16; i++)
            {
                mm[i] = BitConverter.ToSingle(matData, i * 4);
            }
            Matrix matrix = new Matrix();
            matrix.M11 = mm[0];
            matrix.M12 = mm[4];
            matrix.M13 = mm[8];
            matrix.M14 = mm[12];
            matrix.M21 = mm[1];
            matrix.M22 = mm[5];
            matrix.M23 = mm[9];
            matrix.M24 = mm[13];
            matrix.M31 = mm[2];
            matrix.M32 = mm[6];
            matrix.M33 = mm[10];
            matrix.M34 = mm[14];
            matrix.M41 = mm[3];
            matrix.M42 = mm[7];
            matrix.M43 = mm[11];
            matrix.M44 = mm[15];
            return matrix;
            //return new Matrix(mm[0], mm[4], mm[8], mm[12], mm[1], mm[5], mm[9], mm[13], mm[2], mm[6], mm[10], mm[14], mm[3], mm[7], mm[11], mm[15]);
        }

        public static byte[] WriteBytesToAddress(IntPtr address, byte[] value)
        {
            IntPtr hProcess = open(CurrentProcess);
            value = write(address, value, hProcess);
            close(hProcess);
            return value;
        }
        public static int WriteIntToAddress(IntPtr address, int value)
        {
            return BitConverter.ToInt32(WriteBytesToAddress(address, BitConverter.GetBytes(value)), 0);
        }
        public static float WriteFloatToAddress(IntPtr address, float value)
        {
            return BitConverter.ToSingle(WriteBytesToAddress(address, BitConverter.GetBytes(value)), 0);
        }
        public static double WriteDoubleToAddress(IntPtr address, double value)
        {
            byte[] ret = WriteBytesToAddress(address, BitConverter.GetBytes(value));
            return BitConverter.ToDouble(ret, 0);
        }
        public static byte[] NopOutAddress(IntPtr address, uint amountOfNops)
        {
            byte[] value = new byte[amountOfNops];
            for (int i = 0; i < amountOfNops; i++)
            {
                value[i] = 0x90;
            }
            return WriteBytesToAddress(address, value);
        }
        public static IntPtr GetResolvedAddress32(IntPtr MODULE_ADDRESS, IntPtr BASE_ADDRESS, uint[] offsets)
        {
            IntPtr a = AddOffsetToIntPtr(MODULE_ADDRESS, BASE_ADDRESS);
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                a = AddOffsetToIntPtr(new IntPtr(GetUIntFromAddress(a)), offsets[i]);
            }
            return AddOffsetToIntPtr(new IntPtr(GetUIntFromAddress(a)), offsets[offsets.Length - 1]);
        }
        public static IntPtr GetResolvedAddress64(IntPtr MODULE_ADDRESS, IntPtr BASE_ADDRESS, uint[] offsets)
        {
            IntPtr a = AddOffsetToIntPtr(MODULE_ADDRESS, BASE_ADDRESS);
            for (int i = 0; i < offsets.Length - 1; i++)
            {
                a = new IntPtr(GetLongFromAddress(a));
                a = new IntPtr(a.ToInt64() + offsets[i]);
            }
            return AddOffsetToIntPtr(new IntPtr(GetLongFromAddress(a)), offsets[offsets.Length - 1]);
        }
        public static IntPtr AddOffsetToIntPtr(IntPtr adr, uint offset)
        {
            return new IntPtr(adr.ToInt64() + offset);
        }
        public static IntPtr AddOffsetToIntPtr(IntPtr adr, IntPtr offset)
        {
            return new IntPtr(adr.ToInt64() + offset.ToInt64());
        }
    }
}
