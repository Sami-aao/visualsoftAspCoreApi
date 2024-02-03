using VisualSoftAspCoreApi.Context;
using VisualSoftAspCoreApi.Contracts;
using VisualSoftAspCoreApi.Entities;
using VisualSoftAspCoreApi.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VisualSoftAspCoreApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DapperContext _context;
        private readonly string _jwtSecret;
        public UserRepository(DapperContext context, string jwtSecret)
         {
            _context = context;
            _jwtSecret = jwtSecret;
        }

        // Get All Users
        public async Task<IEnumerable<User>> GetUsers()
        {
           var query = "SELECT * FROM Users";
           using (var conn= _context.CreateConnection())
           {
            var users = await conn.QueryAsync<User>(query);
            return users.ToList();
           }
        }

        // Get User by Id
        public async Task<User> GetUser(int id)
        {
           var query = "SELECT * FROM Users WHERE Id = @Id";
           using (var conn= _context.CreateConnection())
           {
            var user = await conn.QueryAsync<User>(query, new {id});
            return user;
           }
        }

        // Create new User
        public async Task<User> CreateUser(UserCreationDto User)
        { 
            var query= " INSERT INTO Users (model, details,advancePayment, monthlyInstallment, financeDuration,imageURL) VALUES (@model,@details,@advancePayment,@monthlyInstallment, @financeDuration,@imageURL)"+
            "SELECT CAST(SCOPE_IDENTITY() AS int)";

public string email {get; set;}
        public string pass {get; set;}
        public string? address {get; set;}
        public int? tel {get; set;}
        public bool activated {get; set;}
        public string role {get; set;}

            var parameters = new DynamicParameters();
            parameters.Add("email", User.email, DbType.string);
            parameters.Add("pass", User.pass, DbType.string);
            parameters.Add("address", User.address, DbType.string);
            parameters.Add("tel", User.tel, DbType.int);
            parameters.Add("activated", User.activated, DbType.bool);
            parameters.Add("role", User.role, DbType.string);

            using (var conn = new _context.CreateConnection())
            {
                var id = await conn.QuerySingleAsync<int>(query, parameters);

                var CreateUser = new User
                {
                    Id = id,
                    email =User.email,
                    pass = User.pass,
                    address = User.address,
                    tel = User.tel,
                    activated = User.activated,
                    role = User.role
                };

                return CreateUser;
            }

        }


        // Update an Existing User
        public async Task UpdateUser(int id, UserUpdateDto User)
        {
            var query = "UPDATE Users SET model=@model, details=@details,advancePayment=@advancePayment, monthlyInstallment=@monthlyInstallment, financeDuration=@financeDuration,imageURL=@imageURL WHERE Id=@Id ";

            var parameters = new DynamicParameters();
            parameters.Add("Id", User.Id, DbType.int);
            parameters.Add("email", User.email, DbType.string);
            parameters.Add("pass", User.pass, DbType.string);
            parameters.Add("address", User.address, DbType.string);
            parameters.Add("tel", User.tel, DbType.int);
            parameters.Add("activated", User.activated, DbType.bool);
            parameters.Add("role", User.role, DbType.string);

             using (var conn = new _context.CreateConnection())
            {
                await conn.ExecuteAsync(query, parameters);

            }
        }

         // Delete an Existing User
        public async Task DeleteUser(int id)
        {
            var query = "DELETE FROM Users WHERE Id =@Id";
            using (var conn = new _context.CreateConnection())
            {
                await conn.ExecuteAsync(query, new {id});

            }
         }

public async Task<string> Login(UserLoginDto loginDto)
{
    try
    {
        var query = "SELECT * FROM Users WHERE email = @Email AND pass = @Password";
        using (var conn = _context.CreateConnection())
        {
            var user = await conn.QuerySingleOrDefaultAsync<User>(query, new { Email = loginDto.Email, Password = loginDto.Password });

            if (user != null)
            {
                // إذا تم العثور على المستخدم وكانت البيانات صحيحة، قم بإصدار رمز JWT

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.email),
                    
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(1), 
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return jwtToken;
            }
            else
            {
               
                return "false";
            }
        }
    }
    catch (Exception ex)
    {
       
        throw new Exception("حدث خطأ أثناء محاولة تسجيل الدخول.", ex);
    }
}



    }
}