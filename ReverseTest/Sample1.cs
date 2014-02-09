using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ReverseTest
{
    /// <summary>
    /// Solution provided by Igor Ostrovsky (i have sent it)
    /// </summary>
    /// <remarks>http://igoro.com/archive/programming-job-interview-challenge/.
    /// 
    /// The task:
    /// Given a 1000X1000 grayscale image where each pixel is 8 bits, you need to reverse the order of the bits in each pixel.
    /// Example: 10001101 becomes 10110001, 00111101 becomes 10111100 and so on. Provide the most efficient solution in the manner
    /// of performance because another input is that you need to handle many images per second. Think…
    /// 
    /// The challenge is to reverse the bits in each byte, given a large array of bytes. What is the fastest possible solution?</remarks>
    class Sample1
    {
        /**
         * There are only 256 possible values for an 8-bit integer, so it makes sense to pre-compute the reverse of each ahead of time.
         * Then, we can simply scan through the input, and reverse each byte by a simple array lookup. We pay a small initialization cost,
         * but after that, we can reverse bytes nearly as fast as we can read them.
         * 
         * And how to efficiently reverse a byte? There is an obvious solution which uses a for loop and some bit manipulation. 
         * My solution below uses a slightly faster approach. It is a nice trick, and not very difficult, so I will leave 
         * the details as an exercise to the reader
         * 
         * Another correct opinions:
         * I think the best solution for this problem would be the construction of an array to store the inverted bits for each byte. 
         * After you have calculated this array, you can easily look up the solution via its index. in the array.
         * This should be much faster then reversing the order of every bit you encounter by hand.
         * The size of this cache is minimal… we only need to store 256 entries here - Compared to the 1.000.000 swapping opperations 
         * for each picture.
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
         * Keep a 256 size array of bytes (byte[256]). For each byte we calculate, we keep its result value in the array, as: 
         * arr[x]=f(x), so next time we get the same byte, we have its calculation in O(1). So our function will run maximum 256 times.
         * 
         * Another one correct answer:
         * To eek out additional speed, construct the array as a static final by hand. Second thought would be to play with larger
         * elements in the array as the source is 1000bytes wide and converting two bytes at a time would be faster.
         * Other considerations would be to see if using multi-processors can be made part of the solution and split the processing
         * into threads.
         * 
         * Another one correct answer:
         * I think there is only correct answer to this question in a job interview: If this is really performance critical,
         * I can’t answer it without implementing at least the two most obvious solutions (using an LUT (look up table) or 
         * using logical operations+SIMD), and profiling them. Anything else would be guesswork and superstition, todays’s CPUs
         * are to complex to decide something like this in an interview.
         * 
         * 
         * I think a lookup in a prefilled table with already reversed bits is the most fast solution trading less than 1 KB memory for it
         * and no functions or bitwise operations used. The whole processing becomes bytewise this way.
         * 
         * There is only 256 possible argument values for reversing function. the fastest solution will be to use precalcutaion.
         * 1. create lookup array:      byte pre[256];
         * 2. fill it using any method.
         * 3. use: byte reverse(byte x){return pre[x];}
         * 
         * From Adam B blog:
         * Assuming you can afford (быть в состоянии, позволить себе) 256 bytes for a cache, one really fast general-purpose way to implement this
         * is to create an array with an entry for each possible value of a byte, storing the reversed bits of the byte. For each byte in the image,
         * you can then look the reversed bits nearly instantly. The runtime of the algorithm is O(n) with a trivial constant multiplier.
         * This algorithm, by the way, is insanely (безумно) easy to make run in multiple threads and should run extremely fast on a multi-core SIMD processor.
         * 
         * */
        public static void Run() 
        {
            //TODO: reverse long array
            //long l = 4294967296;
            //long l = -2814299246381726651;
            //Reverse(l);


            int numItemsToTest = 100000000;
            for (int j = 0; j < 10; j++)
            {
                // init array
                byte[] values = new byte[numItemsToTest];

                // prefill array with the test data
                Random rnd = new Random();
                rnd.NextBytes(values);

                // display first 10 items of test data
               // PrintItems("Source test data (first {0} items)", values);

                // start timer
                Stopwatch timer = new Stopwatch();
                timer.Start();

                Reverse(values);

                timer.Stop();

                // display result for first 10 items of test data
               // PrintItems("Result for test data (first {0} items)", values);
                //Console.WriteLine();
                Console.WriteLine(String.Format("Reveres byte array. Elapsed time: {0}", timer.Elapsed));
                Console.WriteLine();
                //Console.WriteLine();
            }
        }

        // Reverses bits in each byte in the array 
        public static void Reverse(byte[] values)
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
            // по тому же принципу попробую ковертировать long
            // long - Signed 64-bit integer
            // Range: –9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
            // диапозон значений - слишком большой, чтобы сохранить предварительно вычисленные значения ( в массиве reversed)
            // попробую ковертировать каждое значение в отдельности

            throw new NotImplementedException();
        }

        // Reverses bits in a byte 
        static byte Reverse(byte b)
        {
            // 0xffff  = 1111 1111 1111 1111
            // 0xcc = 11001100
            // 0x33 = 00110011
            // 0xaa = 10101010
            // 0x55 = 01010101

            // original bits of "b" 87654321 (these are bit numbers)
            // 1. b >> 4                        = 00008765
            // 2. b & 0xf                       = 00004321
            // 3. (b & 0xf) << 4                = 43210000
            // 4. (b >> 4) | ((b & 0xf) << 4)   = 43218765
            int rev = (b >> 4) | ((b & 0xf) << 4); 

            // 5. rev & 0xcc                    = 43008700
            // 6. (rev & 0xcc) >> 2             = 00430087
            // 7. rev & 0x33                    = 00210065
            // 8. (rev & 0x33) << 2             = 21006500
            // 9. ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2) = 21436587 
            rev = ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2);


            // 10. rev & 0xaa                   = 20406080
            // 11. (rev & 0xaa) >> 1            = 02040608
            // 12. rev & 0x55                   = 01030507
            // 13. (rev & 0x55) << 1            = 10305070
            // 14. ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1) = 12345678
            rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1);

            return (byte)rev;
        }

        static uint ReverseBits(uint x) {
            x = ((x >> 16) | (x << 16));
            x = (((x & 0xff00ff00) >> 8) | ((x & 0x00ff00ff) << 8));
            x = (((x & 0xf0f0f0f0) >> 4) | ((x & 0x0f0f0f0f) << 4));
            x = (((x & 0xcccccccc) >> 2) | ((x & 0x33333333) << 2));
            x = (((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1));
            return x;
        }

        // Reverses bits in a long 
        static long Reverse(long l)
        {
            // 0xf  = 1111
            // 0xcc = 11001100
            // 0x33 = 00110011
            // 0xaa = 10101010
            // 0x55 = 01010101

            // original bits of "l" 64 63 62 61 60 ...32 31 30 29 28 27 26 25 24 23 22 21 20 19 18 17 16 15 14 13 12 11 10 9 8 7 6 5 4 3 2 1 
            // (these are bit numbers)

           
            // 1. b >> 4                        = 00008765
            // 2. b & 0xf                       = 00004321
            // 3. (b & 0xf) << 4                = 43210000
            // 4. (b >> 4) | ((b & 0xf) << 4)   = 43218765
            long rev = (l >> 32) | ((l & 0xffffffff) << 32);
            // now bits: 32 31 30 .... 4 3 2 1 64 63 62 61 ... 35 34 33

            // 5. rev & 0xcc                    = 43008700
            // 6. (rev & 0xcc) >> 2             = 00430087
            // 7. rev & 0x33                    = 00210065
            // 8. (rev & 0x33) << 2             = 21006500
            // 9. ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2) = 21436587 
        //    long l1 =  (long)0xFFFF0000FFFF0000L;
        //    rev = ((rev & l1) >> 16) | ((rev & 0xFFFF0000FFFFL) << 16);
            // now bits: 8 7 6 5 4 3 2 1 16 15  14 13 12 11 10 9 24 23 22 21 20 19 18 17 32 31 30 29 28 27 26 25

            //// 10. rev & 0xaa                   = 20406080
            //// 11. (rev & 0xaa) >> 1            = 02040608
            //// 12. rev & 0x55                   = 01030507
            //// 13. (rev & 0x55) << 1            = 10305070
            //// 14. ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1) = 12345678
            //rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1);

            return (long)rev;
        }

        public static uint InvertBits(uint i)
        {
            // Here is how bits sequence can be inverted without using the bitwise NOT operator (~).
            // We will use XOR operation to invert the bits.
            return i ^ 0xFFFFFFFF;
        }

        static void PrintItems(string messageTitle, byte[] values) 
        {
            int numOfItems2Print = 10;

            Console.WriteLine(messageTitle, numOfItems2Print);
            for (int i = 0; i < numOfItems2Print; i++)
                Console.Write(String.Format("{0}, ", values[i]));
            Console.WriteLine();
        }
    }
}
