using System.ComponentModel.DataAnnotations;

namespace blogv2.Models
{
    public class Makaleler
    {
        public int id { get; set; } // Primary key

        public string? baslik { get; set; }
        public string? icerik { get; set; }
        public string? yazanKisi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM}", ApplyFormatInEditMode = true)]
        public DateTime yazilimTarih { get; set; }
        public string? okumaSure { get; set; }
        public string? icerikResim { get; set; }
        public int goruntulenme { get; set; }
        public string? kategori { get; set; }
    }
}
