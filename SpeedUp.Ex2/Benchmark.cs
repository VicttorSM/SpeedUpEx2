using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Aula_1
{
    /// <summary>
    /// Class dedicated to execute benchmarks of functions
    /// </summary>
    class Benchmark
    {

        /// <summary>
        /// Calculates the SpeedUp value between 2 functions
        /// </summary>
        /// <param name="baseFunctionTime">The base function</param>
        /// <param name="otherFunctionTime">The function it will be comparing to</param>
        /// <returns>Value of the SpeedUp of the otherFunction compared to the baseFunction</returns>
        public static double SpeedUpCalculator(double baseFunctionTime, double otherFunctionTime)
        {
            return baseFunctionTime / otherFunctionTime;
        }

        /// <summary>
        /// Calculates the average time a function takes to execute
        /// </summary>
        /// <param name="function">The function to be benchmarked</param>
        /// <param name="T1">The 1 parameter the function takes</param>
        /// <param name="T2">The 2 parameter the function takes</param>
        /// <param name="T3">The 3 parameter the function takes</param>
        /// <param name="times">The number of times the function will be executed</param>
        /// <returns>The average time in milliseconds it took to execute the function</returns>
        public static double AverageBenchmark(Func<int, int, bool, List<int>> function, int T1, int T2, bool T3, int times)
        {
            List<double> individualBenchResults = new List<double>();
            for (int i = 0; i < times; i++)
            {
                individualBenchResults.Add(BenchmarkFunction(function, T1, T2, T3));
            }
            return individualBenchResults.Average();
        }

        /// <summary>
        /// Calculates the time a function takes to execute once
        /// </summary>
        /// <param name="function">The function to be benchmarked</param>
        /// <param name="T1">The 1 parameter the function takes</param>
        /// <param name="T2">The 2 parameter the function takes</param>
        /// <param name="T3">The 3 parameter the function takes</param>
        /// <returns>The time in milliseconds it took to execute the function</returns>
        public static double BenchmarkFunction(Func<int, int, bool, List<int>> function, int T1, int T2, bool T3)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            function(T1, T2, T3);
            return stopwatch.Elapsed.TotalMilliseconds;
        }

    }
}
