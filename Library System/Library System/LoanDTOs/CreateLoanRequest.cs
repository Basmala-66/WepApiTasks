namespace Library_System.LoanDTOs
{
    public class CreateLoanRequest
    {
        public int BookId { get; set; }
        public int BorrowsId { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
