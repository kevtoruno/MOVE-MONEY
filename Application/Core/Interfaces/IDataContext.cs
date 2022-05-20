using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Core.Interface;

public interface IDataContext
{
    DbSet<Agency> Agency { get; set; }
    DbSet<ClosingCashAgent> ClosingCashAgents { get; set; }
    DbSet<ClosingCashManager> ClosingCashManagers { get; set; }
    DbSet<ClosingCashManangerDetail> ClosingCashManangerDetails { get; set; }
    DbSet<Country> Countries { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<TypeIdentification> TypeIdentifications { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<UserLogs> UserLogs { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    DbSet<Comission> Comissions { get; set; }
    DbSet<ComissionRange> ComissionRanges { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
