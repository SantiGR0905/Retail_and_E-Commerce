namespace Retail.Model
{
    public class UserHistories
    {
        public int UserHistoryId { get; set; }
        public int UserId { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Date { get; set; }
        public required string UserTypes { get; set; }
        public required string Modified { get; set; }
        public required string ModifiedBy { get; set; }

    }
}
