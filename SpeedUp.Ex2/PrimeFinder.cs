using Aula_1;
using SpeedUp.Ex2.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpeedUp.Ex2
{
    class PrimeFinder
    {
        /// <summary>
        /// Delegate to the FindPrimesBeforeNumber to make passing functions as parameters possible
        /// </summary>
        public static Func<int, int, bool, List<int>> DelegateFindPrimesBeforeNumber = (startingNumber, qtdPrimes, parallel) =>
        {
            return FindPrimesBeforeNumber(startingNumber, qtdPrimes, parallel);
        };

        /// <summary>
        /// Delegate to the FindPrimesAfterNumber to make passing functions as parameters possible
        /// </summary>
        public static Func<int, int, bool, List<int>> DelegateFindPrimesAfterNumber = (startingNumber, qtdPrimes, parallel) =>
        {
            return FindPrimesAfterNumber(startingNumber, qtdPrimes, parallel);
        };

        /// <summary>
        /// Finds the a certain amount of prime numbers before a starting number (exclusive), handles if the execution is parallel or not based on parameters
        /// </summary>
        /// <param name="startingNumber">Number from which starts the verification of prime numbers</param>
        /// <param name="qtdPrimes">Quantity of prime numbers the function will find</param>
        /// <param name="parallel">"true" to run the core in multiple threads, "false" to run in one thread</param>
        /// <returns>List of prime numbers between the defined interval</returns>
        public static List<int> FindPrimesBeforeNumber(int startingNumber, int qtdPrimes, bool parallel = false)
        {
            if (parallel)
            {
                return FindPrimesBasedOnNumberParallel(startingNumber, qtdPrimes, new SubUtil());
            }
            return FindPrimesBasedOnNumber(startingNumber, qtdPrimes, new SubUtil());
        }

        /// <summary>
        /// Finds the a certain amount of prime numbers after a starting number (exclusive), handles if the execution is parallel or not based on parameters
        /// </summary>
        /// <param name="startingNumber">Number from which starts the verification of prime numbers</param>
        /// <param name="qtdPrimes">Quantity of prime numbers the function will find</param>
        /// <param name="parallel">"true" to run the core in multiple threads, "false" to run in one thread</param>
        /// <returns>List of prime numbers between the defined interval</returns>
        public static List<int> FindPrimesAfterNumber(int startingNumber, int qtdPrimes, bool parallel = false)
        {
            if (parallel)
            {
                return FindPrimesBasedOnNumberParallel(startingNumber, qtdPrimes, new SumUtil());
            }
            return FindPrimesBasedOnNumber(startingNumber, qtdPrimes, new SumUtil());
        }

        /// <summary>
        /// Finds a certain amount of prime numbers based on a starting number
        /// </summary>
        /// <param name="startingNumber">Number from which starts the verification of prime numbers</param>
        /// <param name="qtdPrimes">Quantity of prime numbers the function will find</param>
        /// <param name="op">Object that will tell which operations needs to be implemented</param>
        /// <returns>List of prime numbers between the defined interval</returns>
        private static List<int> FindPrimesBasedOnNumber(int startingNumber, int qtdPrimes, OperationHandler op)
        {
            startingNumber = op.Iterator(startingNumber); // Does not test the startingNumber
            List<int> primeNumbers = new List<int>();
            for (int i = startingNumber; primeNumbers.Count < qtdPrimes; i = op.Iterator(i))
            {
                if (i < 0)
                {
                    throw new Exception("Not enough prime numbers to find");
                }

                if (TestaPrimo.TestaPrimo3(i))
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        }


        /// <summary>
        /// Finds a certain amount of prime numbers based on a starting number using every thread the system has to offer
        /// </summary>
        /// <param name="startingNumber">Number from which starts the verification of prime numbers</param>
        /// <param name="qtdPrimes">Quantity of prime numbers the function will find</param>
        /// <param name="op">Object that will tell which operations needs to be implemented</param>
        /// <returns>List of prime numbers between the defined interval</returns>
        private static List<int> FindPrimesBasedOnNumberParallel(int startingNumber, int qtdPrimes, OperationHandler op)
        {
            startingNumber = op.Iterator(startingNumber); // Does not test the startingNumber
            List<int> primeNumbers = new List<int>();

            int start;
            int end = startingNumber;
            while (primeNumbers.Count < qtdPrimes)
            {
                // Creates a interval to test prime numbers in multiple threads
                start = end;
                end = op.Operator(start, qtdPrimes - primeNumbers.Count);

                // if the "end" is lesser than the "start" then:
                int startFor;
                int endFor;
                if (end < start)
                {
                    // Reverses the start to the end in order to the Parallel.For to understand the interval
                    startFor = end;
                    endFor = start;
                }
                else
                {
                    startFor = start;
                    endFor = end;
                }

                // Prevents testing of negative numbers
                if (endFor < 0)
                    endFor = 0;

                // If the first number to be tested is 0, then there was not enough prime numbers to find
                if (startFor == 0)
                {
                    throw new Exception("Not enough prime numbers to find");
                }

                // Tests each number in a parallel for
                Parallel.For(startFor, endFor, i =>
                {
                    if (TestaPrimo.TestaPrimo3(i))
                    {
                        // Creates a lock on primeNumbers to prevent Race Condition
                        lock (primeNumbers)
                        {
                            primeNumbers.Add(i);
                        }
                    }
                });
            }

            return primeNumbers;
        }
    }
}
