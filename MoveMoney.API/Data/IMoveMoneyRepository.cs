using System.Collections.Generic;
using System.Threading.Tasks;
using MoveMoney.API.Models;

namespace MoveMoney.API.Data
{
    public interface IMoveMoneyRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> CreateCustomer(Customer customer);
        Task<string> CustomerExists(string phoneNumber, string Identification);
    }
}