using NUnit.Framework.Internal;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace DelawareDBTests
{
    public class DatabseTests
    {
        [Fact]
        public void TestQuery()
        {
            // Set up the connection string and the query
            string connectionString = "Server=localhost;TrustServerCertificate=True;Database=DelawareDB;Trusted_Connection=True;";
            string query = "select top 1 name, leftInStock from delaware_db.product";

            // Open a connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Create a command object and set the query text and connection
                SqlCommand command = new SqlCommand(query, connection);

                // Execute the query and get the result set
                SqlDataReader reader = command.ExecuteReader();

                // Assert that the reader contains at least one row
                Assert.True(reader.HasRows);

                // Read the first row of the result set
                reader.Read();

                // Retrieve the values of the name and leftInStock columns
                string name = reader.GetString(0);
                int leftInStock = reader.GetInt32(1);

                // Assert that the values are as expected
                Assert.Equal("sokken", name);
                Assert.Equal(1000, leftInStock);
            }
        }
    }
}