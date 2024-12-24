
namespace Services
{
    internal class AuthenticationService(
        UserManager<ApplicationUser> userManager, 
        IOptions<JwtOptions> options
        )
        : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) throw new UnAuthorizedException($"Email {loginModel.Email} doesn't Exist.");

            var result = await userManager.CheckPasswordAsync(user, loginModel.Password);

            if (!result) throw new UnAuthorizedException();

            return new UserResultDTO(
              user.DisplayName,
              user.UserType,
              user.Email!,
              await CreateTokenAsync(user));

        }
        public async Task<UserResultDTO> RegisterAsync(RegisterDTO registerModel)
        {
            var user = new ApplicationUser
            {
                UserType = registerModel.UserType,
                Email = registerModel.Email,
                DisplayName = registerModel.DisplayName,
                UserName = registerModel.UserName,
                PhoneNumber = registerModel.PhoneNumber,
                Country = registerModel.Country,
                City = registerModel.City
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                throw new ValidationException(errors);
            }

            return new UserResultDTO(
             user.DisplayName,
             user.UserType,
             user.Email!,
             await CreateTokenAsync(user));
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwtOptions = options.Value;

            // Create Claims 
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim(ClaimTypes.UserData, user.UserType)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: authClaims,
                signingCredentials: creds,
                audience: jwtOptions.Audience,
                issuer: jwtOptions.Issure,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
