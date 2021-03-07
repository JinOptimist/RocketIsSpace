using System;
using System.Collections.Generic;
using System.Text;

namespace HumansResources.Humans.Employes
{
    public class DepartmentVariant
    {
        public enum Department { 
            Manufactory,
            Laboratory,
            MissionControlCenter,
            SpacecraftCrew 
        };

        /*Например, ограничиваем количество работников, которые могут работать в этом департамете
        (по умолчанию оставляем 1)*/
        public int MaximumCountEmployes { get; set; } = 1;
        //Список работников департамента
        public List<EmployeVariant> listEmployes = new List<EmployeVariant>();
        //Запрещаем изменять тип департамента, его можно указать только через конструктор при создании
        public Department DepartmentType { get; }

        public DepartmentVariant() { 
        }

        public DepartmentVariant(DepartmentVariant.Department department)
        {
            DepartmentType = department;
        }

        public DepartmentVariant(DepartmentVariant.Department department, int maximumCountEmploes)
        {
            DepartmentType = department;
            if (maximumCountEmploes > 0)
            {
                MaximumCountEmployes = maximumCountEmploes;
            }            
        }
        
        /*Здесь можно создать какие-то условия, я придумал что нельзя принять в отдел
        работника, если штат уже заполнен, а также нельзя принять работника с неизвестной 
        специальностью и если это не космонавт его нельзя включить в состав экипажа. 
        Простор для творчества! Как вариант можно сделать - дополнительно формировать штат
        с учетом специальности. Например, на производстве могут работать только 1 руководитель,
        3 инженера и 5 техников. Сделать это через переменные, чтобы это количество и 
        набор специальностей можно было регулировать, а потом проверять если уже есть 
        руководитель, то добавить его в список работников нельзя и т.д. 
        Затем можно сделать unit-тест для этого метода.*/
        public bool SetEmploye(EmployeVariant employe) {
            if (listEmployes.Count >= MaximumCountEmployes ||
                employe.SpecificationEmploye == EmployeVariant.Specification.Unknown ||
                (DepartmentType == Department.SpacecraftCrew &&
                employe.SpecificationEmploye != EmployeVariant.Specification.Spaceman))
            {
                return false;
            }
            listEmployes.Add(employe);
            return true;
        }

       
        /*Этот метод должен возвращать количество работников со специальностью,
        которая передаётся через аргументы. Реализуй сам, используя LINQ*/
        public int GetCountEmployes(EmployeVariant.Specification specification) {
            return 0;
        }
        
        /*И последнее, если тебе нужно проверять как что будет работать или дебажить, то
         ты можешь временно писать в методе Main класса Program в нашем проекте и запускать
         его обычным способом. Приблизительно, например:

         EmployeVariant employe = new EmployeVariant (new Person());
         DepartmentVariant department = new Department ();
         bool ok = department.setEmploye(employe);
         Console.WriteLine(ok);

         Только перед этим нужно назначить проект исполняемым в контекстном меню правой кнопкой
         по проекту. Сейчас в том виде, который есть он не запустится (не соберется),
         поскольку в твоем коде есть ошибки. 
         После того как закончишь из Main свой код нужно будет удалить.
         Простые методы и поля, которые можно добавить - название отдела, метод получить в виде
         string список работников отдела.*/
    }
}