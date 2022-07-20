using System.ComponentModel.DataAnnotations;

namespace Training.Models
{
    public partial class SelectAll_Result
    {
        [Key]
        public virtual int IdNv { get; set; }

        public virtual string NameDepartment { get; set; }

        public virtual string Name { get; set; }

        public virtual string Position { get; set; }
    }

}
