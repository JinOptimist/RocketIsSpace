using System;
using System.Reflection;

namespace ReflectionLessone
{
    class Program
    {
        static void Main(string[] args)
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
}
