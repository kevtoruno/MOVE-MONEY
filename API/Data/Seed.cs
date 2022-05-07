using System.Collections.Generic;
using System.Linq;
using MoveMoney.API.Models;
using Newtonsoft.Json;

namespace MoveMoney.API.Data
{
    public class Seed
    {
        public static void SeedCustomers(DataContext context)
        {
            if(!context.Customers.Any())
            {
                var customerData = System.IO.File.ReadAllText("SeedData/CustomerSeedData.json");
                var customers = JsonConvert.DeserializeObject<List<Customer>>(customerData);

                foreach (var customer in customers)
                {
                    customer.Identification.ToLower();
                    context.Add(customer);
                }

                context.SaveChanges();
            }
        }

        public static void SeedTypeIdentification(DataContext context)
        {
            if(!context.TypeIdentifications.Any())
            {
                var typeIdData = System.IO.File.ReadAllText("SeedData/TypeIdentificationData.json");
                var typeId = JsonConvert.DeserializeObject<List<TypeIdentification>>(typeIdData);

                foreach (var identification in typeId)
                {
                    context.Add(identification);
                }

                context.SaveChanges();
            }
        }

        public static void SeedCountry(DataContext context)
        {
            if(!context.Countries.Any())
            {
                var countrydData = System.IO.File.ReadAllText("SeedData/CountrySeedData.json");
                var countries = JsonConvert.DeserializeObject<List<Country>>(countrydData);

                foreach (var country in countries)
                {
                    context.Add(country);
                }

                context.SaveChanges();
            }
        }

        public static void SeedAgency(DataContext context)
        {
            if(!context.Agency.Any())
            {
                var agencyData = System.IO.File.ReadAllText("SeedData/AgencySeedData.json");
                var agencies = JsonConvert.DeserializeObject<List<Agency>>(agencyData);

                foreach (var agency in agencies)
                {
                    context.Add(agency);
                }

                context.SaveChanges();
            }
        }
        public static void SeedUserRole(DataContext context)
        {
            if(!context.Users.Any())
            {
                var userRoleData = System.IO.File.ReadAllText("SeedData/UserRoleSeedData.json");
                var userRoles = JsonConvert.DeserializeObject<List<UserRole>>(userRoleData);

                foreach (var userRole in userRoles)
                {
                    context.Add(userRole);
                }

                context.SaveChanges();
            }
        }
        public static void SeedComission(DataContext context)
        {
            if(!context.Comissions.Any())
            {
                var comissionData = System.IO.File.ReadAllText("SeedData/ComissionSeedData.json");
                var comissions = JsonConvert.DeserializeObject<Comission[]>(comissionData);

                foreach (var comission in comissions)
                {
                    context.Add(comission);
                }

                context.SaveChanges();
            }
        }

        public static void SeedComissionRanges(DataContext context)
        {
            if(!context.ComissionRanges.Any())
            {
                var comissionRangesData = System.IO.File.ReadAllText("SeedData/ComissionRangesSeedData.json");
                var comissionsRanges = JsonConvert.DeserializeObject<ComissionRange[]>(comissionRangesData);

                foreach (var comissionRange in comissionsRanges)
                {
                    context.Add(comissionRange);
                }

                context.SaveChanges();
            }
        }

        public static void SeedUser(DataContext context)
        {
            if(!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("SeedData/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);

                foreach (var user in users)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("password", out passwordHash, out passwordSalt);

                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.UserName = user.UserName.ToLower();
                    context.Add(user);
                }

                context.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }    
        }
    }
}