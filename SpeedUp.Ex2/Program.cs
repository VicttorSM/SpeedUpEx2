using Aula_1;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeedUp.Ex2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string RA = "222170243";
                if (RA.Where(x => char.IsDigit(x)).Count() != RA.Length)
                {
                    throw new Exception("Not all characteres are digits");
                }
                int aaa = int.Parse(RA.Substring(0, 3));
                int bbb = int.Parse(RA.Substring(3, 3));
                int ccc = int.Parse(RA.Substring(6, 3));

                int P = bbb * ccc;

                string wayOfSearching;
                double timeNormal;
                double timeParallel;
                List<int> list;

                // Se P < 5000, encontre a soma dos aaa numeros primos imediatamente posteriores a P
                if (P < 5000)
                {
                    wayOfSearching = "after";
                    list = PrimeFinder.FindPrimesAfterNumber(P, aaa);
                    timeNormal = Benchmark.BenchmarkFunction(PrimeFinder.DelegateFindPrimesAfterNumber, P, aaa, false);
                    timeParallel = Benchmark.BenchmarkFunction(PrimeFinder.DelegateFindPrimesAfterNumber, P, aaa, true);
                }
                // Caso contrario, encontre a soma dos aaa numeros primos imediatamente anteriores a P
                else
                {
                    wayOfSearching = "before";
                    list = PrimeFinder.FindPrimesBeforeNumber(P, aaa);
                    timeNormal = Benchmark.AverageBenchmark(PrimeFinder.DelegateFindPrimesBeforeNumber, P, aaa, false, 50);
                    timeParallel = Benchmark.AverageBenchmark(PrimeFinder.DelegateFindPrimesBeforeNumber, P, aaa, true, 50);
                }
                double speedUp = Benchmark.SpeedUpCalculator(timeNormal, timeParallel);
                Console.WriteLine($"RA: {RA}");
                Console.WriteLine($"P: {P}");

                Console.WriteLine($"Finding {aaa} prime numbers {wayOfSearching} {P}");

                /// Prints all the prime numbers found
                Console.WriteLine("Prime numbers found:");
                Console.WriteLine(string.Join(" ", list.OrderBy(x => x)));

                Console.WriteLine("SOMA: " + list.Sum());
                Console.WriteLine($"Normal time: {Math.Round(timeNormal, 4)} ms");
                Console.WriteLine($"Parallel time: {Math.Round(timeParallel, 4)} ms");
                Console.WriteLine($"SpeedUp do paralelo em relação ao normal: {Math.Round(speedUp, 4)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Critical error: {ex.Message}");
            }
            Console.WriteLine("Press enter to close the program...");
            Console.ReadLine();
        }
    }
}
