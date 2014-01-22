using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseTest
{
	class Program
	{
		// Reverses order of the bits in each byte in the array 
		private static void Reverse(byte[] values)
		{
            // Precompute the value of each reversed byte 
            byte[] reversed = new byte[256];
            for (int i = 0; i < 256; i++) reversed[i] = Reverse((byte)i);

            // Reverse each byte in the input 
            for (int i = 0; i < values.Length; i++) values[i] = reversed[values[i]];
		}

		// Reverses order of the bits in each value in the array 
		private static void Reverse(long[] values)
		{
           
           
			throw new NotImplementedException();
		}

		static void Main(string[] args)
		{
		}

        // Reverses bits in a byte 
        static byte Reverse(byte b)
        {
            int rev = (b >> 4) | ((b & 0xf) << 4);
            rev = ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2);
            rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1);

            return (byte)rev;
        }
	}
}
