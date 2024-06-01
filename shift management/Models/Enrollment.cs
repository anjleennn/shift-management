namespace shift_management.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int WorkerId { get; set; }
        public int ShiftId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool Confirmation { get; set; }
        public Worker Worker { get; set; }
        public Shift Shift { get; set; }
    }
}
