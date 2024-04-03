namespace Ricetta.Data.Entities
{
    public class Inbox : Entity
    {
        public string UserId { get; set; }
        public Member? User { get; set; }

        public string ReceiverId { get; set; }
        public Member? Receiver { get; set; }

        public ICollection<Message>? Messages { get; set; }
    }
}
