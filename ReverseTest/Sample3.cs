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
   
     * 
     * This sample was marked as correct
     * ****/
    class Sample3_FromCompuboy
    {
        // Reverses bits in each byte in the array 
        public static void Reverse(byte[] values) 
        {
           // first pre-compute all reverses in a byte array simply like this:
            byte[] reverse = new byte[256];
            for (int x = 0; x < 256; x++)
                reverse[x] = (byte)(((x & 1) << 7) + ((x & 2) << 5) + ((x & 4) << 3) + ((x & 8) << 1) + ((x & 16) >> 1) + ((x & 32) >> 3) + ((x & 64) >> 5) + ((x & 128) >> 7));
                // на сайте не совсем корректно отобразилась эта строка (со смайликом)
                // reverse[x] = (byte) (((x & 1) << 7) + ((x & 2) << 5) + ((x & 4) << 3) + ((x & "8)" <> 1) + ((x & 32) >> 3) + ((x & 64) >> 5) + ((x & 128) >> 7));
            // но, судя по логике, представим ч = 87654321(порядковые номера бит). Тогда
                // 1. ((x & 1) << 7) = сдвигаем 1 на 7 бит влево
                // 2. ((x & 2) << 5) = сдвигаем 2 на 5 бит влево
                // 3. (x & 4) << 3 = сдвигаем 3 на 3 бита влево
                // 4. ((x & "8)" <> 1)- пока непонятная операция, см. ниже
                // 5. (x & 32) >> 3 = сдвигаем 6 на 3 бита вправо
                // 6. (x & 64) >> 5 = сдвигаем 7 на 5 бит вправо
                // 7. (x & 128) >> 7 = сдвигаем 8 на 7 бит вправо
                // После операций 1-3 и 5-7 получается: 123__678
                // Значит, в п.4 выполняется перемена местами 4 и 5 бита
                // скорее всего так: ((x & 8) << 1) + ((x & 16) >> 1)
                // And then we simply iterate the pixels of our image and foreach pixel value ‘x’ we will substitute it with reverse[x].
            // ‘reverse’ can be computed once and will be reused for each image.
             for (int i = 0; i < values.Length; i++) values[i] = reverse[values[i]];
        }
    }
}
