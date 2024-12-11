namespace MusicalShop.Services
{
    using MusicalShop.Services.Models.Extarnal;
    using System.Threading.Tasks;

    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken);
        Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken);
    }
}