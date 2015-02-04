namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoices
    {
        public Invoices()
        {
            HistoryOfInvoice = new HashSet<HistoryOfInvoice>();
        }

        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int invoiceId { get; set; }

        [Required]
        [StringLength(50)]
        public string type { get; set; }

        [Required]
        [StringLength(50)]
        public string from { get; set; }

        [Required]
        [StringLength(50)]
        public string to { get; set; }

        [Required]
        [StringLength(50)]
        public string nameOfCreator { get; set; }

        public int articleId { get; set; }

        [Required]
        [StringLength(50)]
        public string articleName { get; set; }

        public int articleAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string dataCreated { get; set; }

        [Required]
        [StringLength(50)]
        public string done { get; set; }

        [StringLength(200)]
        public string text { get; set; }

        public virtual ICollection<HistoryOfInvoice> HistoryOfInvoice { get; set; }
    }
}
