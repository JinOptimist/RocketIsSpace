namespace SpaceWeb.EfStuff.Model
{
    public class OrdersEmployes : BaseModel
    {
        public virtual Order Order { get; set; }
        public virtual Employe Employe { get; set; }
    }
}