using CleanUser.ApplicationCore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanUser.ApplicationCore.Interfaces
{
    public interface IUserRepository
    {
        List<UserResponse> GetUsers();

        UserResponse GetUserById(int userId);

        void DeleteUserById(int userId);

        UserResponse CreateUser(CreateUserRequest request);

        // UserResponse UpdateUser(int userId, UpdateUserRequest request);
        UserResponse UpdateUser(int userId, CreateUserRequest request);
    }
}
