using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BeaconBookingApi.Options;
using BeaconBookingApi.ViewModels;
using BeaconBookingApi.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BeaconBookingApi.Controllers
{
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        //private readonly JsonSerializerSettings _serializerSettings;
        private readonly IUserRepository _userRepository;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory, IUserRepository userRepository)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _userRepository = userRepository;
            _logger = loggerFactory.CreateLogger<JwtController>();
            //_serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromBody]RequestViewModel model)
        {
            var identity = await GetClaimsIdentity(model);

            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({model.UserName}) or password ({model.Password})");
                return BadRequest("Felaktigt användarnamn eller lösenord");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                //identity.FindFirst("SigmaEmployee")
            };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            //var response = new
            //{
            //    access_token = encodedJwt,
            //    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            //};

            var authenticatedUserViewModel = new AuthenticatedUserViewModel
            {
                UserName = model.UserName,
                AccessToken = encodedJwt,
                ExpiresIn = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            //return new OkObjectResult(json);
            return new OkObjectResult(authenticatedUserViewModel);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        private Task<ClaimsIdentity> GetClaimsIdentity(RequestViewModel model)
        {
            var user = _userRepository.GetUserByUsername(model.UserName);

            if (user != null)
            {
                var result = _userRepository.CheckUserPassword(user, model.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return Task.FromResult(new ClaimsIdentity(new GenericIdentity(model.UserName, "Token"), new[] 
                    {
                        new Claim("SigmaEmployee", "Value")
                    }));
                }
            }

            return Task.FromResult<ClaimsIdentity>(null);
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
