using System.ComponentModel.DataAnnotations;

namespace LinkStorageAPI.Models
{
    public class Links
    {
        [Key]
        public int LinkId { get; set; }
        public int ListId { get; set; }
        public string LinkItem { get; set; }
    }
}
