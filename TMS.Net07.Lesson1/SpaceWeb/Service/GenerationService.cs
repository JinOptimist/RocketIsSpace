using SpaceWeb.EfStuff.Model;
using SpaceWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWeb.Service
{
    public class GenerationService : IGenerationService
    {
        public string GenerateAccountNumber(int accountNumberLength = 10)
        {
            StringBuilder sb = new StringBuilder();

            Random rnd = new Random();

            for (int i = 0; i < accountNumberLength; i++)
            {
                sb.Append(rnd.Next(0, 9));
            }

            return sb.ToString();
        }
    }
}
