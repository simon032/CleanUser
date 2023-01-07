using CleanUser.ApplicationCore.DTOs;

namespace CleanUser.ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        List<UserResponse> GetUsers();

        UserResponse GetUserById(int userId);

        void DeleteUserById(int userId);

        UserResponse CreateUser(CreateUserRequest request);

        UserResponse UpdateUser(int userId, CreateUserRequest request);
    }
}
