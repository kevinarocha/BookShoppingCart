namespace BookShoppingCartMvcUI.Services
{
    public interface IConnectionService
    {
        string GetConnectionString(IConfiguration configuration);
    }
}
