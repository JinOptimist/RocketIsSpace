using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models
{
    public class MainViewModel
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please, enter rocket stage model name.")]
        public string RocketStageModel { get; set; }
        public double Weight { get; set; }
        public string EnginesModel { get; set; }
        public string FuelTanksModel { get; set; }
        public string RocketStageDescription { get; set; }
    }
}
