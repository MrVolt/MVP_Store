namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoryOfInvoice")]
    public partial class HistoryOfInvoice
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int historyId { get; set; }

        [Required]
        [StringLength(50)]
        public string typeChanged { get; set; }

        [Required]
        [StringLength(50)]
        public string fromChanged { get; set; }

        [Required]
        [StringLength(50)]
        public string toChanged { get; set; }

        [Required]
        [StringLength(50)]
        public string dataChanged { get; set; }

        [Required]
        [StringLength(50)]
        public string nameOfChanger { get; set; }

        public int articleIdChanged { get; set; }

        [Required]
        [StringLength(50)]
        public string articleNameChanged { get; set; }

        public int articleAmountChanged { get; set; }

        public int invoiceId { get; set; }

        public virtual Invoices Invoices { get; set; }
    }
}
