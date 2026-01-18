using System.ComponentModel.DataAnnotations;

namespace RegionalTempleInfo.Models
{
    public class Information
    {
        [Key]
        public int sr { get; set; }
        public string Name_of_god { get; set; }
        public string name_of_mandir { get; set; }
        public string location { get; set; }
        public string stays { get; set; }
        public string exact_location { get; set; }
    }
}
