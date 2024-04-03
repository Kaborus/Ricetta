using System.ComponentModel.DataAnnotations;

namespace Ricetta.Data.Entities
{
    public class Message : Entity
    {
        [MaxLength(200)]
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public string SenderId { get; set; }
        public Member? Sender { get; set; }
        public string RecipientId { get; set; }
        public Member? Recipient { get; set; }
    }
}
