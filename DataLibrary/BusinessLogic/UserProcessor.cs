using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser(string emailAddress, string firstName, string lastName, string identityNumber, string password)
        {
            UserModel data = new UserModel
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName=lastName,
                IdentityNumber=identityNumber,
                Password=password,
                Role=0
            };

            //string sql = @"insert into dbo.User (EmailAddress) values(@EmailAddress);";
            string sql = @"insert into [dbo].[User] (EmailAddress, FirstName, LastName, IdentityNumber, Password, Role) values (@EmailAddress, @FirstName, @LastName, @IdentityNumber, @Password, @Role);";
            return SqlDataAccess.SaveData(sql, data);
        }
        public static List<UserModel> LoadUsers()
        {
            string sql = @"select Id, EmailAddress, FirstName, LastName, IdentityNumber from [dbo].[User];";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }

        public static List<UserModel> LoadUser(string email)
        {

            string sql = @"SELECT [dbo].[User].Id, [dbo].[User].Password, [dbo].[User].Firstname, [dbo].[User].Lastname, [dbo].[User].EmailAddress, [dbo].[User].Role
                            FROM [dbo].[User] WHERE [dbo].[User].EmailAddress=@param;";

            return SqlDataAccess.LoadDataWithParam<UserModel>(sql, email);

        }
    }

}
