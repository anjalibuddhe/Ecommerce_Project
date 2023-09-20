
using System.Data.SqlClient;

namespace ProductCategory.Models
{
    public class CartCurd
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public CartCurd(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DbConnection"));
        }
        public int AddTOCart(Cart cart)
        {
            int result = 0;

            string qry = "insert into Cart values (@id,@userID,@quantity)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", cart.Id);
            cmd.Parameters.AddWithValue("@userID", cart.UserID);
            cmd.Parameters.AddWithValue("@quantity", cart.Quantity);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;

        }
        public List<Product> ViewCart(int UId)
        {
            List<Product> products = new List<Product>();
            string qry = "SELECT p.*, c.caid, c.id, c.quantity from Product p join Cart c on c.id=p.id WHERE c.userID = userID";
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
                    p.Imageurl = dr["imageUrl"].ToString();
                    p.CartId = Convert.ToInt32(dr["caid"]);
                    p.Quantity = Convert.ToInt32(dr["quantity"]);
                    products.Add(p);
                }
            }
            return products;
        }
        public int DeleteCart(int CartId)
        {
            int result = 0;

            string qry = " delete from Cart where caid=@caid";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@caid", CartId);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();

            return result;

        }

        internal int AddTOCart(CartDetails cartDetails)
        {
            throw new NotImplementedException();
        }
    }
}
