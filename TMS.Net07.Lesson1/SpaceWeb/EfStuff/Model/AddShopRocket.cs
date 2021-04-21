namespace SpaceWeb.EfStuff.Model
{
    public class AddShopRocket : BaseModel
    {
        public string Name { get; set; }
        
        public double Cost { get; set; }
        
        public string ImageUrl { get; set; }
        
        public int Count { get; set; }
    }
}