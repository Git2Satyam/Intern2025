using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp
{
    public class Test
    {
        // Inheritance

        public class Animal  // base class or Parent class
        {
            public void Cat()
            {
                Console.WriteLine("This is a cat");
            }
        }

        public class Dog: Animal
        {
            public void Bark()
            {
                Console.WriteLine();
            }
        }

        // Encapsulation

        public class Account()
        {
            private double balance;   // amount is encapsulate

            public void Deposits(double amount)
            {
                if(amount > 0)
                {
                    balance += amount;
                }
            }

            public double ReturnBalance()
            {
                return balance;
            }
        }
        
        // Polymorphism

        public class AnimalSound
        {
            public virtual void Speak()
            {
                Console.WriteLine("Animal Sound");
            }
        }

        public class Cat: AnimalSound
        {
            public override void Speak()
            {
                Console.WriteLine("Cat speak meow");
            }
        }

        // Abstraction 
        public abstract class Shape
        {
            public abstract double calculateShape();
        }

        public class Circle: Shape
        {
            public int radius;
            public override double calculateShape()
            {
                var area = (Math.PI * radius * radius);
                return area;
            }
        }
    }

   


}
