namespace SpaceWeb.EfStuff.Model
{
    public class RocketStage : BaseModel
    {
        public string ImageUrl { get; set; }
        public string RocketStageModel { get; set; }
        public double Weight { get; set; }
        public string EnginesModel { get; set; }
        public string FuelTanksModel { get; set; }
        public string RocketStageDescription { get; set; }
    }
}
