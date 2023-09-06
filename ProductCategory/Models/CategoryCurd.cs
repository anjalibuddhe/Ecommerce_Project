using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ProductCategory.Models
{
    public class CategoryCurd
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;


        public CategoryCurd(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DbConnection"));
        }

        public IEnumerable<Category> GetCategories() 
        {
            List<Category> list = new List<Category>();
            string qry = "select * from Category";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Category category = new Category();
                    category.CId = Convert.ToInt32(dr["cid"]);
                    category.Cname = dr["cname"].ToString();
                    list.Add(category);
                }
            }
            con.Close();
            return list;
        }

        public Category GetCategoryById(int id) { 
        Category category = new Category();
            string qry = "select * from Category where cid=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    category.CId = Convert.ToInt32(dr["cid"]);
                    category.Cname = dr["cname"].ToString();
                }
            }
            con.Close();
            return category;
        }


        public int AddCategory(Category category)
        {
            int result = 0;
            string qry = "insert into Category values(@cname)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@cname", category.Cname);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public int UpadateCategory(Category category)
        {
            int result = 0;
            string str = "update Category set cname=@cname where cid=@cid";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@cname", category.Cname);
            cmd.Parameters.AddWithValue("@cid", category.CId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }


        public int DeleteCategory(int id) {

            int result = 0;
            string str = "delete from  Category where cid=@cid";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@cid", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}
