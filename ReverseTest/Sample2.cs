using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseTest
{
    // 
   // class Sample2_AntonIrinev
   // {
    // Another one correct answer by Sample2_AntonIrinev
    // данный вариант был отмечен как корректный, но реверс битов  возвращает всегда 255 (возможно, какая то опечатка в коде на сайте
    // http://www.dev102.com/2008/05/05/a-programming-job-interview-challenge-2/
    static class Task
    {
        private static byte[] table = new byte[256]; // 256 = 2^8;
        private static byte[] pow2 = new byte[8] { 1, 2, 4, 8, 16, 32, 64, 128 };

        static Task()
        {
            for (int i = 0; i < table.Length; i++)
            {
                // reverse i and save the result in table[i]
               // for (int j = 0; j 0) table[i] |= pow2[8 - j - 1];
                for (int j = 0; j < 8; j++)
                    table[i] |= pow2[8 - j - 1];
                // COMMENT: what is this? всегда возвращает 255 - неверный код реверса битов!!!
            }
        }


        public static void ReverseBitmap(byte[,] bitmap)
        {
            // GetUpperBound - Gets the index of the last element of the specified dimension in the array.
            for (int i = 0; i < bitmap.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < bitmap.GetUpperBound(1) + 1; j++)
                {
                    bitmap[i, j] = table[bitmap[i, j]];
                }
            }
        }
    }
}
