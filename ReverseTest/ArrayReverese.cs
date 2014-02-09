using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ReverseTest
{
    /**
     * Reverse items of array.
     * Looks like Array.Reverse has native code for reversing an array which sometimes doesn't apply and would fall back to using a simple for loop. 
     * In my testing Array.Reverse is very slightly faster than a simple for loop. In this test of reversing a 1,000,000 element array 1,000 times,
     * Array.Reverse is about 600ms whereas a for-loop is about 800ms.
     * 
     * I wouldn't recommend performance as a reason to use Array.Reverse though. It's a very minor difference which you'll lose the minute you load it into a List
     * which will loop through the array again. Regardless, you shouldn't worry about performance until you've profiled your app and identified the performance
     * bottlenecks.
     * ***/
    class ArrayReverse
    {
        public static void Test()
        {
            /**
             * You could use the Array.Reverse method:
             * byte[] bytes = GetTheBytes();
             * Array.Reverse(bytes, 0, bytes.Length);
             * Or, you could always use LINQ and do:
             * 
             * byte[] bytes = GetTheBytes();
             * byte[] reversed = bytes.Reverse().ToArray();
             * 
             * **************/
            var a = Enumerable.Range(0, 1000000).ToArray();

            var stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                Array.Reverse(a);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed Array.Reverse: " + stopwatch.ElapsedMilliseconds);

            stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                MyReverse(a);
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed MyReverse: " + stopwatch.ElapsedMilliseconds);
        }

        private static void MyReverse(int[] a)
        {
            int j = a.Length - 1;
            for (int i = 0; i < j; i++, j--)
            {
                int z = a[i];
                a[i] = a[j];
                a[j] = z;
            }
        }
    }
}
