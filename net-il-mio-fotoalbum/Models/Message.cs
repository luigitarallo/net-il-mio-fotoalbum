using System.ComponentModel.DataAnnotations;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        [Key]public int MessageId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        
        public Message() { }
    }
}
