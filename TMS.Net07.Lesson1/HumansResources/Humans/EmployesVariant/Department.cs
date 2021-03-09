using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HumansResources.Humans.EmployesVariant
{
    public class Department
    {
        public enum Type { 
            Manufactory,
            Laboratory,
            MissionControlCenter,
            SpacecraftCrew 
        };
        
        public int MaximumCountEmployes { get; set; } = 1;       
        public List<Employe> listEmployes = new List<Employe>();       
        public Type DepartmentType { get; }

        public Department() { 
        }

        public Department(Type departmentType)
        {
            DepartmentType = departmentType;
        }

        public Department(Type departmentType, int maximumCountEmploes)
        {
            DepartmentType = departmentType;
            if (maximumCountEmploes > 0)
            {
                MaximumCountEmployes = maximumCountEmploes;
            }            
        }        
        public bool SetEmploye(Employe employe) {
            if (listEmployes.Count() >= MaximumCountEmployes ||
                employe.SpecificationEmploye == Employe.Specification.Unknown ||
                (DepartmentType == Type.SpacecraftCrew &&
                employe.SpecificationEmploye != Employe.Specification.Spaceman))
            {
                return false;
            }
            listEmployes.Add(employe);
            return true;
        }
       
        public int GetCountEmployes(Employe.Specification specification) {                        
            return listEmployes.Where(e => e.SpecificationEmploye == specification).Count();
        }        
    }
}