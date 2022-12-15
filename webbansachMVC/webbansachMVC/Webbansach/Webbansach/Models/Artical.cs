namespace Webbansach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Artical
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artical()
        {
            ArticlesComments = new HashSet<ArticlesComment>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public bool? Active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticlesComment> ArticlesComments { get; set; }
    }
}
