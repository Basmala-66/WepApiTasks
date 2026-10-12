namespace Library_System.LoanDTOs
{
    public class UpdateLoanRequest
    {
        public int BookId { get; set; }
        public int BorrowsId { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
