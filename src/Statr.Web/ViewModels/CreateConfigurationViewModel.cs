using System.ComponentModel.DataAnnotations;

namespace Statr.Web.ViewModels
{
    public class CreateConfigurationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Pattern { get; set; }
    }
}