using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Application.Core.Interface;
using Domain.Entities;
using Application.Core;

namespace Infraestructure.Repository
{
    public class MoveMoneyRepository : IMoveMoneyRepository
    {
        private readonly IDataContext _context;
        public MoveMoneyRepository(IDataContext context)
        {
            _context = context;

        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers
            .Include(i => i.TypeIdentification)
            .FirstOrDefaultAsync(u => u.Id == id);

            return customer;
        }

        public IQueryable<Customer> GetCustomers(PaginationParams customerParams)
        {
            var customersQueryable = _context.Customers
            .Include(i => i.TypeIdentification)
            .AsQueryable();

            return customersQueryable;
        }

        //User functions
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users
            .Include(u => u.Agency)
            .Include(u => u.UserRole)
            .ToListAsync();

            return users;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users
            .Include(u => u.Agency)
            .Include(u => u.UserRole)
            .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<Comission> GetComissionPerSenderAndCountry(int senderCountryId, int receiverCountryId)
        {
            var comission = await _context.Comissions
                .Include(a => a.ComissionRange)
                .Where(c => c.CountryReceiverId == receiverCountryId && c.CountrySenderId == senderCountryId)
                .FirstOrDefaultAsync();

            /*var comissionRange = await _context.ComissionRanges
            .Where(c => c.Comission.CountryReceiverId == receiverCountryId && c.Comission.CountrySenderId == senderCountryId && 
                amount >= c.MinAmount && amount <= c.MaxAmount) 
            .FirstOrDefaultAsync();*/
            return comission;
        }


        public async Task<IEnumerable<Customer>> GetCustomersAutoComplete(string names)
        {
            var customerToReturn = await _context.Customers.Where(p => (p.FirstName.ToLower() + " " + p.LastName.ToLower()).Contains(names.ToLower())).ToListAsync();

            return customerToReturn;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orderToReturn = await _context.Orders
            .Include(o => o.AgencyDestination)
            .Include(o => o.Sender)
            .Include(o => o.Recipient)
            .Include(o => o.User)
            .ThenInclude(o => o.Agency)
            .ToListAsync();

            return orderToReturn;
        }

        public async Task<Order> GetOrder(int id)
        {
            var orderToReturn = await _context.Orders
            .Where(o => o.Id == id)
            .Include(o => o.AgencyDestination)
            .Include(o => o.Sender)
            .Include(o => o.Sender.TypeIdentification)
            .Include(o => o.Recipient)
            .Include(o => o.Recipient.TypeIdentification)
            .Include(o => o.User)
            .ThenInclude(o => o.Agency)
            .FirstOrDefaultAsync();

            return orderToReturn;
        }
        //Agency Functions

        public async Task<Agency> GetAgency(int id)
        {
            var agency = await _context.Agency
            .FirstOrDefaultAsync(a => a.Id == id);

            return agency;
        }
        public async Task<IEnumerable<Agency>> GetAgencyAutoComplete(string name)
        {
            var agencyToReturn = await _context.Agency.Where(p => (p.AgencyName.ToLower()).Contains(name.ToLower())).ToListAsync();

            return agencyToReturn;
        }

        // UserLog functions
        public async Task<UserLogs> CreateUserLog(UserLogs userLogs)
        {
            await _context.UserLogs.AddAsync(userLogs);
            await _context.SaveChangesAsync();
            return userLogs;
        }

        public async Task<IEnumerable<UserLogs>> GetUserLogs(int id)
        {
            var userLogs = await _context.UserLogs.Where(u => u.AgencyId == id).ToListAsync();

            return userLogs;
        }

        public async Task<int> GetCountryIdByAgency(int AgencyId)
        {
            var countryId = await _context.Agency.Where(a => a.Id == AgencyId).FirstOrDefaultAsync();
            if(countryId == null)
                return 0;
            return countryId.CountryId;
        }
    }
}