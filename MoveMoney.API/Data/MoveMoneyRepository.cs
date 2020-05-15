using System.Threading.Tasks;
using MoveMoney.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers
            .Include(i => i.TypeIdentification)
            .FirstOrDefaultAsync(u => u.Id == id);

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            var customer = await _context.Customers
            .Include(i => i.TypeIdentification)
            .ToListAsync();

            return customer;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<string> CustomerExists(string phoneNumber, string Identification)
        {
            if(await _context.Customers.AnyAsync(c => c.PhoneNumber == phoneNumber))
                return "This phone Number has been used already";
            else if(await _context.Customers.AnyAsync(c => c.Identification == Identification))
                return "This Identification has been used already";
            return null;
        }
    }
}