using System.Data.SqlClient;

namespace ProductCategory.UserData
{
    public class LoginCurd
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;

        public LoginCurd(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(configuration.GetConnectionString("DbConnection"));
        }

        public Register GetLogin(string email, string password)
        {

            Register register = new Register();
            string qry = "select * from Registration where email=@email and password=@password";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    register.UserID = Convert.ToInt32(dr["userId"]);
                    register.Email = dr["email"].ToString();
                    register.RoleId= Convert.ToInt32(dr["roleId"].ToString());

                }
            }
            con.Close();
            return register;
        }
    }
}
