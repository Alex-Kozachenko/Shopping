using LiteDB;
using Shopping.Contracts.Domain.Services;

namespace Shopping.Data;

public class DatabaseService : IDatabaseService
{
    private readonly string? connectionString;
    private readonly Stream? stream;

    static DatabaseService()
    {
        BsonMapperRegistry.Register();
    }

    public DatabaseService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DatabaseService(Stream stream)
    {
        this.stream = stream;
    }

    public ILiteDatabase OpenDatabaseConnection() 
    {
        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            return new LiteDatabase(connectionString);
        }
        else if (stream != null) 
        {
            return new LiteDatabase(stream);
        }

        throw new InvalidOperationException();
    }
}
