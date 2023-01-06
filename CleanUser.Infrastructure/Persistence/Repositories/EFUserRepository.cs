using AutoMapper;
using CleanUser.ApplicationCore.DTOs;
using CleanUser.ApplicationCore.Entities;
using CleanUser.ApplicationCore.Exceptions;
using CleanUser.ApplicationCore.Interfaces;
using CleanUser.ApplicationCore.Utils;
using CleanUser.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanUser.Infrastructure.Persistence.Repositories
{
    internal class EFUserRepository : IUserRepository
    {
        private readonly UserContext _userContext;
        private readonly IMapper _mapper;

        public EFUserRepository(UserContext userContext, IMapper mapper)
        {
            _userContext = userContext;
            _mapper = mapper;
        }
        public UserResponse CreateUser(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            user.CreateTime = DateUtil.GetCurrentDate();

            _userContext.Users.Add(user);
            _userContext.SaveChanges();

            return _mapper.Map<UserResponse>(user);
        }

        public void DeleteUserById(int userId)
        {
            var user = _userContext.Users.Find(userId);
            if (user != null)
            {
                _userContext.Users.Remove(user);
                _userContext.SaveChanges();
            }
            else
            {
                throw new NotFoundException();
            }
        }

        public UserResponse GetUserById(int userId)
        {
            var user = _userContext.Users.Find(userId);
            if (user != null)
            {
                return _mapper.Map<UserResponse>(user);
            }

            throw new NotFoundException();
        }

        public List<UserResponse> GetUsers()
        {
            return _userContext.Users.Select(u => _mapper.Map<UserResponse>(u)).ToList();
        }

        public UserResponse UpdateUser(int userId, CreateUserRequest request)
        {
            var user = _userContext.Users.Find(userId);
            if (user != null)
            {
                user.Name = request.Name;

                user.Age = request.Age;
                user.Credit = request.Credit;
                

                _userContext.Users.Update(user);
                _userContext.SaveChanges();

                return _mapper.Map<UserResponse>(user);
            }

            throw new NotFoundException();
        }
    }
}
