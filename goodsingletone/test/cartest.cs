using System;
using System.Threading;
using Xunit;
using GoodSingleton;

namespace GoodSingleton.Tests
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
                    race = true;
                    break; 
                }
            }
            
            Assert.False(race, "there is race condition")
        }

        private bool RunRaceConditionTest()
        {
            Car.Reset();

            Car first = null;
            Car second = null;

            var thread1 = new Thread(() => 
                first = Car.GetInstance("corvette"));
                
            var thread2 = new Thread(() => 
                second = Car.GetInstance("porsche"));

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            bool dif = !ReferenceEquals(first, second);

            return dif;
        }
    }
}
