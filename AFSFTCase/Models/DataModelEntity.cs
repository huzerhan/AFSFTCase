using System.ComponentModel.DataAnnotations;

namespace AFSFTCase.Models
{
    public class DataModelEntity
    {

        
        [ScaffoldColumn(false)]
        [Key]
        public int TextId { get; set; }

        [Display(Name = "Input")]
        [Required(ErrorMessage = "Enter a proper input between 3-500 characters")]
        [StringLength(500, MinimumLength = 3)]
        public string InputString { get; set; }

        [Display(Name = "Result")]
        public string? TranslatedString { get; set; }

        [Display(Name = "Translation Time")]
        public DateTime TranslationTime { get; set; } = DateTime.Now;
        [Display(Name = "Translator")]
        [Required]
        public string Translator { get; set; }
    }
}
