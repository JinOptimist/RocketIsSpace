namespace SpaceWeb.EfStuff.Model
{
    public class ComfortStructureDBmodel:BaseModel
    {
        public virtual Order Order { get; set; }
        public string Type { get; set; }
    }
}