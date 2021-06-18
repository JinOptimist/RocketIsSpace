namespace SpaceWeb.Service
{
    public interface ISalaryService
    {
        void CalculateAccrual();
        void PayAccrual();
        void GetAccrualInAMonth();
        void GetPayedAccrualInAMonth();
        void GetAllAccruals();
        void GetAllPayedAccruals();
    }
}