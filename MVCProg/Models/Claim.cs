namespace MVCprog.Models
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int LecturerId { get; set; }
        public string Description { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalAmount => HoursWorked * HourlyRate;
        public DateTime DateSubmitted { get; set; }
        public string Status { get; set; }
        public List<Document>? Documents { get; set; }
    }
}
