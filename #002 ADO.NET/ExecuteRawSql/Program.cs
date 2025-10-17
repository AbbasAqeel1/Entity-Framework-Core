using System;
using System.Data;
using System.Net.Http.Headers;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;




namespace ConnectionString
{

    public class Program
    {
        public static void Main(string[] args)
        {
            //ExecuteRawSqlSelectStatement();
            //ExecuteNonQuery();
            //ExecuteScalar();
            //InsertUsingStoredProcedure();
            //UpdateRowSql();
            //ExecuteDelete();
            //ExecuteRowSqlDataAdapter();
            //RunTransaction();
            Console.ReadKey();
        }

        public static void RunTransaction()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            SqlConnection connection = new SqlConnection(configuration.GetSection("constr").Value);

            SqlCommand command = connection.CreateCommand();

            command.CommandType = CommandType.Text;
            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            command.Transaction = transaction;



            try
            {
                command.CommandText = "Update wallets set Balance = Balance - 1000 where Id = 18";
                command.ExecuteNonQuery();


                command.CommandText = "Update wallets set Balance = Balance + 1000 where Id = 1";
                command.ExecuteNonQuery();

                transaction.Commit();

                Console.WriteLine("Transaction completed successfully.");
            }
            catch (Exception)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    //log error
                }

            }
            finally
            {
                try
                { 
                    connection.Close();
                }
                catch (Exception)
                {

                    //log error
                }
               
            }



            


        }
        public static void ExecuteRowSqlDataAdapter()
        {

            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);

            var query = "Select * from Wallets";

            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;

            connection.Open();
            
            SqlDataAdapter adapter = new SqlDataAdapter(query,connection);

            DataTable dt = new DataTable();

            adapter.Fill(dt);

            connection.Close();


            foreach (DataRow row in dt.Rows)
            {

                var wallet = new Wallet
                {
                    Id = Convert.ToInt32(row["id"]),
                    Holder = Convert.ToString(row["Holder"]),
                    Balance = Convert.ToDecimal(row["Balance"])
                };
                Console.WriteLine(wallet);
            }
        }
        public static void ExecuteDelete()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);




            Wallet WalletToInsert = new Wallet();


            var query = "delete from  wallets where Id = @id";



            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;

            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = 16

            };


            command.Parameters.Add(idParameter);


            connection.Open();



            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("Customer deleted Successfully.");
            }
            else
            {
                Console.WriteLine("Customer not deleted successfully.");
            }





            connection.Close();
        }

        public static void UpdateRowSql()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);




            Wallet WalletToInsert = new Wallet();


            var query = "Update wallets set " +
                "Holder = @Holder," +
                "Balance = @Balance " +
                "where Id = @id";



            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;

            SqlParameter idParameter = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = 1
            };
            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = "Michael"
            };

            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = 999
            };
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);
            command.Parameters.Add(idParameter);


            connection.Open();

           

            if (command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine("Customer Updated Successfully.");
            }
            else
            {
                Console.WriteLine("Customer not updated successfully.");
            }





            connection.Close();
        }

        public static void InsertUsingStoredProcedure()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);


            var name = default(string);
            var balance = default(decimal);


            Console.WriteLine("\nPlease enter your name: ");
            name = Console.ReadLine();

            Console.WriteLine("\nPlease enter your Balance($): ");
            balance = Convert.ToDecimal(Console.ReadLine());


            Wallet WalletToInsert = new Wallet
            {
                Holder = name,
                Balance = balance,
            };



           


            SqlCommand command = new SqlCommand("AddWallet", connection);

            command.CommandType = CommandType.StoredProcedure;


            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Holder
            };

            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Balance
            };
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);


            connection.Open();

            WalletToInsert.Id = command.ExecuteNonQuery();






            connection.Close();
        }

        public static void ExecuteScalar()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);


            var name = default(string);
            var balance = default(decimal);


            Console.WriteLine("\nPlease enter your name: ");
            name = Console.ReadLine();

            Console.WriteLine("\nPlease enter your Balance($): ");
            balance = Convert.ToDecimal(Console.ReadLine());


            Wallet WalletToInsert = new Wallet
            {
                Holder = name,
                Balance = balance,
            };



            var query = "Insert into wallets(Holder,Balance)" +
                "values(@Holder,@Balance);" +
                "Select cast(SCOPE_IDENTITY() as int)";



            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;


            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Holder
            };

            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Balance
            };
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);


            connection.Open();

            WalletToInsert.Id =(int) command.ExecuteScalar();
            
            




            connection.Close(); 
        }

        public static void ExecuteRawSqlSelectStatement()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);

            var query = "Select * from Wallets";

            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            Wallet wallet;

            while (reader.Read())
            {
                wallet = new Wallet
                {
                    Id = reader.GetInt32("Id"),
                    Holder = reader.GetString("Holder"),
                    Balance = reader.GetDecimal("Balance")
                };

                Console.WriteLine(wallet);

            }

            connection.Close();
        }

        public static void ExecuteNonQuery()
        {
            var configuration1 = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();


            SqlConnection connection = new SqlConnection(configuration1.GetSection("constr").Value);


            var name = default(string);
            var balance = default(decimal);


            Console.WriteLine("\nPlease enter your name: ");
            name = Console.ReadLine();

            Console.WriteLine("\nPlease enter your Balance($): ");
            balance = Convert.ToDecimal(Console.ReadLine());


            Wallet WalletToInsert = new Wallet
            {
                Holder = name,
                Balance = balance,
            };



            var query = "Insert into wallets(Holder,Balance)" +
                "values(@Holder,@Balance)";



            SqlCommand command = new SqlCommand(query, connection);

            command.CommandType = CommandType.Text;


            SqlParameter HolderParameter = new SqlParameter
            {
                ParameterName = "@Holder",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Holder
            };

            SqlParameter BalanceParameter = new SqlParameter
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.Decimal,
                Direction = ParameterDirection.Input,
                Value = WalletToInsert.Balance
            };
            command.Parameters.Add(HolderParameter);
            command.Parameters.Add(BalanceParameter);
           

            connection.Open();
            

            if(command.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Customer With name ({WalletToInsert.Holder}) added Successfully.\n");
            }
            else
            {
                Console.WriteLine("Customer not Added Successfully");
            }
            

            

            connection.Close();
        }
    }
}