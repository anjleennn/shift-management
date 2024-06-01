namespace shift_management.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<InvitationLog> InvitationLogs { get; set; }
    }
}
