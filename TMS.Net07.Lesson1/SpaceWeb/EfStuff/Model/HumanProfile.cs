
namespace SpaceWeb.EfStuff.Model
{
    public class HumanProfile : BaseModel
    {

        public virtual Client Client { get; set; }
        public virtual Employe Employe { get; set; }
        public long ForeignKeyUser { get; set; }
        public virtual User User { get; set; }
    }
}
