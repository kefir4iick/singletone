using System;
using System.Threading;

namespace BadSingleton
{
    public sealed class Car
    {
        private static Car _instance;
        public string Name { get; private set; }

        private Car(string name)
        {
            Thread.Sleep(1000);
            Name = name;
            Console.WriteLine($"car: {Name}");
        }

        public static Car GetInstance(string name)
        {
            if (_instance == null)
            {
                _instance = new Car(name);
            }
            return _instance;
        }

        public void Drive() => Console.WriteLine($"{Name} go");

        public static void Reset()
        {
            _instance = null;
        }
    }

    class Program
    {
        static void Main()
        {
            Thread thread1 = new Thread(() => {
                var car = Car.GetInstance("chevrolet corvette"); // I'll buy this baby in 3 years
                car.Drive();
            });
            
            Thread thread2 = new Thread(() => {
                var car = Car.GetInstance("porsche 911 gt3 rs"); // and I will buy this one after I move to Monaco
                car.Drive();
            });

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
            
            var final = Car.GetInstance("mercedes amg f1 w12 e"); // I'm ready to sell my soul for this racing car
            
            Console.WriteLine($"last car: {final.Name}");
            
        }
    }
}
