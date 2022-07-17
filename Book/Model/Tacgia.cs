using System.ComponentModel.DataAnnotations;

namespace Book.Model
{
    public class Tacgia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tacgia()
        {
            Books = new HashSet<_Book>();
        }

        public int id { get; set; }

        [Required]
        public string ten_tg { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_Book> Books { get; set; }
    }
}
