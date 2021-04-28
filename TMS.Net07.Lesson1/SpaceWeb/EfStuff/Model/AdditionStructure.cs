namespace SpaceWeb.EfStuff.Model
{
    public class AdditionStructure:BaseModel
    {
        public virtual Order Order { get; set; }
        public string Type { get; set; }
    }
}