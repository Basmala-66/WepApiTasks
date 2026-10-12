namespace Library_System.LoanDTOs
{
    public class GetLoanResponse
    {
        public int BookId { get; set; }
        public int BorrowsId { get; set; }

        public DateTime LoanDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
