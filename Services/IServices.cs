using My_App.Store.Demo.Models;
using Online_shop.Models;
using Online_shop.Models.ViewModels;

namespace Online_shop.Services
{
    public interface IServices
    {
        bool LogInUser(LogInUserViewModel model);
        bool LogOut();
        bool RegisterUser(RegisterUserViewModel model);
        List<Product> SearchedProductList(string? search);
        void Delete(int id);
        void CreateProduct(CreateProductViewModel model);
        Product GetById (int id);
        void Edit(Product product);
        void AddToBasket(int id);
        List<CartProductViewModel> GetBasketList();
        void FinalPurchase();
        List<Factor> FactorsList();
        List<Product> GetFactorsDetails(int id);
    }
}
