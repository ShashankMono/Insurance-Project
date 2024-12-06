using AutoMapper;
using Azure;
using Insurance_final_project.Dto;
using Insurance_final_project.Exceptions;
using Insurance_final_project.Models;
using Insurance_final_project.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
        private readonly IRepository<Role> _roleRepo;
        public UserService(IRepository<User> userRepo,
            IMapper mapper
            ,IConfiguration configure
            , IRepository<Role> roleRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = configure;
            _roleRepo = roleRepo;
        }
        public async Task<Guid> AddUser(UserDto user)
        {
            var existingUser = _userRepo.GetAll().AsNoTracking().FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                throw new UsernameAlreadyUsedEException("Username Exist!");
            }
            User newUser = _userRepo.Add(_mapper.Map<UserDto,User>(user));
            return newUser.UserId;
        }
        public UserDto AddNewUser(Guid roleId)
        {
            var newUsername = Guid.NewGuid().ToString();
            Random random = new Random();
            var password = random.Next(100001, 1000000).ToString();
            UserDto user = new UserDto()
            {
                Username = newUsername,
                Password = password,
                RoleId = roleId,
            };
            User userAgent = _userRepo.Add(_mapper.Map<UserDto, User>(user));
            user.UserId = userAgent.UserId;
            return user;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return _mapper.Map<List<User>, List<UserDto>>(_userRepo.GetAll().ToList());
        }

        public async Task<(string token,UserLogInResponseDto userData)> LogIn(UserLoginDto user)
        {
            var existingUser = _userRepo.GetAll().AsNoTracking().Include(u=>u.Role).FirstOrDefault(u => u.Username == user.Username);
            if (existingUser == null) {
                throw new UserInvalidException("Invalid Username!");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(user.Password, existingUser.Password))
            {
                throw new PasswordInvalidException("Invalid Password!");
            }
            var token = CreateToken(existingUser);
            return (token,_mapper.Map<UserLogInResponseDto>(existingUser));
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

        public async Task<bool> UpdateUsername(UserUpdateDto userUpdate)
        {
            var existingUser = _userRepo.GetAll().AsNoTracking().FirstOrDefault(u => u.UserId == userUpdate.UserId);
            if ( existingUser == null)
            {
                throw new UserInvalidException("Invalid User!");
            }
            var checkUsername = _userRepo.GetAll().AsNoTracking().FirstOrDefault(u => u.Username == userUpdate.Username);
            if (checkUsername != null)
            {
                throw new UsernameAlreadyUsedEException("Username Exist!");
            }
            existingUser.Username = userUpdate.Username;
            _userRepo.Update(existingUser);
            return true;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            return _mapper.Map<User, UserDto>(_userRepo.Get(id));
        }

        public async Task<bool> ChangePassword(ChangePasswordDto changePassword)
        {
            var existingUser = _userRepo.GetAll().AsNoTracking().FirstOrDefault(u=>u.Username==changePassword.Username);
            if (existingUser == null) {
                throw new UserInvalidException("Invalid User!");
            }
            if (!BCrypt.Net.BCrypt.EnhancedVerify(changePassword.OldPassword, existingUser.Password))
            {
                throw new PasswordInvalidException("Invalid Old password!");
            }
            existingUser.Password = BCrypt.Net.BCrypt.EnhancedHashPassword(changePassword.NewPassword);
            var response = _userRepo.Update(existingUser);
            if (response == null)
                return false;
            return true;
        }

        public async Task<bool> DeactivateUser(ChangeUserStatusDto changeStatus)
        {
            var existingUser = _userRepo.GetAll().AsNoTracking().FirstOrDefault(u=>u.UserId == changeStatus.UserId);
            if(existingUser == null)
            {
                throw new UserInvalidException("User not found!");
            }
            existingUser.IsActive = changeStatus.IsActive;
            var updateStatus = _userRepo.Update(existingUser);
            if(updateStatus == null) return false;
            return true;
        }

        public async Task<List<UserDto>> GetUsersByRole(Guid RoleId)
        {
            if(_roleRepo.Get(RoleId) == null)
            {
                throw new InvalidGuidException("Role not found!");
            }
            return _mapper.Map<List<User>, List<UserDto>>(_userRepo.GetAll().Where(u=>u.RoleId == RoleId).ToList());
        }
    }
}
