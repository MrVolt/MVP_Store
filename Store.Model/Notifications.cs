namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notifications
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int notificationId { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [StringLength(50)]
        public string from { get; set; }

        [StringLength(50)]
        public string to { get; set; }

        [StringLength(50)]
        public string dateOfCreation { get; set; }

        [StringLength(50)]
        public string dateOfShowing { get; set; }

        [StringLength(50)]
        public string nameOfCreator { get; set; }

        [StringLength(150)]
        public string text { get; set; }
    }
}
