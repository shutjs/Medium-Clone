namespace blogv2.Models
{

    using System.ComponentModel.DataAnnotations;

    public class Kullanicilar
    {
        [Key]
        public int id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Pass { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? profileImage { get; set; }
        public int? MailCode { get; set; }
        public string? MailVerifDurum { get; set;}
        public string? About { get; set; }
    }
}
