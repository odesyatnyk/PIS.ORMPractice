using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIS.ORMPractice.CodeFirst.Entities
{
    public class Department
    {
        [Key]
        [Required]
        public int IdDepartment { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }

        public ICollection<Position> Positions{ get; set; }
    }
}
