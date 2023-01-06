using AutoMapper;
using CleanUser.ApplicationCore.DTOs;
using CleanUser.ApplicationCore.Entities;
using CleanUser.ApplicationCore.Exceptions;
using CleanUser.ApplicationCore.Interfaces;
using CleanUser.ApplicationCore.Utils;
using CleanUser.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanUser.Infrastructure.Persistence.Repositories
{
    internal class ADOUserRepository : IUserRepository
    {

        private readonly IMapper _mapper;
        private readonly SqlConnection _con;

        public ADOUserRepository(IMapper mapper)
        {
            _mapper = mapper;
            _con = new SqlConnection("Server=localhost;Database=CleanUser;Trusted_Connection=True;TrustServerCertificate=True;");

        }
        public UserResponse CreateUser(CreateUserRequest request)
        {
            var user = _mapper.Map<User>(request);

            user.CreateTime = DateUtil.GetCurrentDate();

            string result = "";

            try

            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Users", _con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", 0);

                cmd.Parameters.AddWithValue("@Name", user.Name);

                cmd.Parameters.AddWithValue("@Age", user.Age);

                cmd.Parameters.AddWithValue("@Credit", user.Credit);

                cmd.Parameters.AddWithValue("@CreateTime", user.CreateTime);

                cmd.Parameters.AddWithValue("@Query", 1);

                _con.Open();

                result = cmd.ExecuteScalar().ToString();

                return _mapper.Map<UserResponse>(user);

            }

            catch

            {

                throw new Exception("User has not been created");

            }

            finally

            {

                _con.Close();

            }

        }

        public void DeleteUserById(int userId)
        {
            string result = "";

            try

            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_User", _con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", userId);

                cmd.Parameters.AddWithValue("@Name", null);

                cmd.Parameters.AddWithValue("@Age", null);

                cmd.Parameters.AddWithValue("@Credit", null);

                cmd.Parameters.AddWithValue("@CreateTime", null);

                cmd.Parameters.AddWithValue("@Query", 3);

                _con.Open();

                result = cmd.ExecuteScalar().ToString();

            }

            catch

            {

                throw new Exception("User was not deleted");

            }

            finally

            {
                _con.Close();

            }
        }


        public UserResponse GetUserById(int userId)
        {
            DataSet ds = null;

            User Userobj = null;

            try

            {

                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Users", _con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", userId);

                cmd.Parameters.AddWithValue("@Name", null);

                cmd.Parameters.AddWithValue("@Age", null);

                cmd.Parameters.AddWithValue("@Credit", null);

                cmd.Parameters.AddWithValue("@CreateTime", null);

                cmd.Parameters.AddWithValue("@Query", 5);

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                ds = new DataSet();

                da.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    Userobj = new User();

                    Userobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);

                    Userobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();

                    Userobj.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"]);

                    Userobj.CreateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreateTime"].ToString());

                    Userobj.Credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());

                }

            }

            catch

            {

                throw new Exception("User was not found");

            }

            finally

            {

                _con.Close();

            }
            if (Userobj != null)
            {
                return _mapper.Map<UserResponse>(Userobj);
            }

            throw new NotFoundException();
        }

        public List<UserResponse> GetUsers()
        {
            DataSet ds = null;

            List<UserResponse> usertlist = null;

            try

            {

                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Users", _con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", null);

                cmd.Parameters.AddWithValue("@Name", null);

                cmd.Parameters.AddWithValue("@Age", null);

                cmd.Parameters.AddWithValue("@Credit", null);

                cmd.Parameters.AddWithValue("@CreateTime", null);

                cmd.Parameters.AddWithValue("@Query", 4);

                _con.Open();

                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = cmd;

                ds = new DataSet();

                da.Fill(ds);

                usertlist = new List<UserResponse>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)

                {

                    User Userobj = new User();

                    Userobj.Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Id"]);

                    Userobj.Name = ds.Tables[0].Rows[i]["Name"].ToString();

                    Userobj.Age = Convert.ToInt32(ds.Tables[0].Rows[i]["Age"]);

                    Userobj.CreateTime = Convert.ToDateTime(ds.Tables[0].Rows[i]["CreateTime"].ToString());

                    Userobj.Credit = Convert.ToDecimal(ds.Tables[0].Rows[i]["Credit"].ToString());



                    usertlist.Add(_mapper.Map<UserResponse>(Userobj));

                }

                return usertlist;

            }

            catch

            {

                throw new Exception("Error was occured!");

            }

            finally

            {

                _con.Close();

            }
        }

        public UserResponse UpdateUser(int userId, CreateUserRequest request)
        {
            string result = "";

            try

            {
                SqlCommand cmd = new SqlCommand("Usp_InsertUpdateDelete_Users", _con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", userId);

                cmd.Parameters.AddWithValue("@Name", request.Name);

                cmd.Parameters.AddWithValue("@Age", request.Age);

                cmd.Parameters.AddWithValue("@Credit", request.Credit);

                cmd.Parameters.AddWithValue("@CreateTime", request.CreateTime);

                cmd.Parameters.AddWithValue("@Query", 2);

                _con.Open();

                result = cmd.ExecuteScalar().ToString();

                return new UserResponse() 
                {
                    Id= userId,
                    Name= request.Name,
                    Age= request.Age,
                    Credit= request.Credit,
                    CreateTime= request.CreateTime,
                };

            }

            catch

            {

                throw new Exception("Error was occured!");

            }

            finally

            {

                _con.Close();

            }

            throw new NotFoundException();
        }
    }
}
