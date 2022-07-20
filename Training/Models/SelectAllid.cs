using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Training
{
    public partial class SelectAllid
    {


        [Key]
        public virtual int IdNv { get; set; }

        public virtual string Name { get; set; }

        public virtual string Position { get; set; }

        public virtual string NameDepartment { get; set; }

     
    }

}
