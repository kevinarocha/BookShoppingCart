using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookShoppingCartMvcUI.Services
{
    public interface ShoppingCartEmailSender : IEmailSender
    {
        Task SendContactEmailAsync(string emailFrom, string name, string subject, string htmlMessage);
    }
}
