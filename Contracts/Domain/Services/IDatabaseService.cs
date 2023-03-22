using LiteDB;

namespace Shopping.Contracts.Domain.Services;

public interface IDatabaseService
{
    ILiteDatabase OpenDatabaseConnection();
}
