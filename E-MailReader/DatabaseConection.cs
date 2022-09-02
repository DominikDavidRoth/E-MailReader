using System.Data.SqlClient;


public class DatabaseConection
{
    public void getDivices(List<Device> devices)
        {

        //connection string 
        string constr = "Server=DESKTOP-IE300PO\\SQLEXPRESS01;Database=master;Integrated Security=SSPI";
        string querystr = "SELECT Name FROM [master].[dbo].[Device]";

        //create instanace of database connection
        SqlConnection conn = new SqlConnection(constr);
        SqlCommand command = new SqlCommand(querystr, conn);

        try
        {
            conn.Open();
            //read data line for line
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Device device = new Device((string)reader["Name"]);
                devices.Add(device);
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Database Error: " + e.Message);
        }
        conn.Close();

    }
}

