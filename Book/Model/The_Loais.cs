using System.ComponentModel.DataAnnotations;

namespace Book.Model
{
    public class The_Loais
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public The_Loais()
        {
            Books = new HashSet<_Book>();
        }
        public int id { get; set; }
        [Required]
        public string ten_the_loai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<_Book> Books { get; set; }
    }
}
