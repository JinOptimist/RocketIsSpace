using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ReflectionLessone
{
    public class MyNumber
    {
        public static int StaticNumber = 0;
        public int Number
        {
            get
            {
                Console.WriteLine($"read number {++StaticNumber}");
                return StaticNumber;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var myNumbers = GetNumbers();
            var numbers = myNumbers
                .Select(x => x.Number);
            
            var sum = numbers.Sum();
            Console.WriteLine($"sum {sum}");

            var count = numbers.Count();
            Console.WriteLine($"count {count}");

            var sum2 = numbers.Sum();
            Console.WriteLine($"sum2 {sum2}");
        }

        public static IEnumerable<MyNumber> GetNumbers()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new MyNumber();
            }
        }

        public static void TryCatchExnple()
        {
            try
            {
                Dived();
            }
            catch (MyDiveByZeroException e)
            {
                Console.WriteLine("MyDiveByZeroException");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("DivideByZeroException");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }

        public static int Dived()
        {
            var result = 0;
            try
            {
                BasicDive();//DivideByZeroException
            }
            catch (MyDiveByZeroException e)
            {
                Console.WriteLine("MyDiveByZeroException");
            }
            catch (DivideByZeroException e)
            {
                //!
                Console.WriteLine("DivideByZeroException");
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }

            return result;
        }

        public static int BasicDive()
        {
            var b = 0;
            return 1 / b;
        }

        public static void HashSetExmple()
        {
            var dictionary = new HashSet<Human>();

            dictionary.Add(new Human(201));

            dictionary.Add(new Human(202));

            dictionary.Add(new Human(203));

            var age =  Fun();
        }

        public static Task<int> Fun()
        {
            Thread.Sleep(10 * 1000);
            return new Task<int>(() => 8);
        }

        public static void Refl()
        {
            var kate = new Human(2000);

            var kateViewMpdel = Map<Human, HumanViewModel>(kate);

            Console.WriteLine(kate.Age);

            var type = kate.GetType();
            foreach (var fieldInfo in type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
            {
                if (fieldInfo.Name == "_yearOfBirthday")
                {
                    fieldInfo.SetValue(kate, 2010);
                }
            }

            Console.WriteLine(kate.Age);
        }

        public static OutType Map<InType, OutType>(InType inputObject)
            where OutType : new()
            where InType : class
        {
            var outType = typeof(OutType);
            var constructorWithoutParam = outType.GetConstructor(new Type[0]);
            var answer = (OutType)constructorWithoutParam.Invoke(new object[0]);

            var inputProperties = typeof(InType).GetProperties();
            foreach (var outPropertyInfo in outType.GetProperties())
            {
                foreach (var inPropertyInfo in inputProperties)
                {
                    if (inPropertyInfo.Name == outPropertyInfo.Name)
                    {
                        var propValue = inPropertyInfo.GetValue(inputObject);
                        outPropertyInfo.SetValue(answer, propValue);
                    }
                }
            }

            return answer;
        }
    }

    public class MyDiveByZeroException : DivideByZeroException
    {

    }
}
