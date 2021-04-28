namespace SpaceWeb.EfStuff.Model
{
    public class OrderList : BaseModel
    {
        public virtual Order Order { get; set; }
        public virtual Employe Employe { get; set; }
    }
}