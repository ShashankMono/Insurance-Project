using AutoMapper;
using Azure;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Claim = System.Security.Claims.Claim;

namespace Insurance_final_project.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IRepository<User> userRepo,
            IMapper mapper
            ,IConfiguration configure)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = configure;
        }
        public Guid AddUser(UserDto user)
        {
            User newUser = _userRepo.Add(_mapper.Map<UserDto,User>(user));
            return newUser.UserId;
        }

        public List<UserDto> GetUsers()
        {
            return _mapper.Map<List<User>, List<UserDto>>(_userRepo.GetAll().ToList());
        }

        public (string token,User userData) LogIn(UserDto user)
        {
            var existingUser = _userRepo.GetAll().FirstOrDefault(u=>u.Username == user.Username);
            if (existingUser == null) {
                throw new UserInvalidException("Invalid Username!");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(user.Password, existingUser.HashedPassword))
            {
                throw new PasswordInvalidException("Invalid Password!");
            }
            var token = CreateToken(existingUser);
            return (token,existingUser);
        }

        private string CreateToken(User user)
        {
            var role = user.Role.RoleName;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,role),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //token construction
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(5),
                signingCredentials: cred
                );
            //generate the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public bool UpdateUser(UserDto user)
        {
            if (_userRepo.Update(_mapper.Map<UserDto, User>(user)) == null)
            {
                throw new UserInvalidException("Invalid User!");
            }   
            return true;
        }

        public UserDto GetUserById(Guid id)
        {
            return _mapper.Map<User, UserDto>(_userRepo.Get(id));
        }

        public bool ChangePassword(ChangePasswordDto changePassword)
        {
            var existingUser = _userRepo.GetAll().FirstOrDefault(u=>u.Username==changePassword.Username);
            if (existingUser == null) {
                throw new UserInvalidException("Invalid User!");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(changePassword.OldPassword, existingUser.HashedPassword))
            {
                throw new PasswordInvalidException("Invalid Old password!");
            }
            existingUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(changePassword.NewPassword);
            var response = _userRepo.Update(existingUser);
            if (response == null)
                return false;
            return true;
        }

        public bool DeactivateUser(ChangeUserStatusDto changeStatus)
        {
            var existingUser = _userRepo.Get(changeStatus.UserId);
            existingUser.IsActive = changeStatus.IsActive;
            var updateStatus = _userRepo.Update(existingUser);
            if(updateStatus == null) return false;
            return true;
        }
    }
}
