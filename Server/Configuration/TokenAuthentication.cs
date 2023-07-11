namespace Server.Configuration
{
	public class TokenAuthentication
	{
		public string? Key { get; set; }
		public string? Issuer { get; set; }
		public string? Audience { get; set; }
		public int ExpirationInMinutes { get; set; }
		public string? CookieToken { get; set; }
		public string? CookieUsername { get; set; }
		public string? CookieRefresh { get; set; }
	}
}
