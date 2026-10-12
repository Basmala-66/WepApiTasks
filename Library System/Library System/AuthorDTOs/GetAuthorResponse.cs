namespace Library_System.AuthorDTOs
{
    public class GetAuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateAuthorRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
