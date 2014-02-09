using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ReverseTest
{
	class Program
	{
		

		static void Main(string[] args)
		{
            int numItemsToTest = 100000000;
            for (int j = 0; j < 10; j++)
            {
                // init array
                byte[] values = new byte[numItemsToTest];

                // prefill array with the test data
                Random rnd = new Random();
                rnd.NextBytes(values);

                // start timer
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Sample3_FromCompuboy.Reverse(values);
                timer.Stop();
                Console.WriteLine(String.Format("Sample3 Reverses byte array. Elapsed time: {0}", timer.Elapsed));
                Console.WriteLine();

                timer.Reset();
                timer.Start();
                //Sample1 - немного более быстрый (за все 10 тестов - ни разу не "отстал" от Sample3)
                Sample1.Reverse(values);
                //INFO: Sample3 двигает каждый бит в отдельности
                // итого получается:
                // 8 операций сдвига, 8 операций & (logical bitwise AND)
                // и результат суммируется: 7 сложений

                // Sample1: двигает биты группами (по 4 бита, по 2 бита и по 1 биту)
                // итого получается:
                // 6 операций сдвига, 5 операций  & (logical bitwise AND), 3 операции | ( bitwise OR)
                timer.Stop();
                Console.WriteLine(String.Format("Sample1 Reverses byte array. Elapsed time: {0}", timer.Elapsed));
                Console.WriteLine();
            }
            
            
            
            //TestLeftShifOperator();

            //Igor Ostrovsky sample
            //Sample1.Run();

            // Irinev Sample
            //byte[,] bitmap = new byte[10,10];
            //Task.ReverseBitmap(bitmap);


            Console.ReadLine();
		}

        static void TestLeftShifOperator() 
        {
            int i = 1;
            long lg = 1;
            // Shift i one bit to the left. The result is 2.
            Console.WriteLine("0x{0:x}", i << 1);
            // In binary, 33 is 100001. Because the value of the five low-order 
            // bits is 1, the result of the shift is again 2. 
            Console.WriteLine("0x{0:x}", i << 33);
            // Because the type of lg is long, the shift is the value of the six 
            // low-order bits. In this example, the shift is 33, and the value of 
            // lg is shifted 33 bits to the left. 
            //     In binary:     10 0000 0000 0000 0000 0000 0000 0000 0000  
            //     In hexadecimal: 2    0    0    0    0    0    0    0    0
            Console.WriteLine("0x{0:x}, {1}", lg << 33, lg << 33);
        }

        
	}
}
