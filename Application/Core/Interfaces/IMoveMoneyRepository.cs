using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Core.Interface
{
    public interface IMoveMoneyRepository
    {
        //Customer functions
        Task<Customer> GetCustomer(int id);
        Task<IEnumerable<Customer>> GetCustomersAutoComplete(string names);

        IQueryable<Customer> GetCustomers(PaginationParams customerParams);

        //User functions
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);

        //Agency functions

        Task<Agency> GetAgency(int id);
        Task<IEnumerable<Agency>> GetAgencyAutoComplete(string name);

        //Order functions
        Task<Comission> GetComissionPerSenderAndCountry(int senderCountry, int receiverCountry);
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);

        //User logs functions
        Task<UserLogs> CreateUserLog(UserLogs userLogs);
        Task<IEnumerable<UserLogs>> GetUserLogs(int id);

        //Country Function
        Task<int> GetCountryIdByAgency(int AgencyId);
    }
}