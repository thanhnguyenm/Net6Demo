using Dapper;
using System.Data.SqlClient;

namespace OpenTelemetryDemo;

public interface IUserProvider
{
    UserAccount[] GetAll();
}

public class UserProvider : IUserProvider
{
    string connectionString;
    public UserProvider(string ctr)
    {
        connectionString = ctr;
    }

    public UserAccount[] GetAll()
    {
        using var connection = new SqlConnection(connectionString);
        var rs = connection.Query<UserAccount>("Select UserAccountId, Name From UserAccount").ToArray();

        return rs;
    }
    
}

public class UserAccount
{
    public int UserAccountId { get; set; }
    public string Name { get; set; }
}