using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cerovskyTestDb20220503
{
    public class SqlRepository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test20220503;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private List<Bill> GetBillsFromDb()
        {
            List<Bill> bills = new List<Bill>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM PurpleData";
                        using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                var Bill = new Bill()
                                {
                                    ID = Convert.ToInt32(sqlDataReader["id"]),
                                    dateTime = Convert.ToDateTime(sqlDataReader["times"]),
                                    BillNumber = Convert.ToInt32(sqlDataReader["orders"]),
                                    Subscriber = Convert.ToString(sqlDataReader["subscriber"])
                                };
                                bills.Add(Bill);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
                return bills;
            }
            catch (Exception ex)
            {
                throw new Exception($"Vyskytla se chyba: {ex.Message}");
            }
        }

        private Bill GetBill(int id)
        {
            List<Bill> bills = GetBillsFromDb();
            Bill bill = bills[id - 2];
            return bill;
        }

        public List<Operacione> GetHodnotyFromDb()
        {
            List<Operacione> values = new List<Operacione>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT * FROM OrangeData";
                    using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            var idBill = Convert.ToInt32(sqlDataReader["idBill"]);
                            var Values = new Operacione()
                            {
                                ID = Convert.ToInt32(sqlDataReader["id"]),
                                Name = Convert.ToString(sqlDataReader["names"]),
                                PriceForOnePiece = Convert.ToInt32(sqlDataReader["onepiece"]),
                                DPH = Convert.ToInt32(sqlDataReader["dph"]),
                                Bill = GetBill(idBill)
                            };
                            values.Add(Values);
                        }
                    }
                }
                sqlConnection.Close();
            }
            return values;

        // Nejde mi přeposílat přes GitHub databáze.

        //    CREATE TABLE OrangeData(id integer NOT NULL PRIMARY KEY, names varchar(30) NOT NULL, counts integer NOT NULL, onepiece integer NOT NULL, dph integer NOT NULL, bill integer NOT NULL FOREIGN KEY REFERENCES PurpleData(id));
        //    INSERT INTO OrangeData(id, names, counts, onepiece, dph, bill) VALUES(1, 'Alfa', 13, 10, 7, 1);
        //    INSERT INTO OrangeData(id, names, counts, onepiece, dph, bill) VALUES(2, 'Beta', 10, 9, 10, 2);

        //    CREATE TABLE PurpleData(id integer NOT NULL PRIMARY KEY, times date  NOT NULL, orders integer  NOT NULL, subscriber varchar(30) NOT NULL);
        //    INSERT INTO PurpleData(id, times, orders, subscriber) VALUES(1, '2015-12-17', 7891, 'Andrej');
        //    INSERT INTO PurpleData(id, times, orders, subscriber) VALUES(2, '2017-11-10', 7255, 'Michal');



        }
    }
}
