namespace DTO.AccountDtos
{
    public class AppUserRefreshTokenDto
    {
        public string Message { get; set; }
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
    }
}
