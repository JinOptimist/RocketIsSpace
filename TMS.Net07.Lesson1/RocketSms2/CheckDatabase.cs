using SpaceWeb.EfStuff.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocketSms2
{
    public class CheckDatabase
    {
        private InsuranceTypeRepository _insuranceTypeRepository;
        private InsuranceRepository _insuranceRepository;

        public CheckDatabase() { }

        /*public CheckDatabase(InsuranceTypeRepository insuranceTypeRepository, InsuranceRepository insuranceRepository)
        {
            _insuranceTypeRepository = insuranceTypeRepository;
            _insuranceRepository = insuranceRepository;
        }*/

        public void CheckDb(InsuranceTypeRepository _insuranceTypeRepository, InsuranceRepository _insuranceRepository)
        {
            var models = _insuranceRepository
                .GetAll()
                .ToList();
            Console.WriteLine();
        }
    }
}
