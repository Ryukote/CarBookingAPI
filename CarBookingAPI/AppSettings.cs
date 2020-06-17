namespace CarBookingAPI
{
    public class AppSettings
    {
        public string SQLServerConnectionString { get; set; }
        public string JWTSecret { get; set; }
        public string HashSecret { get; set; }
    }
}
