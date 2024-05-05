namespace ModelSharingService.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public bool Result { get; set; }
        public List<string>? Errors { get; set; }
    }
}
