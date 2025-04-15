using System;
using System.Threading;
using Xunit;

namespace BadSingleton.Tests
{
    public class CarTests
    {
        [Fact]
        public void DemonstrateRaceCondition()
        {
            bool race = false; 

            for (int i = 0; i < 100; i++) 
            {
                if (RunRaceConditionTest())
                {
                    Console.WriteLine("there is race condition");
                    race = true;
                    break; 
                }
                Console.WriteLine();
            }

            if (!race)
            {
                Console.WriteLine("there is no race condition");
            }
        }

        private bool RunRaceConditionTest()
        {
            Car.Reset();

            string firstCar = null;
            string secondCar = null;

            var thread1 = new Thread(() => 
                first = Car.GetInstance("corvette").Name);
                
            var thread2 = new Thread(() => 
                second = Car.GetInstance("porsche").Name);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            return first != second;
        }
    }
}
