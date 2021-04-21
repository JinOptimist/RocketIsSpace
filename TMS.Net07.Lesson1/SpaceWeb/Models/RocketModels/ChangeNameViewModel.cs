using System.ComponentModel.DataAnnotations;

namespace SpaceWeb.Models.RocketModels
{
    public class ChangeNameViewModel
    {
        public long Id { get; set; }
        public string OldName { get; set; }

        [Required]
        public string NewName { get; set; }
    }
}