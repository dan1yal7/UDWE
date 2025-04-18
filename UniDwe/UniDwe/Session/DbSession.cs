namespace UniDwe.Session
{
    public class DbSession
    {
        public Guid SessionId { get; set; }
        public string? SessionData { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastAccessed { get; set; }
        public int? UserId { get; set; }
    }
}
