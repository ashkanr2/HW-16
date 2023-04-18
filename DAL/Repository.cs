using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using Online_shop.Models;
using System.Collections.Generic;
using My_App.Store.Demo.Models;

namespace Online_shop.DAL
{
    public class Repository : IRepository
    {
        private readonly string _connectionString;

        public Repository(IConfiguration connectionString)
        {
            _connectionString = connectionString.GetConnectionString("ConnectionString");
        }

        public int SearchRegisteredUser(string? username, string? password)
        {
        
        int id = 1;
            return id;
        }

        public void InsertNewUser(string? firsName, string? lastName, string? password, string? username)
        {
            const string queryString = "INSERT INTO Customers (First_Name,Last_Name,Password,Email)" +
                                       " VALUES (N'@f_name',N'@l_name','@password','@email');";

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("f_name", firsName));
            command.Parameters.Add(new SqlParameter("l_name", lastName));
            command.Parameters.Add(new SqlParameter("password", password));
            command.Parameters.Add(new SqlParameter("email", username));

            command.ExecuteNonQuery();

            connection.Close();
        }

        public List<Product> SearchedProductList(string? search)
        {
            List<Product> products = new();
            const string queryString = "SELECT S.Id,S.Product_Name,S.Unit_Price,I.Qty,I.Enter_Time,I.Exit_Time " +
                                       "FROM Products S JOIN Inventories I ON S.Id=I.Product_Id WHERE  S.Product_Name IS NOT NULL;";

            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            //command.Parameters.Add(new SqlParameter("search_string", search));


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    ProductName = reader["Product_Name"].ToString(),
                    Price = (int)reader["Unit_Price"],
                    Qty = (int)reader["Qty"],
                    EnterTime = Convert.ToDateTime(reader["Enter_Time"].ToString()),
                    ExitTime = Convert.ToDateTime(reader["Exit_Time"].ToString())
                });
            }

            reader.Close();

            connection.Close();

            return products;
        }

        public void Delete(int id)
        {
            string queryString = "Delete FROM Products Where Id=@id ";

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", id));
            command.ExecuteNonQuery();

            connection.Close();
        }


        public void CreateProduct(string productName, int price, int qty)
        {
            string queryString1 = "Insert Into Products (Product_Name,Unit_Price) Values(@name, @unitPrice);" +
                                  "INSERT INTO Inventories (Product_Id,Qty,Enter_Time,Exit_Time) VALUES ((SELECT MAX(Id)FROM Products) ,@qty,GETDATE(),GETDATE())";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                var command = new SqlCommand(queryString1, connection);
                command.Parameters.Add(new SqlParameter("name", productName));
                command.Parameters.Add(new SqlParameter("unitPrice", price));
                command.Parameters.Add(new SqlParameter("qty", qty));
                command.ExecuteNonQuery();


                connection.Close();
            }
        }

        public Product GetById(int id)
        {
            List<Product> products = new List<Product>();

            string queryString = "SELECT S.Id,S.Product_Name,S.Unit_Price,I.Qty,I.Enter_Time,I.Exit_Time " +
                                 "FROM Products S JOIN Inventories I ON S.Id=I.Product_Id WHERE S.Id=@id;";

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                var command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("id", id));
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductName = reader["Product_Name"].ToString(),
                        Price = (int)reader["Unit_Price"],
                        Qty = (int)reader["Qty"],
                        EnterTime = Convert.ToDateTime(reader["Enter_Time"]),
                        ExitTime = Convert.ToDateTime(reader["Exit_Time"])
                    });
                }

                reader.Close();
            }

            return products.First();
        }

        public void Edit(Product product)
        {
            string queryString = "Update Products Set Product_Name=@name, Unit_Price=@unitPrice Where Id=@id;" +
                                 "Update Inventories Set Qty=@qty, Enter_Time=@Enter_Time, Exit_Time=@Exit_Time Where Product_Id=@id";

            using var connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", product.Id));
            command.Parameters.Add(new SqlParameter("name", product.ProductName));
            command.Parameters.Add(new SqlParameter("unitPrice", product.Price));

            command.Parameters.Add(new SqlParameter("qty", product.Qty));
            command.Parameters.Add(new SqlParameter("Enter_Time", product.EnterTime));
            command.Parameters.Add(new SqlParameter("Exit_Time", product.ExitTime));
            command.ExecuteNonQuery();


            connection.Close();
        }

        public int CheckForInsertIntoProductTable(string productName, int price)
        {
            const string queryString =
                "SELECT P.Id FROM Products P WHERE P.Product_Name=@name AND P.Unit_Price=@price;";

            var id = 0;
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("name", productName));
            command.Parameters.Add(new SqlParameter("price", price));

            var reader = command.ExecuteReader();

            while (reader.Read())
                id = (int)reader["Id"];

            reader.Close();

            connection.Close();
            return id;
        }

        public void AddToBasket(int id)
        {
            string queryString1 =
                "Insert Into Baskets(Customer_Id, Product_Id, Count) values(@customer_id,@product_id, @count);" +
                "UPDATE Inventories SET Qty = Qty - 1 WHERE Product_Id = @product_id";

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new SqlCommand(queryString1, connection);
            command.Parameters.Add(new SqlParameter("customer_id", CurrentUser.Id));
            command.Parameters.Add(new SqlParameter("product_id", id));
            command.Parameters.Add(new SqlParameter("count", 1));

            command.ExecuteNonQuery();


            connection.Close();
        }

        public int CheckForInsertIntoBasketTable(int productId)
        {
            const string queryString = "SELECT B.Id FROM Baskets B WHERE B.Product_Id =@Id";

            var id = 0;
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", productId));
            var reader = command.ExecuteReader();

            while (reader.Read())
                id = (int)reader["Id"];

            reader.Close();

            connection.Close();
            return id;
        }

        public void EditForBasketTable(int id)
        {
            string queryString = "UPDATE Baskets SET Count = Count + 1 WHERE Product_Id = @id;" +
                                 "UPDATE Inventories  SET Qty = Qty - 1 WHERE Product_Id = @id";

            using var connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", id));

            command.ExecuteNonQuery();


            connection.Close();
        }

        public List<CartProductViewModel> GetBasketList()
        {
            List<CartProductViewModel> cardProducts = new();
            const string queryString =
                "SELECT B.Count,P.Product_Name,P.Unit_Price FROM Baskets B JOIN Products P ON B.Product_Id = P.Id " +
                "JOIN Customers C ON C.Id=B.Customer_Id WHERE C.Id =@Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", CurrentUser.Id));
            //command.Parameters.Add(new SqlParameter("search_string", search));


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                cardProducts.Add(new CartProductViewModel()
                {
                    ProductName = reader["Product_Name"].ToString(),
                    Price = (int)reader["Unit_Price"],
                    Qty = (int)reader["Count"],
                });
            }

            reader.Close();

            connection.Close();

            return cardProducts;
        }

        public int CreateFactor()
        {
            string queryString1 = @"INSERT INTO Factors(create_date, total_price, discount, customer_id)

            SELECT GETDATE(), SUM(B.Count * P.Unit_Price), 0, @current_user
                FROM Baskets B

            JOIN Products P ON B.Product_Id = P.Id

            JOIN Customers C ON C.Id = B.Customer_Id

            WHERE C.Id = @current_user;"; 

            using SqlConnection connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new SqlCommand(queryString1, connection);
            command.Parameters.Add(new SqlParameter("current_user", CurrentUser.Id));
            command.ExecuteNonQuery();

            string queryString2 = "SELECT Id FROM Factors WHERE Customer_Id=@current_user";
            var selectCommand = new SqlCommand(queryString2, connection);
            selectCommand.Parameters.Add(new SqlParameter("current_user", CurrentUser.Id));
            var reader = selectCommand.ExecuteReader();

            var result = 0;
            while (reader.Read())
                     result =int.Parse(reader["Id"].ToString()!);

            connection.Close();
            return  result;
            
        }

        public void FinalPurchase(int id)
        {
            string queryString1 =
                @"INSERT INTO Product_Factor(Product_Id, Factor_Id, Qty) (SELECT P.Id,F.Id,B.Count FROM Baskets B JOIN Factors F
            ON B.Customer_Id = F.Customer_Id JOIN Products P
                ON B.Product_Id = P.Id WHERE F.Id = @factor_id AND F.Customer_Id = @customer_id);

            DELETE FROM Baskets WHERE Customer_id = @customer_id";

            using var connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            connection.Open();
            var command = new SqlCommand(queryString1, connection);
            command.Parameters.Add(new SqlParameter("factor_id", id));
            command.Parameters.Add(new SqlParameter("customer_id", CurrentUser.Id));

            command.ExecuteNonQuery();


            connection.Close();
        }

        public List<Factor> GetFactorList()
        {
            List<Factor> factors = new();
            const string queryString =
                "   SELECT Id ,Create_Date,Total_Price FROM Factors WHERE Customer_Id=@user_id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("user_id", CurrentUser.Id));


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                factors.Add(new Factor()
                {
                    FactorId =int.Parse(reader["Id"].ToString()!),
                    CreateDate = Convert.ToDateTime(reader["Create_Date"]),
                    TotalPrice = (int)reader["Total_Price"],
                });
            }

            reader.Close();

            connection.Close();

            return factors;
        }

        public List<Product> GetFactorDetails(int id)
        {
            List<Product> products = new();
            const string queryString =
                "SELECT P.Product_Name , P.Unit_Price, PF.Qty FROM Product_Factor PF JOIN Factors F " +
                "ON PF.Factor_Id=F.Id JOIN Products P ON PF.Product_Id=P.Id where F.Id=@factor_id AND F.Customer_Id=@id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(new SqlParameter("id", CurrentUser.Id));
            command.Parameters.Add(new SqlParameter("factor_id", id));


            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                products.Add(new Product()
                {
                    ProductName = (reader["Product_Name"].ToString()!),
                    Price = Convert.ToInt32(reader["Unit_Price"]),
                    Qty = (int)reader["Qty"],
                });
            }

            reader.Close();

            connection.Close();

            return products;
        }
    }
}