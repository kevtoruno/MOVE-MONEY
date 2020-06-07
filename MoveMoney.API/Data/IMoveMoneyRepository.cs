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
        Task<IEnumerable<Customer>> GetCustomersAutoComplete(string names);

        //User functions
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);

        //Agency functions

        Task<Agency> GetAgency(int id);
        Task<IEnumerable<Agency>> GetAgencyAutoComplete(string name);

        //Order functions
        Task<double> GetComissionValue(double amount, int senderCountry, int receiverCountry);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);

        //User logs functions
        Task<UserLogs> CreateUserLog(UserLogs userLogs);
        Task<IEnumerable<UserLogs>> GetUserLogs(int id);

        //Country Function
        Task<int> GetCountryIdByAgency(int AgencyId);
    }
}