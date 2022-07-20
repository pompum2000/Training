using System.ComponentModel.DataAnnotations;

namespace Training.Models
{
    public partial class Department
    {
        [Key]
        public virtual int IdDepartment { get; set; }
        public virtual string NameDepartment { get; set; }
        public virtual IList<NhanVien> NhanVien { get; set; }
    }

}
