namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        
        public Message() { }
    }
}
