namespace Webbansach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ArticlesComment
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime? CommentOn { get; set; }

        public int? ArticalId { get; set; }

        public int? Rating { get; set; }

        public virtual Artical Artical { get; set; }
    }
}
