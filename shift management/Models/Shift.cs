namespace shift_management.Models
{
    public class Shift
    {
        public int ShiftId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string Task { get; set; }
        public decimal Rate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<InvitationLog> InvitationLogs { get; set; }
        public ICollection<Worker> Worker { get; set; }

        public string ConfirmationLink {get; set;}

    }
}
