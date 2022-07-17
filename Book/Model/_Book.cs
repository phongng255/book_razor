using System.ComponentModel.DataAnnotations;

namespace Book.Model
{
    public class _Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Không Được Để Trống !")]
        public string TenSach { get; set; }

        public int the_loaiId { get; set; }

        public int tacgiaId { get; set; }

        public string file_name { get; set; }

        public string img { get; set; }


        public virtual Tacgia Tacgia { get; set; }

        public virtual The_Loais The_Loai { get; set; }

       
    }
}
