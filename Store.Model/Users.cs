namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        public Users()
        {
            Employees = new HashSet<Employees>();
        }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        [StringLength(50)]
        public string login { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
