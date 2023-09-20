

using System.Data.SqlClient;

namespace ProductCategory.UserData
{
    public class RegisterCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public RegisterCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("DbConnection"));
        }
        public IEnumerable<Register> GetAllUsers()
        {
            List<Register> list = new List<Register>();
            string qry = "select * from Registration";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Register user = new Register();
                    user.UserID = Convert.ToInt32(dr["userId"]);
                    user.FirstName = dr["firstName"].ToString();
                    user.LastName = dr["lirstName"].ToString();
                    user.Contact_No = dr["phone"].ToString();
                    user.Email = dr["email"].ToString();
                    user.Password = dr["password"].ToString();


                    list.Add(user);

                    
                }
            }
            con.Close();
            return list;
        }
        public Register GetUserById(int userID)
        {
            Register user = new Register();
            string qry = "select * from Registration where userID=@userID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@userID", userID);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                   user.UserID = Convert.ToInt32(dr["userID"]);
                    user.FirstName = dr["firstName"].ToString();
                    user.LastName = dr["lastName"].ToString();
                    user.Contact_No = dr["phone"].ToString();
                    user.Email = dr["email"].ToString();
                    user.Password = dr["password"].ToString();
                }
            }
            con.Close();
            return user;
        }
        public int AddUser(Register register)
        {
            int result = 0;
            string qry = "insert into Registration(firstName,lastName,phone,email,Password) values(@firstName,@lastName,@phone,@email,@password)";
            cmd = new SqlCommand(qry, con);

            cmd.Parameters.AddWithValue("@firstName", register.FirstName);
            cmd.Parameters.AddWithValue("@lastName", register.LastName);
            cmd.Parameters.AddWithValue("@phone", register.Contact_No);
            cmd.Parameters.AddWithValue("@email", register.Email);
            cmd.Parameters.AddWithValue("@password", register.Password);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
        public int UpdateUser(Register register)
        {
            int result = 0;
            string qry = "update Registration set userID=@userID,firstName=@firstName,lastName=@lastName,phone=@phone,email=@email,password=@password where userID=@userID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@userID", register.UserID);
            cmd.Parameters.AddWithValue("@firstName", register.FirstName);
            cmd.Parameters.AddWithValue("@lastName", register.LastName);
            cmd.Parameters.AddWithValue("@phone", register.Contact_No);
            cmd.Parameters.AddWithValue("@email", register.Email);
            cmd.Parameters.AddWithValue("@password", register.Password);
            cmd.Parameters.AddWithValue("@userID", register.UserID);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


        // soft delete --> record should be present in DB , but should not visible on the form
        public int DeleteUser(int userID)
        {
            int result = 0;
            string qry = "update Registration set  where userID=@userID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@userID", userID);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

    }
}