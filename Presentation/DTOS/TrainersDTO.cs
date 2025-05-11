using System.ComponentModel.DataAnnotations;

namespace Presentation.DTOS
{
    public class TrainersDTO
    {
        public string? FullName { get; set; }
        public string? Specialty { get; set; }
        public string Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public int Salary { get; set; }
    }
}
