namespace SpaceWeb.Service
{
    public interface ISalaryService
    {
        void CalculateSalary();
        void PaySalary();
        void GetCalculatedSalaryInAMonth();
        void GetPayedSalaryInAMonth();
        void GetAllCalculatedSalary();
        void GetAllPayedSalary();
    }
}