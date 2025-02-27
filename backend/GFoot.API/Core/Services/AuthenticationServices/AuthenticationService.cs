
using System.Net;

namespace Services.AuthenticationServices
{
    internal class AuthenticationService(
        UserManager<ApplicationUser> userManager,
        IUnitOfWork _unitOfWork,
        IOptions<JwtOptions> options,
        IEmailService emailService
        )
        : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            var user = await userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) throw new UnAuthorizedException($"Email {loginModel.Email} doesn't Exist.");

            if (!user.EmailConfirmed)
                throw new UnAuthorizedException("Email not confirmed. Please check your email.");

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

            // Assign a role based on UserType
            var role = registerModel.UserType.ToLower() switch
            {
                "individualuser" => "IndividualUserRole",
                "factoryuser" => "FactoryUserRole",
                "environmentalagent" => "EnvironmentalAgentRole",
                "admin" => "AdminRole",
                _ => throw new ArgumentException("Invalid UserType specified.")
            };

            // Add the user to the appropriate role
            var roleAssignmentResult = await userManager.AddToRoleAsync(user, role);
            if (!roleAssignmentResult.Succeeded)
            {
                var errors = roleAssignmentResult.Errors.Select(error => error.Description).ToList();
                throw new ValidationException(errors);
            }

            // Create corresponding entity based on UserType
            switch (registerModel.UserType.ToLower())
            {
                case "individualuser":
                    var individualRepo = _unitOfWork.GetRepository<IndividualUser, string>();
                    await individualRepo.AddAsync(new IndividualUser { ApplicationUserId = user.Id });
                    break;

                case "factoryuser":
                    var factoryRepo = _unitOfWork.GetRepository<FactoryUser, string>();
                    await factoryRepo.AddAsync(new FactoryUser
                    {
                        ApplicationUserId = user.Id,
                        IndustryType = registerModel.IndustryType
                    });
                    break;

                case "environmentalagent":
                    var agentRepo = _unitOfWork.GetRepository<EnvironmentalAgent, string>();
                    await agentRepo.AddAsync(new EnvironmentalAgent { ApplicationUserId = user.Id });
                    break;

                case "admin":
                    // No additional entity creation is needed for Admin users
                    break;

                default:
                    throw new ArgumentException("Invalid UserType specified.");
            }

            await _unitOfWork.SaveChangesAsync();

            // Generate email confirmation token
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebUtility.UrlEncode(token); // Ensure URL safe token
            var confirmationLink = $"https://yourfrontend.com/confirm-email?email={user.Email}&token={encodedToken}";

            var emailBody = $@"
            <h2>Confirm Your Email</h2>
            <p>Click the link below to confirm your email:</p>
            <a href='{confirmationLink}'>Confirm Email</a>
            <p>If you didn't request this, ignore this email.</p>";

            await emailService.SendEmailAsync(user.Email, "Confirm Your Email", emailBody);

            return new UserResultDTO(
             user.DisplayName,
             user.UserType,
             user.Email!,
             await CreateTokenAsync(user));
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return false;

            var result = await userManager.ConfirmEmailAsync(user, token);
            return result.Succeeded;
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var jwtOptions = options.Value;

            // Create Claims 
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.UserName!),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.UserData, user.UserType),
                new Claim(ClaimTypes.NameIdentifier , user.Id),
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

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequestDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;  // Email doesn't exist

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var resetUrl = $"https://localhost:5001/api/Authentication/Reset-Password?email={dto.Email}&token={token}";
            // var resetUrl = $"{_config["AppSettings:FrontendUrl"]}/Reset-Password?email={email}&token={token}";


            var emailBody = $@"
            <h2>Password Reset Request</h2>
            <p>Click the link below to reset your password:</p>
            <a href='{resetUrl}'>Reset Password</a>
            <p>If you didn't request this, ignore this email.</p>";

            return await emailService.SendEmailAsync(dto.Email, "Reset Your Password", emailBody);
        }
        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null) return false;  // Email doesn't exist

            // Validate new password strength
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var result = await passwordValidator.ValidateAsync(userManager, user, dto.NewPassword);
            if (!result.Succeeded) return false;  // Password is not strong enough

            var resetResult = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
            return resetResult.Succeeded;
        }
    }
}
