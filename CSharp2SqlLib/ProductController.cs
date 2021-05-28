using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace CSharp2SqlLib
{
   public class ProductController
    {
        private static Connection connection { get; set; }

       public ProductController(Connection connection)
        {
            ProductController.connection = connection;
        }



        private void GetVendorForProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                GetVendorForProduct(product);
            }
        }

        private void GetVendorForProduct(Product product)
        {
            var vendCtrl = new VendorController(connection);
            product.Vendor = vendCtrl.GetByPk(product.VendorId);
        }

        private Product FillProductFromSqlRow(SqlDataReader reader)
        {
            var product = new Product()
            {
                Id = Convert.ToInt32(reader["Id"]),
                PartNbr = Convert.ToString(reader["PartNbr"]),
                Name = Convert.ToString(reader["Name"]),
                Price = Convert.ToDecimal(reader["Price"]),
                Unit = Convert.ToString(reader["Unit"]),
                PhotoPath = Convert.ToString(reader["PhotoPath"]),
                VendorId = Convert.ToInt32(reader["VendorId"])
            };
            return product;
        }

        public bool Create(Product product, String Vendorcode)
        {
            var vendCtrl = new VendorController(connection);
            var vendor = vendCtrl.GetByCode(Vendorcode);
            product.VendorId = vendor.Id;
            return Create(product);
        }


        public bool Create(Product product)
        {

            var sql = $" INSERT into Products" + "( PartNbr, Name, Price, Unit, PhotoPath, VendorId ) " +
                " Values " + $" (@partnbr, @name, @price, @unit, @photopath, @vendorid) ";

            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@partnbr", product.PartNbr);
            sqlcmd.Parameters.AddWithValue("@name", product.Name);
            sqlcmd.Parameters.AddWithValue("@price", product.Price);
            sqlcmd.Parameters.AddWithValue("@unit", product.Unit);
            sqlcmd.Parameters.AddWithValue("@photopath", (object)product.PhotoPath ?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@vendorid", product.VendorId);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);

        }


        public Product GetByPk(int Id)
        {
            var sql = $"Select * from Products where id = {Id}";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);

            var sqldatareader = sqlcmd.ExecuteReader();
            if (!sqldatareader.HasRows)
            {
                return null;
            }
            sqldatareader.Read();
            var product = FillProductFromSqlRow(sqldatareader);
            sqldatareader.Close();
            GetVendorForProduct(product);
            return product;
        }

        public List<Product> GetAll()
        {
            var sql = "SELECT * from Products;";
            var cmd = new SqlCommand(sql, connection.SqlConn);
            var reader = cmd.ExecuteReader();
            var products = new List<Product>();
            while (reader.Read())
            {
                var product = FillProductFromSqlRow(reader);
                products.Add(product);

            }
            reader.Close();
            GetVendorForProducts(products);
            return products;
        }

        public bool Change(Product product)
        {
            var sql = $" Update Products Set " +
                "PartNbr = @partnbr, " +
                "Name = @name, " +
                "Price = @price, " +
                "Unit = @unit, " +
                "PhotoPath = @photopat, " +
                "VendorId = @vendorid, " +
                "Where Id = @id;";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@partnbr", product.PartNbr);
            sqlcmd.Parameters.AddWithValue("@name", product.Name);
            sqlcmd.Parameters.AddWithValue("@price", product.Price);
            sqlcmd.Parameters.AddWithValue("@unit", product.Unit);
            sqlcmd.Parameters.AddWithValue("@photopath", product.PhotoPath);
            sqlcmd.Parameters.AddWithValue("@vendorid", product.VendorId);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);


        }


        public bool Delete(Product product)
        {
            var sql = $"Delete from Products" +
                "Where Id = @id";
            var sqlcmd = new SqlCommand(sql, connection.SqlConn);
            sqlcmd.Parameters.AddWithValue("@id", product.Id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);

        }


    }
}
