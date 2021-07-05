using System;
using System.Collections.Generic;
using System.IO;
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
        delegate void Message();

        event Message Notify;

        static void Main(string[] args)
        {
            var olga = new Human(2000);
            var ivan = new Human(1998);

            var a = 50 + olga;
        }

        public static void MessageEmail()
        {
            Console.WriteLine("Email");
        }

        public static void MessagePhone()
        {
            Console.WriteLine("Phone");
        }

        public static int Sum(string name, int a, int b)
        {
            return 0;
        }

        public static int Sum(string name, params int[] numbers)
        {
            return 0;
        }

        public static void ExampleIndexer()
        {
            var invoice = new Invoice();

            //var firstDish = invoice.InvoiceRecords[0].Name;
            var firstDish = invoice[0].Name;
            var invoices = new Invoice[10];

            foreach (var record in invoice)
            {

            }
        }


        public static IEnumerable<MyNumber> GetNumbers()
        {
            for (int i = 0; i < 3; i++)
            {
                yield return new MyNumber();
            }
        }

        public static void ExampleLinq()
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

        public static async Task HashSetExmpleAsync()
        {
            var dictionary = new HashSet<Human>();

            dictionary.Add(new Human(201));

            dictionary.Add(new Human(202));

            dictionary.Add(new Human(203));

            var age = Fun();
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

    public class A
    {
        public virtual void Do() { }
    }

    public class B : A
    {
        public override void Do()
        {
        }

        public string Af()
        {
            return "Af";
        }
    }
}
