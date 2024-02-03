using VisualSoftAspCoreApi.Entities;
using VisualSoftAspCoreApi.Dto;

namespace VisualSoftAspCoreApi.Contracts
{
    public interface  IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers();
        public Task <User> GetUser(int id);
        public Task <User> CreateUser(UserCreationDto user);
        public Task UpdateUser(int id, UserUpdateDto user);
        public Task DeleteUser(int id);
    }
}