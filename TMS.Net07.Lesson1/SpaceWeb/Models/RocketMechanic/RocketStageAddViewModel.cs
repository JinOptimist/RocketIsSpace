using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public class RocketStageAddViewModel
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please, enter rocket stage model name.")]
        public string RocketStageModel { get; set; }
        public double Weight { get; set; }
        public string EnginesModel { get; set; }
        public string FuelTanksModel { get; set; }
        public string RocketStageDescription { get; set; }
    }
}
