namespace Store.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Articles
    {
        [Key, DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int articleId { get; set; }

        [StringLength(50)]
        public string nameOfMaker { get; set; }

        [StringLength(50)]
        public string nameOfArticle { get; set; }

        public int amountAll { get; set; }

        public int amountBusy { get; set; }

        public int amountFree { get; set; }

        public int price { get; set; }

        public int waiting { get; set; }
    }
}
