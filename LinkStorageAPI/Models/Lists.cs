using System.ComponentModel.DataAnnotations;

namespace LinkStorageAPI.Models
{
    public class Lists
    {
        [Key]
        public int ListId { get; set; }
        public int UserId { get; set; }
        public string ListName { get; set; }
    }
}
