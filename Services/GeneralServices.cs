using Online_shop.DAL;
using Online_shop.Models;
using Online_shop.Models.ViewModels;
using System.Reflection;
using My_App.Store.Demo.Models;

namespace Online_shop.Services
{
    public class GeneralServices:IServices
    {
        private readonly IRepository _repository;

        public GeneralServices(IRepository repository)
        {
            _repository = repository;
        }
        public bool LogInUser(LogInUserViewModel model)
        {
            var id = _repository.SearchRegisteredUser(model.Email, model.Password);
            if (id <= 0) return false;
            CurrentUser.Id = id;
            return true;
        }

        public bool LogOut()
        {
            if (CurrentUser.Id == 0) return false;
            CurrentUser.Id = 0;
            return true;
        }

        public bool RegisterUser(RegisterUserViewModel model)
        {
            var id = _repository.SearchRegisteredUser(model.Email, model.Password);
            if (id > 0) return false;
            _repository.InsertNewUser(model.Name,model.Family,model.Password,model.Email);
            return true;
        }

        public List<Product> SearchedProductList(string? search) => _repository.SearchedProductList(search);
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public void CreateProduct(CreateProductViewModel model)
        {
            var resultId = _repository.CheckForInsertIntoProductTable(model.ProductName, model.Price) ;
            if (resultId > 0)
            {
                var resultProduct = _repository.GetById(resultId);
                resultProduct.ProductName = model.ProductName;
                resultProduct.Price = model.Price;
                resultProduct.Qty += model.Qty;
                _repository.Edit(resultProduct);
            }

            else
                _repository.CreateProduct(model.ProductName, model.Price, model.Qty);
        }



        public Product GetById(int id)
        {
           return _repository.GetById(id);
        }

        public void Edit(Product product)
        {
            _repository.Edit(product);
        }

        public void AddToBasket(int id)
        {
            var resultId = _repository.CheckForInsertIntoBasketTable(id);
            if (resultId > 0)
            {
                _repository.EditForBasketTable(id);
            }
            else
                _repository.AddToBasket(id);

            

        }

        public List<CartProductViewModel> GetBasketList()
        {
           return _repository.GetBasketList();
        }

        public void FinalPurchase()
        {
            var id=_repository.CreateFactor();
            _repository.FinalPurchase(id);
        }

        public List<Factor> FactorsList()
        {
            return _repository.GetFactorList();
        }

        public List<Product> GetFactorsDetails(int id)
        {
            return _repository.GetFactorDetails(id);
        }
    }
}
