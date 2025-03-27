using EBISX_POS.API.Models;
using EBISX_POS.API.Models.Utils;
using ManagerLibrary.Data;
using ManagerLibrary.Data.utils;
using ManagerLibrary.ManagerData;
using Microsoft.EntityFrameworkCore;

namespace EBISX_POS.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DataContext(
                serviceProvider.GetRequiredService<DbContextOptions<DataContext>>()))
            {
                // Look for any data already in the database.
                if (context.User.Any() || context.Category.Any() || context.Menu.Any() || context.Timestamp.Any() || context.Order.Any() || context.Item.Any()  )
                {
                    return;   // DB has been seeded
                }

                var users = new User[]
                {
                    new User { UserEmail = "user1@example.com", UserFName = "John", UserLName = "Doe", UserRole = "Manager" },
                    new User { UserEmail = "user2@example.com", UserFName = "Jane", UserLName = "Doe", UserRole = "Cashier" },
                    new User { UserEmail = "user3@example.com", UserFName = "Alice", UserLName = "Smith", UserRole = "Manager" },
                    new User { UserEmail = "user4@example.com", UserFName = "Bob", UserLName = "Brown", UserRole = "RGM" },
                    new User { UserEmail = "user5@example.com", UserFName = "Charlie", UserLName = "Davis", UserRole = "Cashier" },
                    new User { UserEmail = "user6@example.com", UserFName = "David", UserLName = "Evans", UserRole = "Manager" },
                    new User { UserEmail = "user7@example.com", UserFName = "Eve", UserLName = "Foster", UserRole = "Cashier" },
                    new User { UserEmail = "user8@example.com", UserFName = "Frank", UserLName = "Green", UserRole = "Cashier" },
                    new User { UserEmail = "user9@example.com", UserFName = "Grace", UserLName = "Hill", UserRole = "Cashier" },
                    new User { UserEmail = "user10@example.com", UserFName = "Hank", UserLName = "Ivy", UserRole = "Manager" }
                };
                context.User.AddRange(users);

    
                var drinkTypes = new DrinkType[] {
                    new DrinkType { DrinkTypeName = "Hot" },
                    new DrinkType { DrinkTypeName = "Cold" },
                };
                context.DrinkType.AddRange(drinkTypes);

                var addOnTypes = new AddOnType[]
                {
                    new AddOnType{ AddOnTypeName = "Sides" },
                    new AddOnType{ AddOnTypeName = "Desserts" },
                };
                context.AddOnType.AddRange(addOnTypes);

                var categories = new Category[]
                {
                    new Category { CtgryName = "Burger" },
                    new Category { CtgryName = "Spaghetti" },
                    new Category { CtgryName = "Chickensad" },
                    new Category { CtgryName = "Sandwich" },
                    new Category { CtgryName = "Drinks" },
                    new Category { CtgryName = "Desserts" },
                    new Category { CtgryName = "Fries" },
                };
                context.Category.AddRange(categories);

                var menus = new Menu[]
                {
                    // Burgers
                    new Menu { MenuName = "Cheeseburger", MenuPrice = 4.99m, Category = categories[0], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Cheeseburger", MenuPrice = 5.99m, Category = categories[0]},
                    new Menu { MenuName = "Burger Ka Sakin", MenuPrice = 4.99m, Category = categories[0], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Burger Ka Sakin", MenuPrice = 5.99m, Category = categories[0]},
                    new Menu { MenuName = "Cheese", MenuPrice = 0.99m, Category = categories[0], IsAddOn = true }, // Addon
                    new Menu { MenuName = "Bacon", MenuPrice = 1.49m, Category = categories[0], IsAddOn = true },  // Addon

                    // Spaghetti
                    new Menu { MenuName = "Spaghetti Bolognese", MenuPrice = 6.99m, Category = categories[1], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Spaghetti Bolognese", MenuPrice = 7.99m, Category = categories[1]},
                    new Menu { MenuName = "Spaghetti", MenuPrice = 5.99m, Category = categories[1], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Spaghetti", MenuPrice = 6.99m, Category = categories[1]},
                    new Menu { MenuName = "Spaghetti w/ Chickensad", MenuPrice = 5.99m, Category = categories[1], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Spaghetti w/ Chickensad", MenuPrice = 6.99m, Category = categories[1]},

                    //  Chickensad
                    new Menu { MenuName = "Chickensad", MenuPrice = 5.99m, Category = categories[2], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Chickensad", MenuPrice = 6.99m, Category = categories[2] },
                    new Menu { MenuName = "Chicken Sandwich", MenuPrice = 5.99m, Category = categories[2], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Chicken Sandwich", MenuPrice = 6.99m, Category = categories[2] },
                    new Menu { MenuName = "Grilled Chicken", MenuPrice = 9.99m, Category = categories[2], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Grilled Chicken", MenuPrice = 10.99m, Category = categories[2] },
                    new Menu { MenuName = "Rice", MenuPrice = 1, Category = categories[2], HasDrink = false, HasAddOn = false },

                    // Sandwich
                    new Menu { MenuName = "Club Sandwich", MenuPrice = 5.99m, Category = categories[3], HasDrink = false, HasAddOn = false },
                    new Menu { MenuName = "Club Sandwich", MenuPrice = 6.99m, Category = categories[3] },

                    // Drinks
                    new Menu { MenuName = "Coke", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Coke", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Coke", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Sprite", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Sprite", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Sprite", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Royal", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Royal", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Royal", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Tea", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Tea", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Tea", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[1], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Kape Letse", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Kape Letse", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Kape Letse", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Pinaig na Mais", MenuPrice = 1.99m, Category = categories[4], Size = MenuSize.R.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Pinaig na Mais", MenuPrice = 2.99m, Category = categories[4], Size = MenuSize.M.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },
                    new Menu { MenuName = "Pinaig na Mais", MenuPrice = 3.99m, Category = categories[4], Size = MenuSize.L.ToString(), DrinkType = drinkTypes[0], HasDrink = false, HasAddOn = false, IsAddOn = true },

                    // Desserts
                    new Menu { MenuName = "Ice Cream", MenuPrice = 2.99m, Category = categories[5], Size = MenuSize.R.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },
                    new Menu { MenuName = "Ice Cream", MenuPrice = 3.99m, Category = categories[5], Size = MenuSize.M.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },
                    new Menu { MenuName = "Ice Cream", MenuPrice = 4.99m, Category = categories[5], Size = MenuSize.L.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },
                    new Menu { MenuName = "Durian Pie", MenuPrice = 2.99m, Category = categories[5], Size = MenuSize.R.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },
                    new Menu { MenuName = "Durian Pie", MenuPrice = 3.99m, Category = categories[5], Size = MenuSize.M.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },
                    new Menu { MenuName = "Durian Pie", MenuPrice = 4.99m, Category = categories[5], Size = MenuSize.L.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[1]  },

                    // Fries
                    new Menu { MenuName = "Rice", MenuPrice = 0.49m, Category = categories[6], HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[0] },
                    new Menu { MenuName = "Bisaya Fries", MenuPrice = 2.49m, Category = categories[6], Size = MenuSize.R.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[0]   },
                    new Menu { MenuName = "Bisaya Fries", MenuPrice = 3.49m, Category = categories[6], Size = MenuSize.M.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[0]  },
                    new Menu { MenuName = "Bisaya Fries", MenuPrice = 4.49m, Category = categories[6], Size = MenuSize.L.ToString(), HasDrink = false, HasAddOn = false, IsAddOn = true, AddOnType = addOnTypes[0]  },


                };
                context.Menu.AddRange(menus);


                var branches = new StoreBranch[]
                {
                     new StoreBranch {BranchName = "Guadalupe", BranchAddress = "Guadalupe, Cebu City", VAtRegTin = "12345456423", MinNo= "09123456789", SerialNumber = "SN23100232123", PosNumber="1293023292"},
                     new StoreBranch {BranchName = "Mabolo", BranchAddress = "Mabolo, Cebu City", VAtRegTin = "12345456423", MinNo= "09123456789", SerialNumber = "SN23100232123", PosNumber= "1293023292" },
                     new StoreBranch {BranchName = "Lapu-Lapu", BranchAddress = "Lapu-Lapu, Cebu City", VAtRegTin = "12345456423", MinNo= "09123456789", SerialNumber = "SN23100232123",PosNumber="0293212323" },
                };
                context.StoreBranch.AddRange(branches);
                context.SaveChanges();

                var orders = new Order[]
                {
                     new Order { OrderType = "Dine-In", TotalAmount = 20.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[1], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Takeaway", TotalAmount = 15.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[2], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Dine-In", TotalAmount = 25.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[3], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Takeaway", TotalAmount = 30.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[4], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Dine-In", TotalAmount = 35.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[5], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Takeaway", TotalAmount = 40.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[6], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Dine-In", TotalAmount = 45.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[7], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Takeaway", TotalAmount = 50.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[8], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Dine-In", TotalAmount = 55.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[9], BranchId = branches[0].BranchId },
                     new Order { OrderType = "Takeaway", TotalAmount = 60.00m, CreatedAt = DateTimeOffset.Now, Cashier = users[0], BranchId = branches[0].BranchId }
                };
                context.Order.AddRange(orders);
                context.SaveChanges();

                var Receipts = new Invoice[]
                {
                    new Invoice { InvoiceNumber = "INV-0001", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[0].Id, ReceiptType = ReceiptType.Regular, Cashier = users[1], BranchId = branches[0].BranchId , CashInDrawer = 2000m   },
                    new Invoice { InvoiceNumber = "INV-0002", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[1].Id, ReceiptType = ReceiptType.SeniorCitizen,  Cashier = users[2],BranchId = branches[0].BranchId, CashInDrawer = 2000m },
                    new Invoice { InvoiceNumber = "INV-0003", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[2].Id, ReceiptType = ReceiptType.PWD,  Cashier = users[3], BranchId = branches[0].BranchId, CashInDrawer = 2000m },
                    new Invoice { InvoiceNumber = "INV-0004", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[3].Id, ReceiptType = ReceiptType.Regular,  Cashier = users[4], BranchId = branches[0].BranchId, CashInDrawer = 2000m},
                    new Invoice { InvoiceNumber = "INV-0005", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[4].Id, ReceiptType = ReceiptType.SeniorCitizen,  Cashier = users[5], BranchId = branches[0].BranchId, CashInDrawer = 2000m },
                    new Invoice { InvoiceNumber = "INV-0006", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[5].Id, ReceiptType = ReceiptType.PWD, Cashier = users[6], BranchId = branches[0].BranchId , CashInDrawer = 2000m},
                    new Invoice { InvoiceNumber = "INV-0007", ReceiptDate = DateTimeOffset.Now, ReceiptTime = DateTimeOffset.Now, OrderId = orders[6].Id, ReceiptType = ReceiptType.Regular,  Cashier = users[7], BranchId = branches[0].BranchId , CashInDrawer = 2000m },
    
                };
                context.Invoice.AddRange(Receipts);
                context.SaveChanges();

                
            }
        }
    }
}
