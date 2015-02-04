namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }

        [StringLength(50)]
        public string firstName { get; set; }

        [StringLength(50)]
        public string lastName { get; set; }

        [Column("e-mail")]
        [StringLength(50)]
        public string e_mail { get; set; }

        [StringLength(50)]
        public string telephone { get; set; }

        public int userId { get; set; }

        public virtual Users Users { get; set; }
    }
}
