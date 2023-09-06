using System.Data.SqlClient;

namespace ProductCategory.Models
{
    public class ProductCurd
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public ProductCurd(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DbConnection"));
        }


        public IEnumerable<Product> GetProducts()
        {
            List<Product> list = new List<Product>();
            string qry = "select pro.*, cate.Cname from Product pro inner join Category cate on cate.cid = pro.cid";
            cmd = new SqlCommand(qry, con);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    product.Imageurl = dr["imageurl"].ToString();
                    product.CId = Convert.ToInt32(dr["cid"]);
                    product.Cname = dr["cname"].ToString();
                    list.Add(product);
                }
            }
            con.Close();
            return list;
        }
        public Product GetProductById(int id)
        {
            Product product = new Product();
            string qry = "select pro.*, cate.Cname from Product pro inner join Category cate on cate.cid = pro.cid where pro.id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    product.Id = Convert.ToInt32(dr["id"]);
                    product.Name = dr["name"].ToString();
                    product.Price = Convert.ToDouble(dr["price"]);
                    product.Imageurl = dr["imageurl"].ToString();
                    product.CId = Convert.ToInt32(dr["cid"]);
                    product.Cname = dr["cname"].ToString();
                }
            }
            con.Close();
            return product;
        }


        public int AddProduct(Product product)
        {
            int result = 0;
            string qry = "insert into Product values(@name,@price,@imageurl,@cid)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@imageurl", product.Imageurl);
            cmd.Parameters.AddWithValue("@cid", product.CId);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;


        }
        public int UpdateProduct(Product product)
        {
            int result = 0;
            string qry = "update Product set name=@name,price=@price,imageurl=@imageurl,cid=@cid where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@price", product.Price);
            cmd.Parameters.AddWithValue("@imageurl", product.Imageurl);
            cmd.Parameters.AddWithValue("@cid", product.CId);
            cmd.Parameters.AddWithValue("@id", product.Id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int DeleteProduct(int id)
        {
            int result = 0;
            string qry = "delete from Product where id=@id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}

