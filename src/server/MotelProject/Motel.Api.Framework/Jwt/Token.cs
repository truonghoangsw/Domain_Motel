using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Motel.Api.Framework.Jwt
{
    public class Token
    {
        [JsonPropertyName("refreshToken")]
        [Required]
        public string RefreshToken { get; set; }
    }
}
