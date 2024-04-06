using System.ComponentModel.DataAnnotations;

namespace Ricetta.Data.Entities
{
    public class Message : Entity
    {
        public int InboxId { get; set; }
        public Inbox? Inbox { get; set; }
        [MaxLength(200)]
        public string Content { get; set; }
        public string SenderId { get; set; }
        public Member? Sender { get; set; }
    }
}