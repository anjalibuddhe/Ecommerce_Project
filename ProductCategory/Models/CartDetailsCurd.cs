using System.Data.SqlClient;

namespace ProductCategory.Models
{
    public class CartDetailsCurd
    {
         
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CartDetailsCurd(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DbConnection"));
        }

        public int AddToOrder(CartDetails cartDetails)
        {
            int result = 0;

            string qry = "insert into Orders(quantity,id,userID)values(@quantity,@id,@userID)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@quantity", cartDetails.Quantity);
            cmd.Parameters.AddWithValue("@id", cartDetails.Id);
            cmd.Parameters.AddWithValue("@userID", cartDetails.UserID);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;

        }


        public List<Product> MyOrder(int UId)
        {
            List<Product> products = new List<Product>();
            string qry = "select p.*,o.* from Product p join Orders o on o.id=p.id where o.userID=@userID";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@userID", UId);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product p = new Product();
                    p.Id = Convert.ToInt32(dr["id"]);
                    p.Name = dr["name"].ToString();
                    p.Price = Convert.ToDouble(dr["price"]);
                    p.Imageurl = dr["imageurl"].ToString();
                    p.Id = Convert.ToInt32(dr["id"]);
                    p.Quantity = Convert.ToInt32(dr["quantity"]);
                    products.Add(p);
                }
            }
            return products;
        }

    }
}
