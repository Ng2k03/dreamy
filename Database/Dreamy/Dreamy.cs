
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using WebApplicationn.Models;
using WebApplicationn.Database.Db;


namespace WebApplicationn.Database.Dreamy
{
    public class Dreamy
    {

        public static string AddToCart(Cart cart)
        {

            Connection connection = new Connection();

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();

                string query = "Insert into productsdb.cart (productNo,productName,category,quantity,price,finalPrice) values ('" + cart.productNo + "','" + cart.productName + "','" + cart.category + "','" + cart.quantity + "','" + cart.price + "','" + cart.finalPrice + "')";

                cmd = new MySqlCommand(query, connection.Config());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

            return "Success";
        }

        public static List<WebApplicationn.Models.Cart> GetProducts()
        {
            List<WebApplicationn.Models.Cart> list = new List<WebApplicationn.Models.Cart>();


            Connection connection = new Connection();

            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();

                string query = "select productno,productname, category, quantity, price, finalPrice from productsdb.cart;";

                cmd = new MySqlCommand(query, connection.Config());
                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    list.Add(new WebApplicationn.Models.Cart()
                    {
                        productNo = mySqlDataReader.IsDBNull(0) ? 0 : mySqlDataReader.GetInt32(0),
                        productName = mySqlDataReader.IsDBNull(1) ? null : mySqlDataReader.GetString(1),
                        category = mySqlDataReader.IsDBNull(2) ? null : mySqlDataReader.GetString(2),
                        quantity = mySqlDataReader.IsDBNull(3) ? 0 : mySqlDataReader.GetInt32(3),
                        price = mySqlDataReader.IsDBNull(4) ? 0 : mySqlDataReader.GetInt64(4),
                        finalPrice = mySqlDataReader.IsDBNull(5) ? 0 : mySqlDataReader.GetInt64(5),

                    });
                }
            }


            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        public static string RemoveFromCart(int prodNo)
        {
            Connection connection = new Connection();

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();

                string query = "Delete from productsdb.cart where productno=" + prodNo + ";";

                cmd = new MySqlCommand(query, connection.Config());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

            return "Success";
        }

        public static string UpdateCart(int qty, int prodNo, Int64 finalPrice)
        {

            Connection connection = new Connection();

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();


                string query = "UPDATE `productsdb`.`cart` SET `quantity` = " + "'" + qty + "'" + ", `finalPrice` = '" + finalPrice + "' WHERE (`productno` = '" + prodNo + "');";


                cmd = new MySqlCommand(query, connection.Config());
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

            return "success";
        }

        public static Int64 SubTotal()
        {
            Cart cart = new Cart();
            List<Cart> carts = new List<Cart>();

            Int64 subtotal = 0;

            Connection connection = new Connection();

            try
            {

                connection.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = connection.Config();
                string query = "select SUM(finalPrice) from productsdb.cart;";
                cmd = new MySqlCommand(query, connection.Config());

                MySqlDataReader mySqlDataReader = cmd.ExecuteReader();



                if (mySqlDataReader.Read())
                {
                    subtotal = mySqlDataReader.IsDBNull(0) ? 0 : mySqlDataReader.GetInt64(0);
                }



            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Something Went Wrong: {ex.Message}");
            }

            return subtotal;
        }


    }
}
