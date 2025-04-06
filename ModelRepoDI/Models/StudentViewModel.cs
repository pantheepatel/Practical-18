using System.ComponentModel.DataAnnotations;

namespace ViewModels.Models
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Course { get; set; }
        public int Age { get; set; }
    }
}
