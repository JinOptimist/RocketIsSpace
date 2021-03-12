namespace HumansResources.Humans.Employes
{
    public interface IEmploye
    {
        decimal SalaryPerHour { get; set; }
        Specification SpecificationType { get; set; }
    }
}