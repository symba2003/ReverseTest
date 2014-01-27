using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseTest
{
    /// <summary>
    /// Answer for A Programming Job Interview Challenge #2
    /// </summary>
    /**
     * Given a 1000X1000 grayscale image where each pixel is 8 bits, you need to reverse the order of the bits in each pixel. 
     * Example: 10001101 becomes 10110001, 00111101 becomes 10111100 and so on. Provide the most efficient solution 
     * in the manner of performance because another input is that you need to handle many images per second.
     * 
     * Task: http://www.dev102.com/2008/05/05/a-programming-job-interview-challenge-2/
     * 
     * Solution: http://www.dev102.com/2008/05/12/a-programming-job-interview-challenge-3/
     * 
     * CorrectAnswer:
     * This is a Big “O” problem that screams for a LUT (Look Up Table). Don’t spin on each pixel, create your 256 entry look up table of all possible reversals,
     * and do a quick pass over the 1000×1000 array by simply indexing into the table with the original value as the lookup.
     * 
     * Roberto Orsini wrote a really great implementation of how to parallelise the problem here - http://blog.fogbank.org/ , using threads from the threadPool,
     * but he didn’t use LUT to reverse the bits. Although this problem is perfectly parallelizable, you can’t rely on the fact that there is more than one CPU 
     * (unless stated it in the question). All of the others who provided bitwise operations: even if your solution is correct (XOR with 0xFF is wrong for instance)
     * it is not the most efficient one, you should assume that your code should work on every machine and every processor and you can’t rely on specific 
     * hardware where a specific operation may be very efficient.
     * 
     * This sample was marked as correct
     * ****/
    class Sample3_FromCompuboy
    {
        // Reverses bits in each byte in the array 
        private static void Reverse(byte[] values) 
        {
            // Precompute the value of each reversed byte 
            byte[] reverse = new byte[256];
            for (int x = 0; x < 256; x++)
                reverse[x] = (byte) (((x & 1) << 7) + ((x & 2) << 5) + ((x & 4) << 3) + ((x & 8) <> 1) + ((x & 32) >> 3) + ((x & 64) >> 5) + ((x & 128) >> 7));
            // And then we simply iterate the pixels of our image and foreach pixel value ‘x’ we will substitute it with reverse[x].
            // ‘reverse’ can be computed once and will be reused for each image.
             for (int i = 0; i < values.Length; i++) values[i] = reverse[values[i]];
        }
    }
}
