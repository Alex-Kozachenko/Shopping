using Shopping.Contracts.Data;

namespace Shopping.Contracts.Domain.Services;

public interface IProductService
{
    public Product Process(Product product);
}
