namespace Library_System.BorrowerDTOs
{
    public class GetBorrowerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public DateTime MembershipDate { get; set; }
    }
}
