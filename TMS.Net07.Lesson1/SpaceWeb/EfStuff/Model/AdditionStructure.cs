namespace SpaceWeb.EfStuff.Model
{
    public class AdditionStructureDBmodel:BaseModel
    {
        public virtual Order Order { get; set; }
        public string Type { get; set; }
    }
}