using DayFourTsk.Models;
using System.Data;

namespace DayFourTsk.DataAccess
{
    public class UserDAL
    {
        private readonly DataAccessLayer objDl;

        public UserDAL(IConfiguration config)
        {
            objDl = new DataAccessLayer(config);
        }
        public LinkedList<User> GetAllUsers()
        {
            var users = new List<User>();
            try
            {
                DataSet ds = objDl.RetrivedData("Sp_Users", null, null);
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        users.Add(new User
                        {
                            Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                            Name = row["Name"]?.ToString() ?? string.Empty,
                            Email = row["Email"]?.ToString() ?? string.Empty,
                            Age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0
                        });
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Error retrieving users: " + Ex.Message);
            }
            return new LinkedList<User>(users);
        }
        public void AddUser(User user)
        {
            bool Result = objDl.InsertData("Sp_UserAddition", new string[] { "@Name", "@Email", "@Age" }, new string[] { user.Name, user.Email, user.Age.ToString() });
            if (Result == true)
            {
                Console.WriteLine("User added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add user.");
            }
        }
        public User? GetUser(int Id)
        {
            try
            {
                DataSet ds = objDl.RetrivedData("Sp_Users", new string[] { "@Id" }, new string[] { Id.ToString() });
                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        return new User
                        {
                            Id = row["Id"] != DBNull.Value ? Convert.ToInt32(row["Id"]) : 0,
                            Name = row["Name"]?.ToString() ?? string.Empty,
                            Email = row["Email"]?.ToString() ?? string.Empty,
                            Age = row["Age"] != DBNull.Value ? Convert.ToInt32(row["Age"]) : 0
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving users: " + ex.Message);
            }
            return null;
        }
        public void UpdateUser(User user)
        {
            bool Result = objDl.InsertData("Sp_UserUpdate", new string[] { "Id", "@Name", "@Email", "@Age" }, new string[] { user.Id.ToString(), user.Name, user.Email, user.Age.ToString() });
            if (Result == true)
            {
                Console.WriteLine("User updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update user.");
            }
        }
        public void DeleteUser(int Id)
        {
            bool Result = objDl.InsertData("Sp_UserDeletion", new string[] { "@Id" }, new string[] { Id.ToString() });
            if (Result == true)
            {
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("Failed to delete user.");
            }
        }
    }
}
