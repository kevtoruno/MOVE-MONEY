using System.Threading.Tasks;
using MoveMoney.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using MoveMoney.API.Helper;

namespace MoveMoney.API.Data
{
    public class MoveMoneyRepository : IMoveMoneyRepository
    {
        private readonly DataContext _context;
        public MoveMoneyRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //Customer Functions
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers
            .Include(i => i.TypeIdentification)
            .FirstOrDefaultAsync(u => u.Id == id);

            return customer;
        }

        public async Task<PagedList<Customer>> GetCustomers(CustomerParams customerParams)
        {
            var customers = _context.Customers
            .Include(i => i.TypeIdentification)
            .AsQueryable();

            return await PagedList<Customer>.CreateAsync(customers, customerParams.PageNumber, customerParams.PageSize);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<string> CustomerExists(string phoneNumber, string Identification)
        {
            if (await _context.Customers.AnyAsync(c => c.PhoneNumber == phoneNumber))
                return "This phone Number has been used already";
            else if (await _context.Customers.AnyAsync(c => c.Identification == Identification))
                return "This Identification has been used already";
            return null;
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

        public async Task<double> GetComissionValue(double amount, int senderCountryId, int receiverCountryId)
        {
            var comission = _context.Comissions
            .Include(c => c.ComissionRange)
            .Where(c => c.CountryReceiverId == receiverCountryId && c.CountrySenderId == senderCountryId)
            .FirstOrDefault();

            var comissionRange = await _context.ComissionRanges
            .Where(c => c.ComissionId == comission.Id)
            .FirstOrDefaultAsync(c => amount >= c.MinAmount && amount <= c.MaxAmount);

            return comissionRange.Percentage;
        }

        public async Task<Agency> GetAgency(int id)
        {
            var agency = await _context.Agency
            .FirstOrDefaultAsync(a => a.Id == id);

            return agency;
        }
    }
}