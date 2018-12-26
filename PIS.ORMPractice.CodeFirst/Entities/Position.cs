using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.ORMPractice.CodeFirst.Entities
{
    public class Position
    {
        [Key]
        [Required]
        public int IdPosition { get; set; }

        [Required]
        public string PositionName { get; set; }

        public string PositionDescription { get; set; }

        [Required]
        public int IdDepartment { get; set; }

        public Department Department { get; set; }
    }
}
