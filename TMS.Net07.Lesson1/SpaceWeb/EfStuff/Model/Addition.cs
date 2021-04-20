using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceWeb.EfStuff.Model
{
    public class Addition : BaseModel
    {
        public int RestRoomCount { get; set; }

        public int RescueCapsuleCount { get; set; }

        public int ObservarionDeckCount { get; set; }

        public int BotanicalCenterCount { get; set; }
    }
}
