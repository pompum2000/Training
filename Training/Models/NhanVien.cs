using System.ComponentModel.DataAnnotations;

namespace Training.Models
{
    public partial class NhanVien
    {
        [Key]
        public virtual int IdNv { get; set; }

        public virtual string Name { get; set; }

        public virtual string Position { get; set; }

        public virtual int IdDepartment { get; set; }

        public virtual Department Department { get; set; }
    }
}


