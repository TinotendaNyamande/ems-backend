using EMS.Application.Dtos.Auth;
using EMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Cryptography;
using System.Text;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService,ILogger<AuthController> logger) : ControllerBase
    {
        private const string RefreshTokenCookieName = "refreshToken";
        private const string CsrfTokenCookieName = "csrfToken";
        private const string CsrfTokenHeaderName = "X-CSRF-Token";

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto request)
        {
            logger.LogInformation("Register endpoint called for {Email}", request.Email);

            var response = await authService.RegisterAsync(request);
            SetRefreshTokenCookie(response.RefreshToken);
            SetCsrfTokenCookie();
            logger.LogInformation("Register endpoint completed for {Email} ({UserId})", response.Email, response.UserId);
            return Ok(response);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            logger.LogInformation("Login endpoint called for {Email}", request.Email);

            var response = await authService.LoginAsync(request);
            SetRefreshTokenCookie(response.RefreshToken);
            SetCsrfTokenCookie();
            logger.LogInformation("Login endpoint completed for {Email} ({UserId})", response.Email, response.UserId);
            return Ok(response);

        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto? request)
        {
            logger.LogInformation("Refresh endpoint called");

            if (Request.Cookies.ContainsKey(RefreshTokenCookieName) && !IsCsrfTokenValid())
            {
                logger.LogWarning("Refresh endpoint rejected request because CSRF token was invalid");
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Invalid CSRF token." });
            }

            var refreshToken = request?.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshToken) && Request.Cookies.TryGetValue(RefreshTokenCookieName, out var cookieToken))
            {
                logger.LogInformation("Refresh endpoint using refresh token from cookie");
                refreshToken = cookieToken;
            }

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                logger.LogWarning("Refresh endpoint rejected request because refresh token was missing");
                return BadRequest(new { message = "Refresh token is missing." });
            }

            var result = await authService.RefreshTokenAsync(refreshToken);
            SetRefreshTokenCookie(result.RefreshToken);
            SetCsrfTokenCookie();
            logger.LogInformation("Refresh endpoint completed for {Email} ({UserId})", result.Email, result.UserId);
            return Ok(result);
        }


        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto? request)
        {
            logger.LogInformation("Logout endpoint called");

            var isCsrfValid = !Request.Cookies.ContainsKey(RefreshTokenCookieName) || IsCsrfTokenValid();
            if (!isCsrfValid)
            {
                logger.LogWarning("Logout endpoint received invalid CSRF token; cookies will still be cleared");
            }

            var refreshToken = request?.RefreshToken;
            if (string.IsNullOrWhiteSpace(refreshToken) && Request.Cookies.TryGetValue(RefreshTokenCookieName, out var cookieToken))
            {
                logger.LogInformation("Logout endpoint using refresh token from cookie");
                refreshToken = cookieToken;
            }

            if (isCsrfValid && !string.IsNullOrWhiteSpace(refreshToken))
            {
                await authService.LogoutAsync(refreshToken);
            }
            else if (string.IsNullOrWhiteSpace(refreshToken))
            {
                logger.LogInformation("Logout endpoint did not receive a refresh token; clearing cookies only");
            }

            DeleteRefreshTokenCookie();
            logger.LogInformation("Logout endpoint completed");
            return NoContent();

        }

        private void SetRefreshTokenCookie(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                logger.LogWarning("Refresh token cookie was not set because token was empty");
                return;
            }

            var cookieOptions = BuildRefreshCookieOptions(DateTimeOffset.UtcNow.AddDays(30));

            Response.Cookies.Append(RefreshTokenCookieName, refreshToken, cookieOptions);
            logger.LogInformation("Refresh token cookie set; secure={Secure}, sameSite={SameSite}, domain={Domain}", cookieOptions.Secure, cookieOptions.SameSite, cookieOptions.Domain);
        }

        private void DeleteRefreshTokenCookie()
        {
            var cookieOptions = BuildRefreshCookieOptions(DateTimeOffset.UtcNow.AddDays(-1));
            Response.Cookies.Append(RefreshTokenCookieName, string.Empty, cookieOptions);
            DeleteCsrfTokenCookie();
            logger.LogInformation("Refresh token cookie deleted");
        }

        private void SetCsrfTokenCookie()
        {
            var csrfToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var cookieOptions = BuildCsrfCookieOptions(DateTimeOffset.UtcNow.AddDays(30));
            Response.Cookies.Append(CsrfTokenCookieName, csrfToken, cookieOptions);
            logger.LogInformation("CSRF token cookie set; secure={Secure}, sameSite={SameSite}, domain={Domain}", cookieOptions.Secure, cookieOptions.SameSite, cookieOptions.Domain);
        }

        private void DeleteCsrfTokenCookie()
        {
            var cookieOptions = BuildCsrfCookieOptions(DateTimeOffset.UtcNow.AddDays(-1));
            Response.Cookies.Append(CsrfTokenCookieName, string.Empty, cookieOptions);
            logger.LogInformation("CSRF token cookie deleted");
        }

        private bool IsCsrfTokenValid()
        {
            if (!Request.Cookies.TryGetValue(CsrfTokenCookieName, out var csrfCookie) || string.IsNullOrWhiteSpace(csrfCookie))
            {
                return false;
            }

            if (!Request.Headers.TryGetValue(CsrfTokenHeaderName, out var csrfHeader) || string.IsNullOrWhiteSpace(csrfHeader))
            {
                return false;
            }

            var cookieBytes = Encoding.UTF8.GetBytes(csrfCookie);
            var headerBytes = Encoding.UTF8.GetBytes(csrfHeader.ToString());

            if (cookieBytes.Length != headerBytes.Length)
            {
                return false;
            }

            return CryptographicOperations.FixedTimeEquals(cookieBytes, headerBytes);
        }

        private CookieOptions BuildRefreshCookieOptions(DateTimeOffset expires)
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var sameSiteConfig = configuration.GetValue<string>("Auth:RefreshCookie:SameSite") ?? "Lax";
            var secureConfig = configuration.GetValue<bool?>("Auth:RefreshCookie:Secure");
            var cookieDomain = ResolveCookieDomain(configuration);

            var sameSite = sameSiteConfig.ToLowerInvariant() switch
            {
                "none" => SameSiteMode.None,
                "strict" => SameSiteMode.Strict,
                _ => SameSiteMode.Lax
            };

            return new CookieOptions
            {
                HttpOnly = true,
                Secure = secureConfig ?? Request.IsHttps,
                SameSite = sameSite,
                Expires = expires,
                Domain = cookieDomain,
                Path = "/"
            };
        }

        private CookieOptions BuildCsrfCookieOptions(DateTimeOffset expires)
        {
            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var sameSiteConfig = configuration.GetValue<string>("Auth:RefreshCookie:SameSite") ?? "Lax";
            var secureConfig = configuration.GetValue<bool?>("Auth:RefreshCookie:Secure");
            var cookieDomain = ResolveCookieDomain(configuration);

            var sameSite = sameSiteConfig.ToLowerInvariant() switch
            {
                "none" => SameSiteMode.None,
                "strict" => SameSiteMode.Strict,
                _ => SameSiteMode.Lax
            };

            return new CookieOptions
            {
                HttpOnly = false,
                Secure = secureConfig ?? Request.IsHttps,
                SameSite = sameSite,
                Expires = expires,
                Domain = cookieDomain,
                Path = "/"
            };
        }

        private string? ResolveCookieDomain(IConfiguration configuration)
        {
            var configuredDomain = configuration.GetValue<string>("Auth:RefreshCookie:Domain");
            if (!string.IsNullOrWhiteSpace(configuredDomain))
            {
                return configuredDomain.Trim();
            }

            var host = Request.Host.Host;
            if (string.IsNullOrWhiteSpace(host) ||
                host.Equals("localhost", StringComparison.OrdinalIgnoreCase) ||
                System.Net.IPAddress.TryParse(host, out _))
            {
                return null;
            }

            const string apiSubdomainPrefix = "pms-api.";
            if (!host.StartsWith(apiSubdomainPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            var parentDomain = host[apiSubdomainPrefix.Length..];
            return parentDomain.Contains('.') ? $".{parentDomain}" : null;
        }


    }
}
