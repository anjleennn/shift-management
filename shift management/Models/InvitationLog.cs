namespace shift_management.Models
{
    public class InvitationLog
    {
        public int LogId { get; set; } //primary key
        public int WorkerId { get; set; }
        public int ShiftId { get; set; }
        public DateTime SentDate { get; set; }
        public Worker Worker { get; set; }
        public Shift Shift { get; set; }
    }
}
