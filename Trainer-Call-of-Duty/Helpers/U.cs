using System;
using System.Globalization;
using Microsoft.DirectX;

namespace Trainer_Call_of_Duty.Helpers
{
    public static class U
    {

        public static string strPadding(uint minSize, string input, string padWith = "0")
        {
            while (input.Length < minSize)
            {
                input = padWith + input;
            }
            return input;
        }
        public static Vector3 ByteToVector3(byte[] data, int startIndex = 0)
        {
            float x = BitConverter.ToSingle(data, startIndex + 0);
            float y = BitConverter.ToSingle(data, startIndex + 4);
            float z = BitConverter.ToSingle(data, startIndex + 8);
            return new Vector3(x, y, z);
        }
        public static string Bytes2Hex(byte[] bytes)
        {
            string ret = "";
            foreach (byte b in bytes)
            {
                ret += b.ToString("X") + " ";
            }
            return ret.Substring(0, ret.Length - 1);
        }
        public static string Dec2Hex(int dec)
        {
            return dec.ToString("X");
        }
        public static string Dec2Hex(long dec)
        {
            return dec.ToString("X");
        }
        public static long Hex2Dec(string hex)
        {
            return long.Parse(hex, NumberStyles.HexNumber);
        }
        public static byte[] Hex2Bytes(string hex, uint bytesAmout = 4)
        {
            hex = strPadding(bytesAmout * 2, hex);
            byte[] ret = new byte[bytesAmout];
            for (int i = 0; i < bytesAmout; i++)
            {
                ret[bytesAmout - 1 - i] = (byte)Hex2Dec(hex.Substring(i * 2, 2));
            }
            return ret;
        }


        public static Vector3 WorldToScreen(Matrix matrix, Vector3 pos, int width, int height)
        {
            //multiply vector against matrix
            Vector3 screenPos = new Vector3(
                (matrix.M11 * pos.X) + (matrix.M21 * pos.Y) + (matrix.M31 * pos.Z) + matrix.M41,
                (matrix.M12 * pos.X) + (matrix.M22 * pos.Y) + (matrix.M32 * pos.Z) + matrix.M42,
                (matrix.M14 * pos.X) + (matrix.M24 * pos.Y) + (matrix.M34 * pos.Z) + matrix.M44
                );

            //camera position (eye level/middle of screen)
            Vector2 camPos = new Vector2(width / 2f, height / 2f);

            //convert to homogeneous position
            return new Vector3(camPos.X + (camPos.X * screenPos.X / screenPos.Z),
                camPos.Y - (camPos.Y * screenPos.Y / screenPos.Z),
                screenPos.Z);
        }
        public static bool isVectorOnScreen(Vector3 pos, Vector2 screenSize)
        {
            return !(pos.X < 0 || pos.X > screenSize.X || pos.Y < 0 || pos.Y > screenSize.Y || pos.Z < 0);
        }
        public static bool areVectorsOnScreen(Vector2 screenSize, params Vector3[] positions)
        {
            foreach (Vector3 pos in positions)
            {
                if (!isVectorOnScreen(pos, screenSize))
                {
                    return false;
                }
            }
            return true;
        }
    }
}