using System.ComponentModel.DataAnnotations;

namespace CRUDApplication.Model
{
    public class Employee
    {

        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public int salary { get; set; }
        [Required]
        public string city { get; set; }
    }
}
