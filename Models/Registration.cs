using System.ComponentModel.DataAnnotations;

namespace RegionalTempleInfo.Models
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int phoneno { get; set; }
    }
}
