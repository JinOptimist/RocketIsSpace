namespace SpaceWeb.EfStuff.Model
{
    public class ComfortStructure:BaseModel
    {
        public virtual Order Order { get; set; }
        public string Type { get; set; }
    }
}