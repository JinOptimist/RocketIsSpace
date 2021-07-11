using SpaceWeb.Models;
using System.Collections.Generic;

namespace SpaceWeb.Presentation
{
    public interface IRocketMechanicPresentation
    {
        public List<RocketStageAddViewModel> GetIndexViewModel();
        public RocketStageAddViewModel GetRocketStageAddViewModel(long id = 0);
    }
}
