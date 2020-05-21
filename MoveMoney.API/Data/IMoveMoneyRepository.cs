using System.Collections.Generic;
using System.Threading.Tasks;
using MoveMoney.API.Helper;
using MoveMoney.API.Models;

namespace MoveMoney.API.Data
{
    public interface IMoveMoneyRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();

        //Customer functions
        Task<Customer> GetCustomer(int id);
        Task<PagedList<Customer>> GetCustomers(CustomerParams customerParams);
        Task<Customer> CreateCustomer(Customer customer);
        Task<string> CustomerExists(string phoneNumber, string Identification);

        //User functions
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);

    }
}