using System.ComponentModel.DataAnnotations;

namespace StarterAPI.Models
{
    public class Sample
    {
        [Key]
        public int SampleID { get; set; }
        public string SampleData { get; set; }
    }
}
