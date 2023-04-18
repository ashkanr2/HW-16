using My_App.Store.Demo.Models;
using Online_shop.Models;

namespace Online_shop.DAL
{
    public interface IRepository
    {
        int SearchRegisteredUser(string? username, string? password);
        void InsertNewUser(string? firsName,string? lastName, string? username, string? password);
        List<Product> SearchedProductList(string? search);
        void Delete(int id);
        void CreateProduct(string productName, int price, int qty);
        Product GetById(int id);
        void Edit(Product product);
        int CheckForInsertIntoProductTable(string productName, int price);
        void AddToBasket(int id);
        int CheckForInsertIntoBasketTable(int productId);
        void EditForBasketTable(int id);
        List<CartProductViewModel> GetBasketList();
        int CreateFactor();
        void FinalPurchase(int id);
        List<Factor> GetFactorList();
        List<Product> GetFactorDetails(int id);


    }
}
