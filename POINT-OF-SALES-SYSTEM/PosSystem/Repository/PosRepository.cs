using PosSystem.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using User = PosSystem.Model.User;

namespace PosSystem.Repository
{
    public class PosRepository
    {
        OleDbConnection _connection = new OleDbConnection();

        private string connectionString = ConfigurationManager.ConnectionStrings["AccessDbConnection"].ConnectionString;
        private string _inventoryThreshold = ConfigurationManager.AppSettings["InventoryThreshold"]; 
        private string _recordRetentionDays = ConfigurationManager.AppSettings["RecordRetentionDays"];

        public PosRepository()
        {
            _connection = new OleDbConnection(connectionString);
        }

        #region USERS       

        public int InsertUser(User user)
        {
            var sql = @"INSERT INTO Users (user_name, first_name, last_name, role, [password], is_active) 
                            VALUES (@userName, @firstName, @lastName, @role, @password, True)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@userName", user.UserName);
                    command.Parameters.AddWithValue("@firstName", user.FirstName);
                    command.Parameters.AddWithValue("@lastName", user.LastName);
                    command.Parameters.AddWithValue("@role", Convert.ToInt16(user.Role));
                    command.Parameters.AddWithValue("@password", user.Password);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int UpdateUser(User user)
        {
            var sql = "UPDATE Users SET first_name = '" + user.FirstName + "', last_name = '" + user.LastName + "', " +
                "[password] = '" + user.Password + "', role = " + user.Role + ", is_active = " + user.IsActive +
                            " WHERE user_name = '" + user.UserName + "'";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public User GetUserByUserName(string userName)
        {
            var sql = @"SELECT u.id, user_name, first_name, last_name, role, type, password, is_active 
                        FROM users u INNER JOIN roles r ON u.role = r.id 
                        WHERE user_name = @userName";
            User user = null;

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);                   
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("user_name"))))
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UserName = reader.GetString(reader.GetOrdinal("user_name")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                Role = reader.GetInt16(reader.GetOrdinal("role")),
                                RoleType = reader.GetString(reader.GetOrdinal("type")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"))
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return user;
        }

        public User GetUserByUserNameAndPassword(string userName, string passWord)
        {
            var sql = @"SELECT u.id, user_name, first_name, last_name, role, type, password, is_active 
                        FROM users u INNER JOIN roles r ON u.role = r.id 
                        WHERE user_name = @userName AND password = @passWord";
            User user = null;

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@passWord", passWord);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("user_name"))))
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UserName = reader.GetString(reader.GetOrdinal("user_name")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                Role = reader.GetInt16(reader.GetOrdinal("role")),
                                RoleType = reader.GetString(reader.GetOrdinal("type")),
                                Password = reader.GetString(reader.GetOrdinal("password")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"))
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return user;
        }

        public List<User> GetUsers()
        {
            var sql = "SELECT * FROM Users";

            List<User> users = new List<User>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                UserName = reader.GetString(reader.GetOrdinal("user_name")),
                                FirstName = reader.GetString(reader.GetOrdinal("first_name")),
                                LastName = reader.GetString(reader.GetOrdinal("last_name")),
                                IsActive = reader.GetBoolean(reader.GetOrdinal("is_active"))
                            };

                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return users;
        }

        public List<Role> GetRoleTypes()
        {
            var sql = "SELECT ID, Type FROM Roles";

            List<Role> roleTypes = new List<Role>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var type = new Role
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("ID")),
                                Type = reader.GetString(reader.GetOrdinal("Type"))
                            };

                            roleTypes.Add(type);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return roleTypes;
        }

        public int DeleteUser(string userName)
        {
            var sql = @"DELETE FROM Users WHERE user_name = @userName";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        #endregion USERS

        #region ITEMS
        public Item GetItemByItemCode(string itemCode)
        {
            var sql = @"SELECT itemcode, description, units, tax_type_id, category_name, 
                            inventory_qty, retail  
                            FROM items WHERE itemcode = @itemCode";
            Item item = null;

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("itemcode"))))
                        {
                            item = new Item
                            {
                                ItemCode = reader.GetString(reader.GetOrdinal("ITEMCODE")),
                                Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                                //LongDescription = reader.GetString(reader.GetOrdinal("LONG_DESCRIPTION")),
                                //MaterialCost = Convert.ToDecimal(reader["MATERIAL_COST"] ?? 0.00),
                                //QuickPicks = reader.GetInt16(reader.GetOrdinal("QUICK_PICKS")),
                                //CategoryId = reader.GetInt16(reader.GetOrdinal("CATEGORY_ID")),
                                //Editable = reader.GetBoolean(reader.GetOrdinal("EDITABLE")),
                                //Inactive = reader.GetBoolean(reader.GetOrdinal("INACTIVE")),
                                Units = reader.GetString(reader.GetOrdinal("UNITS")),
                                //MbFlag = reader.GetString(reader.GetOrdinal("MB_FLAG")),
                                //ImageFilename = reader.GetString(reader.GetOrdinal("IMAGE_FILENAME")),
                                //SalesAccount = reader.GetInt16(reader.GetOrdinal("SALES_ACCOUNT")),
                                //CogsAccount = reader.GetInt16(reader.GetOrdinal("COGS_ACCOUNT")),
                                //InventoryAccount = reader.GetInt16(reader.GetOrdinal("INVENTORY_ACCOUNT")),
                                //AdjustmentAccount = reader.GetInt16(reader.GetOrdinal("ADJUSTMENT_ACCOUNT")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("TAX_TYPE_ID")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME")),
                                InventoryQuantity = reader.GetInt16(reader.GetOrdinal("INVENTORY_QTY")),
                                //InventoryLocation = reader.GetString(reader.GetOrdinal("INVENTORY_LOCATION")),
                                Retail = Convert.ToDecimal(reader["Retail"] ?? 0.00),
                                //Wholesale = Convert.ToDecimal(reader["Wholesale"] ?? 0.00),
                                //Employee = reader.GetInt16(reader.GetOrdinal("Employee"))
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return item;
        }

        public List<Item> GetItemByDescription(string description)
        {
            var sql = @"SELECT itemcode, description, units, tax_type_id, 
                        category_name, inventory_qty, retail 
                        FROM items WHERE description LIKE '%" + @description + "%'";
            var allItems = new List<Item>();
            string itemCOdeTemp = "";
            try
            {
                _connection.Open();
                
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@description", description);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            itemCOdeTemp = reader.GetString(reader.GetOrdinal("ITEMCODE"));
                            Item item = new Item
                            {
                                
                                ItemCode = reader.GetString(reader.GetOrdinal("ITEMCODE")),
                                Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                                //LongDescription = reader.GetString(reader.GetOrdinal("LONG_DESCRIPTION")),
                                //MaterialCost = Convert.ToDecimal(reader["MATERIAL_COST"] ?? 0.00),
                                //QuickPicks = reader.GetInt16(reader.GetOrdinal("QUICK_PICKS")),
                                //CategoryId = reader.GetInt16(reader.GetOrdinal("CATEGORY_ID")),
                                //Editable = reader.GetBoolean(reader.GetOrdinal("EDITABLE")),
                                //Inactive = reader.GetBoolean(reader.GetOrdinal("INACTIVE")),
                                Units = reader.GetString(reader.GetOrdinal("UNITS")),
                                //MbFlag = reader.GetString(reader.GetOrdinal("MB_FLAG")),
                                //ImageFilename = reader.GetString(reader.GetOrdinal("IMAGE_FILENAME")),
                                //SalesAccount = reader.GetInt16(reader.GetOrdinal("SALES_ACCOUNT")),
                                //CogsAccount = reader.GetInt16(reader.GetOrdinal("COGS_ACCOUNT")),
                                //InventoryAccount = reader.GetInt16(reader.GetOrdinal("INVENTORY_ACCOUNT")),
                                //AdjustmentAccount = reader.GetInt16(reader.GetOrdinal("ADJUSTMENT_ACCOUNT")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("TAX_TYPE_ID")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME")),
                                InventoryQuantity = reader.GetInt16(reader.GetOrdinal("INVENTORY_QTY")),
                                //InventoryLocation = reader.GetString(reader.GetOrdinal("INVENTORY_LOCATION")),
                                Retail = Convert.ToDecimal(reader["Retail"] ?? 0.00),
                                //Wholesale = Convert.ToDecimal(reader["Wholesale"] ?? 0.00),
                                //Employee = reader.GetInt16(reader.GetOrdinal("Employee"))
                            };
                            allItems.Add(item);
                            
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                var x = itemCOdeTemp;
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return allItems;
        }

        public List<Item> GetAllItems()
        {
            var sql = @"SELECT itemcode, description, units, tax_type_id, 
                        category_name, inventory_qty, retail  
                        FROM items";
            var allItems = new List<Item>();

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                ItemCode = reader.GetString(reader.GetOrdinal("ITEMCODE")),
                                Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                                //LongDescription = reader.GetString(reader.GetOrdinal("LONG_DESCRIPTION")),
                                //MaterialCost = Convert.ToDecimal(reader["MATERIAL_COST"] ?? 0.00),
                                //QuickPicks = reader.GetInt16(reader.GetOrdinal("QUICK_PICKS")),
                                //CategoryId = reader.GetInt16(reader.GetOrdinal("CATEGORY_ID")),
                                //Editable = reader.GetBoolean(reader.GetOrdinal("EDITABLE")),
                                //Inactive = reader.GetBoolean(reader.GetOrdinal("INACTIVE")),
                                Units = reader.GetString(reader.GetOrdinal("UNITS")),
                                //MbFlag = reader.GetString(reader.GetOrdinal("MB_FLAG")),
                                //ImageFilename = reader.GetString(reader.GetOrdinal("IMAGE_FILENAME")),
                                //SalesAccount = reader.GetInt16(reader.GetOrdinal("SALES_ACCOUNT")),
                                //CogsAccount = reader.GetInt16(reader.GetOrdinal("COGS_ACCOUNT")),
                                //InventoryAccount = reader.GetInt16(reader.GetOrdinal("INVENTORY_ACCOUNT")),
                                //AdjustmentAccount = reader.GetInt16(reader.GetOrdinal("ADJUSTMENT_ACCOUNT")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("TAX_TYPE_ID")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME")),
                                InventoryQuantity = reader.GetInt16(reader.GetOrdinal("INVENTORY_QTY")),
                                //InventoryLocation = reader.GetString(reader.GetOrdinal("INVENTORY_LOCATION")),
                                Retail = Convert.ToDecimal(reader["Retail"] ?? 0.00),
                                //Wholesale = Convert.ToDecimal(reader["Wholesale"] ?? 0.00),
                                //Employee = reader.GetInt16(reader.GetOrdinal("Employee"))
                            };

                            allItems.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return allItems;
        }

        public Item GetItemByItemCodeItemManagement(string itemCode)
        {
            var sql = @"SELECT itemcode, description, material_cost, units, tax_type_id, category_name, inventory_qty, retail, inventory_sold  
                            FROM items WHERE itemcode = @itemCode";
            Item item = null;

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("itemcode"))))
                        {
                            item = new Item
                            {
                                ItemCode = reader.GetString(reader.GetOrdinal("ITEMCODE")),
                                Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                                MaterialCost = Convert.ToDecimal(reader["MATERIAL_COST"] ?? 0.00),
                                Units = reader.GetString(reader.GetOrdinal("UNITS")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("TAX_TYPE_ID")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME")),
                                InventoryQuantity = reader["INVENTORY_QTY"] != DBNull.Value
                                        ? Convert.ToInt16(reader["INVENTORY_QTY"]) : 0,
                                Retail = Convert.ToDecimal(reader["Retail"] ?? 0.00),
                                InventorySold = reader["INVENTORY_SOLD"] != DBNull.Value
                                        ? Convert.ToInt16(reader["INVENTORY_SOLD"]) : 0,
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return item;
        }

        public int InsertItem(Item item)
        {
            var sql = @"INSERT INTO Items (itemcode, description, long_description, material_cost, 
                                            units, retail, category_name, inventory_qty, tax_type_id) 
                            VALUES (@itemCode, @description, @description, @materialCost, @unit, 
                                    @price, @categoryName, @inventoryQuantity, @taxTypeId)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", item.ItemCode);
                    command.Parameters.AddWithValue("@description", item.Description);
                    command.Parameters.AddWithValue("@materialCost", item.MaterialCost);
                    command.Parameters.AddWithValue("@unit", item.Units);
                    command.Parameters.AddWithValue("@price", item.Retail);
                    command.Parameters.AddWithValue("@categoryName", item.CategoryName);
                    command.Parameters.AddWithValue("@inventoryQuantity", item.InventoryQuantity);
                    command.Parameters.AddWithValue("@taxTypeId", item.TaxTypeId);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int UpdateItem(Item item)
        {
            var sql = @"UPDATE Items SET 
                        itemcode = @itemCode, 
                        description = @description,
                        long_description = @description, 
                        material_cost  = @materialCost, 
                        units = @unit, 
                        retail = @price, 
                        category_name = @categoryName, 
                        inventory_qty = @inventoryQuantity, 
                        tax_type_id = @taxTypeId 
                        WHERE itemcode = @itemCode";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", item.ItemCode.Trim());
                    command.Parameters.AddWithValue("@description", item.Description);
                    command.Parameters.AddWithValue("@materialCost", item.MaterialCost);
                    command.Parameters.AddWithValue("@unit", item.Units);
                    command.Parameters.AddWithValue("@price", item.Retail);
                    command.Parameters.AddWithValue("@categoryName", item.CategoryName);
                    command.Parameters.AddWithValue("@inventoryQuantity", item.InventoryQuantity);
                    command.Parameters.AddWithValue("@taxTypeId", item.TaxTypeId);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int DeleteItem(string itemCode)
        {
            var sql = @"DELETE FROM Items 
                        WHERE itemcode = @itemCode";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode.Trim());
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int UpdateItemInventory(string itemCode, int quantity)
        {
            var sql = $@"UPDATE Items 
                        SET inventory_qty = inventory_qty - {quantity},
                        inventory_sold = inventory_sold + {quantity}
                        WHERE itemcode = @itemcode";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public List<Item> GetLowInventory(bool includeZero)
        {
            var withZero = includeZero ? "" : " AND inventory_qty > 0";
            var sql = @"SELECT itemcode, description, category_name, material_cost, retail, inventory_qty 
                        FROM items WHERE inventory_qty <= " + _inventoryThreshold + withZero + " ORDER BY inventory_qty DESC";

            var items = new List<Item>();

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Item item = new Item
                            {
                                ItemCode = reader.GetString(reader.GetOrdinal("ITEMCODE")),
                                Description = reader.GetString(reader.GetOrdinal("DESCRIPTION")),
                                CategoryName = reader.GetString(reader.GetOrdinal("CATEGORY_NAME")),
                                InventoryQuantity = reader.GetInt16(reader.GetOrdinal("INVENTORY_QTY")),
                                MaterialCost = reader.GetDecimal(reader.GetOrdinal("MATERIAL_COST")),
                                Retail = Convert.ToDecimal(reader["Retail"] ?? 0.00),
                            };

                            items.Add(item);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return items;
        }

        public List<ItemSalesUser> GetItemSalesUsersByItemCode(string itemCode)
        {
            var sql = @"SELECT c.sale_date, c.trans_num, c.quantity, u.user_name
                        FROM Cart c
                        INNER JOIN users u ON c.cashier = u.id
                        WHERE item_code = @itemCode AND status = 2
                        ORDER BY sale_date";

            var itemSalesUsers = new List<ItemSalesUser>();

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemSalesUser itemSalesUser = new ItemSalesUser
                            {
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                Cashier = reader.GetString(reader.GetOrdinal("user_name"))
                            };

                            itemSalesUsers.Add(itemSalesUser);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return itemSalesUsers;
        }

        #endregion ITEMS

        #region ITEMENTRYHISTORY

        public int InsertItemEntryHistory(ItemEntryHistory itemEntryHistory)
        {
            var sql = @"INSERT INTO Item_Entry_History (item_code, material_cost, price, quantity, updated_by, remarks, date_time) 
                            VALUES (@itemCode, @materialCost, @price, @quantity, @updatedBy, @remarks, NOW())";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemEntryHistory.ItemCode);
                    command.Parameters.AddWithValue("@materialCost", itemEntryHistory.MaterialCost);
                    command.Parameters.AddWithValue("@price", itemEntryHistory.Price);
                    command.Parameters.AddWithValue("@quantity", itemEntryHistory.Quantity);
                    command.Parameters.AddWithValue("@updatedBy", itemEntryHistory.UpdatedBy);
                    command.Parameters.AddWithValue("@remarks", itemEntryHistory.Remarks);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public List<ItemEntryHistory> GetItemEntryHistoryByItemCode(string itemCode, string dateFrom, string dateTo)
        {
            var sql = $@"SELECT item_code, material_cost, price, 
                            quantity, date_time, u.user_name, remarks
                            FROM Item_Entry_History h 
                            LEFT JOIN Users u ON u.id = h.updated_by
                            WHERE item_code = @itemCode
                            AND date_time between #{dateFrom}# AND #{dateTo}#
                            ORDER BY date_time ASC";

            var listItemEntryHistory = new List<ItemEntryHistory>();

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ItemEntryHistory itemEntryHistory = new ItemEntryHistory
                            {
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                MaterialCost = Convert.ToDecimal(reader["material_cost"] ?? 0.00),
                                Price = Convert.ToDecimal(reader["price"] ?? 0.00),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("date_time")),
                                UserName = reader.GetString(reader.GetOrdinal("user_name")),
                                Remarks = !reader.IsDBNull(reader.GetOrdinal("remarks"))
                                            ? reader.GetString(reader.GetOrdinal("remarks")) : string.Empty
                            };

                            if (!string.IsNullOrWhiteSpace(itemEntryHistory.Remarks))
                            {
                                itemEntryHistory.UserName += $" [ {itemEntryHistory.Remarks} ]"; 
                            }

                            listItemEntryHistory.Add(itemEntryHistory);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return listItemEntryHistory;
        }

        public DateTime GetEarliestItemEntry(string itemCode)
        {
            var sql = @"SELECT TOP 1 date_time
                        FROM Item_Entry_History 
                        WHERE item_code = @itemCode
                        ORDER BY date_time ASC";

            var earliestDate = DateTime.Now;

            try
            {
                _connection.Open();

                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            earliestDate = reader.GetDateTime(reader.GetOrdinal("date_time"));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return earliestDate;
        }

        #endregion ITEMENTRYHISTORY

        #region CART

        public Cart GetCartRecordByTransNumAndItemCode(string transNum, string itemCode)
        {
            var sql = @"SELECT c.id, c.trans_num, c.item_code, i.description, i.retail, quantity, 
                        d.discount, status, sale_date, cashier, u.first_name AS cashier_fname, 
                        i.tax_type_id, u.last_name AS cashier_lname 
                        FROM (((Cart c INNER JOIN Items i ON c.item_code = i.itemcode) 
                        LEFT JOIN Users u ON c.cashier = u.id) 
                        LEFT JOIN Discounts d ON c.trans_num = d.trans_num AND c.item_code = d.item_code) 
                        WHERE c.trans_num = @transNum AND c.item_code = @itemCode";

            Cart cartRecord = null;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("trans_num"))))
                        {
                            cartRecord = new Cart
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = Math.Round(Convert.ToDecimal(reader["retail"] ?? 0.00), 2),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                Discount = Math.Round(Convert.ToDecimal(reader["discount"] != DBNull.Value
                                                ? reader["discount"] : 0), 2),
                                Status = reader.GetInt16(reader.GetOrdinal("status")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("tax_type_id")),
                                Cashier = reader.GetInt16(reader.GetOrdinal("cashier")),
                                CashierFirstName = !reader.IsDBNull(reader.GetOrdinal("cashier_fname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_fname")) : string.Empty,
                                CashierLastName = !reader.IsDBNull(reader.GetOrdinal("cashier_lname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_lname")) : string.Empty
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return cartRecord;
        }

        public int GetTransactionStatusByTransNum(string transNum)
        {
            var sql = @"SELECT status FROM Cart WHERE trans_num = '" + transNum.Trim() + "'";

            int status = 1;
            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            status = reader.GetInt16(reader.GetOrdinal("status"));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return status;
        }

        public List<Cart> GetCartRecordByTransNumAndStatus(string transNum, int status)
        {
            var sql = @"SELECT c.id, c.trans_num, c.item_code, i.description, i.retail, quantity, d.discount, 
                        status, sale_date, cashier, u.first_name AS cashier_fname, 
                        i.tax_type_id, u.last_name AS cashier_lname 
                        FROM (((Cart c INNER JOIN Items i ON c.item_code = i.itemcode) 
                        LEFT JOIN Users u ON c.cashier = u.id) 
                        LEFT JOIN Discounts d ON c.trans_num = d.trans_num AND c.item_code = d.item_code) 
                        WHERE c.trans_num LIKE '" + transNum + "' AND c.status = " + status + " ORDER BY c.id";

            var cartRecords = new List<Cart>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cart cartRecord = new Cart
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = Math.Round(Convert.ToDecimal(reader["retail"] ?? 0.00), 2),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                Discount = Math.Round(Convert.ToDecimal(reader["discount"] != DBNull.Value
                                                ? reader["discount"] : 0), 2),
                                Status = reader.GetInt16(reader.GetOrdinal("status")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("tax_type_id")),
                                Cashier = reader.GetInt16(reader.GetOrdinal("cashier")),
                                CashierFirstName = !reader.IsDBNull(reader.GetOrdinal("cashier_fname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_fname")) : string.Empty,
                                CashierLastName = !reader.IsDBNull(reader.GetOrdinal("cashier_lname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_lname")) : string.Empty
                            };

                            cartRecords.Add(cartRecord);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return cartRecords;
        }

        public Cart GetCartRecordById(int cartId)
        {
            var sql = @"SELECT c.id, c.trans_num, c.item_code, i.description, i.retail, quantity, d.discount, 
                        status, sale_date, cashier, u.first_name AS cashier_fname, 
                        i.tax_type_id, u.last_name AS cashier_lname 
                        FROM (((Cart c INNER JOIN Items i ON c.item_code = i.itemcode) 
                        LEFT JOIN Users u ON c.cashier = u.id) 
                        LEFT JOIN Discounts d ON c.trans_num = d.trans_num AND c.item_code = d.item_code) 
                        WHERE c.id = " + cartId + " ORDER BY c.id";

            Cart cartRecord = null;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartRecord = new Cart
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = Math.Round(Convert.ToDecimal(reader["retail"] ?? 0.00), 2),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                Discount = Math.Round(Convert.ToDecimal(reader["discount"] != DBNull.Value
                                                ? reader["discount"] : 0), 2),
                                Status = reader.GetInt16(reader.GetOrdinal("status")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                                TaxTypeId = reader.GetInt16(reader.GetOrdinal("tax_type_id")),
                                Cashier = reader.GetInt16(reader.GetOrdinal("cashier")),
                                CashierFirstName = !reader.IsDBNull(reader.GetOrdinal("cashier_fname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_fname")) : string.Empty,
                                CashierLastName = !reader.IsDBNull(reader.GetOrdinal("cashier_lname"))
                                                    ? reader.GetString(reader.GetOrdinal("cashier_lname")) : string.Empty
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return cartRecord;
        }

        public int UpdateCartQuantity(int Id, int quantity)
        {
            var sql = "UPDATE Cart SET quantity = " + quantity + " WHERE id = " + Id;
            int recordsUpdated = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    recordsUpdated = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return recordsUpdated;
        }

        public void InsertCartRecord(string transNum, string itemCode, int quantity, int cashier)
        {
            var sql = "INSERT INTO Cart (trans_num, item_code, quantity, sale_date, cashier, status) " +
                "VALUES (@transNum, @itemCode, @quantity, Now(), @cashier, 1)";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transnum", transNum);
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@cashier", cashier);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteCartRecord(int id)
        {
            var sql = "DELETE FROM Cart WHERE id = @id";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                _connection.Close();
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteCartByTransactionNumber(string transNum)
        {
            var sql = "DELETE FROM Cart WHERE trans_num = @transNum";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum.Trim());
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateCartStatus(int id, int status)
        {
            var sql = "UPDATE Cart SET status = " + status + " WHERE id = " + id;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public string GetLastTransactionNumber()
        {
            string transDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            var sql = "SELECT TOP 1 trans_num FROM cart WHERE trans_num LIKE '" + transDateTime + "%' ORDER BY id DESC";
            string transNum;
            //int count;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read() && !string.IsNullOrWhiteSpace(reader.GetString(reader.GetOrdinal("trans_num"))))
                        {
                            transNum = reader.GetString(reader.GetOrdinal("trans_num"));
                            //count = int.Parse(transNum.Substring(8, 4));
                            //transNum = transDateTime + (count + 1);
                        }
                        //else
                        //{
                        //    transNum = transDateTime + "1001"; 
                        //}
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return transDateTime;
        }

        public List<Purchase> GetCartByStatusLight(int status)
        {
            string statusClause = string.Empty;

            if (status > 0)
            {
                statusClause = "AND c.status = " + status;
            }

            var dateToday = DateTime.Now.ToString("yyyyMMdd");

            var sql = $@"SELECT SUM(quantity) AS quantity, c.trans_num, SUM(i.retail * quantity) AS price, c.status, MIN(c.sale_date) AS saledate
                        FROM Cart c INNER JOIN Items i ON c.item_code = i.itemcode                                                    
                        WHERE c.trans_num LIKE '{dateToday}%' {statusClause} GROUP BY trans_num, status ORDER BY trans_num DESC";

            List<Purchase> purchases = new List<Purchase>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var purchase = new Purchase
                            {
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                Quantity = Convert.ToInt16(reader.GetDouble(reader.GetOrdinal("quantity"))),
                                Price = reader.GetDecimal(reader.GetOrdinal("price")),
                                Status = reader.GetInt16(reader.GetOrdinal("status")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("saledate"))
                            };

                            purchases.Add(purchase);
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return purchases;
        }

        public void DeleteOldCartRecords()
        {
            var sql = $"DELETE FROM Cart WHERE sale_date <= DateAdd('d', -{_recordRetentionDays}, Date())";
            
            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion CART

        #region DISCOUNT

        public Discount GetDiscountByCartId(int cartId)
        {
            var sql = @"SELECT d.id, d.trans_num, d.item_code, d.discount, d.discount_type, t.description, date_time 
                        FROM ((discounts d 
                        INNER JOIN cart c ON d.trans_num = c.trans_num AND d.item_code = c.item_code )
                        INNER JOIN Discount_Type t ON t.id = d.discount_type)
                        WHERE c.id = " + cartId;

            Discount discount = null;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            discount = new Discount
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                DiscountPercent = reader.GetDecimal(reader.GetOrdinal("discount")),
                                DiscountType = reader.GetInt16(reader.GetOrdinal("discount_type")),
                                DiscountTypeDesc = reader.GetString(reader.GetOrdinal("description"))
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return discount;
        }

        public DiscountType GetDiscountTypeByDiscountId(int discountId)
        {
            var sql = @"SELECT id, description FROM Discount_Type WHERE id = " + discountId;

            DiscountType discountType = null;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            discountType = new DiscountType
                            {
                                DiscountId = reader.GetInt32(reader.GetOrdinal("id")),
                                DiscountDesc = reader.GetString(reader.GetOrdinal("description")),
                            };
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return discountType;
        }

        public int UpdateDiscount(int cartId, decimal discount)
        {
            var sql = @"UPDATE discounts d 
                        INNER JOIN cart c ON d.trans_num = c.trans_num AND d.item_code = c.item_code 
                        SET d.discount = " + discount + " WHERE c.id = " + cartId;

            int recordsUpdated = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    recordsUpdated = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return recordsUpdated;
        }

        public void InsertDiscount(string transNum, string itemCode, decimal discount, int discountType)
        {
            var sql = "INSERT into Discounts (trans_num, item_code, discount, discount_type, date_time) " +
                "VALUES (@transNum, @itemCode, @discount, @discountType, Now())";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transnum", transNum);
                    command.Parameters.AddWithValue("@itemCode", itemCode);
                    command.Parameters.AddWithValue("@discount", discount);
                    command.Parameters.AddWithValue("@discountType", discountType);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteDiscountsByTransactionNumber(string transNum)
        {
            var sql = "DELETE FROM Discounts WHERE trans_num = @transNum";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum.Trim());
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public int InsertDiscountType(string discountType)
        {
            var sql = "INSERT INTO Discount_Type (Description) VALUES (@description)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@description", discountType);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public List<DiscountType> GetDiscountTypes()
        {
            var sql = "SELECT ID, Description FROM Discount_Type";

            List<DiscountType> discountTypes = new List<DiscountType>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var discountType = new DiscountType
                            {
                                DiscountId = reader.GetInt32(reader.GetOrdinal("ID")),
                                DiscountDesc = reader.GetString(reader.GetOrdinal("Description"))
                            };

                            discountTypes.Add(discountType);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return discountTypes;
        }

        public void DeleteOldDiscountRecords()
        {
            var sql = $"DELETE FROM Discounts WHERE date_time <= DateAdd('d', -{_recordRetentionDays}, Date())";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion DISCOUNT

        #region SALES

        public void InsertSales(Sales sales)
        {
            var sql = @"INSERT INTO Sales (trans_num, item_code, description, price, quantity, 
                                        vat_percent, vat_amount, discount_percent, discount_amount, 
                                        discount_type, total, cashier, date_time) 
                                VALUES (@transNum, @itemCode, @description, @price, @quantity, 
                                        @vatPercent, @vatAmount, @discountPercent, @discountAmount, 
                                        @discountType, @total, @cashier, NOW())";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", sales.TransNum);
                    command.Parameters.AddWithValue("@itemCode", sales.ItemCode);
                    command.Parameters.AddWithValue("@description", sales.Description);
                    command.Parameters.AddWithValue("@price", sales.Price);
                    command.Parameters.AddWithValue("@quantity", sales.Quantity);
                    command.Parameters.AddWithValue("@vatPercent", sales.VatPercent);
                    command.Parameters.AddWithValue("@vatAmount", sales.VatAmount);
                    command.Parameters.AddWithValue("@discountPercent", sales.DiscountPercent);
                    command.Parameters.AddWithValue("@discountAmount", sales.DiscountAmount);
                    command.Parameters.AddWithValue("@discountType", sales.DiscountDescription);
                    command.Parameters.AddWithValue("@total", sales.Total);
                    command.Parameters.AddWithValue("@cashier", sales.Cashier);
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public List<Purchase> GetPurchaseByTransNumAndStatus(string transNum, int status)
        {
            string statusClause = string.Empty;

            if (status > 0)
            {
                statusClause = "AND c.status = " + status;
            }

            var sql = $@"SELECT c.id, c.trans_num, c.item_code, i.description, i.retail, quantity, 
                        d.discount, d.discount_type, i.tax_type_id,
                        status, cashier, u.first_name AS cashier_fname, 
                        u.last_name AS cashier_lname,  sale_date 
                        FROM (((Cart c INNER JOIN Items i ON c.item_code = i.itemcode) 
                        INNER JOIN Users u ON c.cashier = u.id) 
                        LEFT JOIN Discounts d ON c.trans_num = d.trans_num AND c.item_code = d.item_code) 
                        WHERE c.trans_num = @transNum {statusClause} ORDER BY c.id";

            List<Purchase> purchases = new List<Purchase>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var purchase = new Purchase
                            {
                                CartId = reader.GetInt32(reader.GetOrdinal("id")),
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = Convert.ToDecimal(reader["retail"]),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                DiscountPercent = Convert.ToDecimal(reader["discount"] != DBNull.Value
                                                ? reader["discount"] : 0),
                                DiscountType = Convert.ToInt16(reader["discount_type"] != DBNull.Value
                                                ? reader["discount_type"] : 0),
                                Vattable = reader.GetInt16(reader.GetOrdinal("tax_type_id")) == 1 ? "Yes" : "No",
                                Status = reader.GetInt16(reader.GetOrdinal("status")),
                                CashierId = reader.GetInt16(reader.GetOrdinal("cashier")),
                                CashierFName = reader.GetString(reader.GetOrdinal("cashier_fname")),
                                CashierLName = reader.GetString(reader.GetOrdinal("cashier_lname")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                            };

                            purchases.Add(purchase);
                        }
                    }
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return purchases;
        }

        public List<Sales> GetSales(string dateFrom, string dateTo, string cashier = "")
        {
            string cashierClause = (string.IsNullOrWhiteSpace(cashier) || cashier.ToLower() == "all") ? "" : "AND LCase(cashier) = '" + cashier.ToLower().Trim() + "'";
            var sql = "SELECT * FROM Sales WHERE date_time between #" + dateFrom + "# AND #" + dateTo + "# " + cashierClause + " ORDER BY ID";

            List<Sales> sales = new List<Sales>();

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sale = new Sales
                            {
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                ItemCode = reader.GetString(reader.GetOrdinal("item_code")),
                                Description = reader.GetString(reader.GetOrdinal("description")),
                                Price = Convert.ToDecimal(reader["price"]),
                                Quantity = reader.GetInt16(reader.GetOrdinal("quantity")),
                                VatPercent = Convert.ToDecimal(reader["vat_percent"]),
                                VatAmount = Convert.ToDecimal(reader["vat_amount"]),
                                DiscountPercent = Convert.ToDecimal(reader["discount_percent"]),
                                DiscountAmount = Convert.ToDecimal(reader["discount_amount"]),
                                DiscountDescription = reader.GetString(reader.GetOrdinal("discount_type")),
                                Total = Convert.ToDecimal(reader["total"]),
                                Cashier = reader.GetString(reader.GetOrdinal("cashier")),
                                SaleDate = reader.GetDateTime(reader.GetOrdinal("date_time"))
                            };

                            sales.Add(sale);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return sales;
        }

        public List<int> GetSalesInvoiceNum()
        {
            var dateFrom = DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00";
            var dateTo = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";
            List<int> salesInvoice = new List<int>();

            var sql = $"SELECT COUNT(*) AS transNum FROM Sales WHERE date_time between #{dateFrom}# AND #{dateTo}# GROUP BY trans_num";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int invoice;
                            invoice = reader.GetInt32(reader.GetOrdinal("transNum"));
                            salesInvoice.Add(invoice);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return salesInvoice;
        }

        public void DeleteOldSalesRecords()
        {
            var sql = $"DELETE FROM Sales WHERE date_time <= DateAdd('d', -{_recordRetentionDays}, Date())";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion SALES

        #region NONCASH

        public int InsertSalesGCash(string transNum, string cellNum, string referenceNum)
        {
            var sql = $@"INSERT INTO Sales_GCash (trans_num, cell_num, reference_num) 
                            VALUES (@transNum, @cellNum, @referenceNum)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    command.Parameters.AddWithValue("@cellNum", cellNum);
                    command.Parameters.AddWithValue("@referenceNum", referenceNum);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int InsertSalesMaya(string transNum, string cellNum, string referenceNum)
        {
            var sql = $@"INSERT INTO Sales_Maya (trans_num, cell_num, reference_num) 
                            VALUES (@transNum, @cellNum, @referenceNum)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    command.Parameters.AddWithValue("@cellNum", cellNum);
                    command.Parameters.AddWithValue("@referenceNum", referenceNum);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public int InsertSalesCard(string transNum, string cardType, string last4Digit)
        {
            var sql = $@"INSERT INTO Sales_Card (trans_num, card_type, last4_digit) 
                            VALUES (@transNum, @cardType, @last4Digit)";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    command.Parameters.AddWithValue("@cardType", cardType);
                    command.Parameters.AddWithValue("@last4Digit", last4Digit);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public List<GCashMayaDisplay> GetSalesGCash(string dateFrom, string dateTo, string cashier = "")
        {
            List<GCashMayaDisplay> gCashDisplayList = new List<GCashMayaDisplay>();

            string cashierClause = (string.IsNullOrWhiteSpace(cashier) || cashier.ToLower() == "all") ? "" : "AND LCase(cashier) = '" + cashier.ToLower().Trim() + "'";
            var sql = $@"SELECT g.trans_num, cell_num, reference_num, SUM(total) AS sales, cashier, date_time
                            FROM Sales_GCash g INNER JOIN Sales s ON g.trans_num = s.trans_num 
                            WHERE date_time BETWEEN #{dateFrom}# AND #{dateTo}# {cashierClause}
                            GROUP BY g.trans_num, cell_num, reference_num, cashier, date_time
                            ORDER BY date_time";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var gCashDisplay = new GCashMayaDisplay
                            {
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                CellNum = reader.GetString(reader.GetOrdinal("cell_num")),
                                ReferenceNum = reader.GetString(reader.GetOrdinal("reference_num")),
                                Total = reader.GetDecimal(reader.GetOrdinal("sales")),
                                Cashier = reader.GetString(reader.GetOrdinal("cashier")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("date_time"))
                            };
                            gCashDisplayList.Add(gCashDisplay);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return gCashDisplayList;
        }

        public List<GCashMayaDisplay> GetSalesMaya(string dateFrom, string dateTo, string cashier = "")
        {
            List<GCashMayaDisplay> mayaDisplayList = new List<GCashMayaDisplay>();

            string cashierClause = (string.IsNullOrWhiteSpace(cashier) || cashier.ToLower() == "all") ? "" : "AND LCase(cashier) = '" + cashier.ToLower().Trim() + "'";
            var sql = $@"SELECT m.trans_num, cell_num, reference_num, SUM(total) AS sales, cashier, date_time
                            FROM Sales_Maya m INNER JOIN Sales s ON m.trans_num = s.trans_num 
                            WHERE date_time BETWEEN #{dateFrom}# AND #{dateTo}# {cashierClause}
                            GROUP BY m.trans_num, cell_num, reference_num, cashier, date_time
                            ORDER BY date_time";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var mayaDisplay = new GCashMayaDisplay
                            {
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                CellNum = reader.GetString(reader.GetOrdinal("cell_num")),
                                ReferenceNum = reader.GetString(reader.GetOrdinal("reference_num")),
                                Total = reader.GetDecimal(reader.GetOrdinal("sales")),
                                Cashier = reader.GetString(reader.GetOrdinal("cashier")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("date_time"))
                            };
                            mayaDisplayList.Add(mayaDisplay);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return mayaDisplayList;
        }

        public List<CardDisplay> GetSalesCard(string dateFrom, string dateTo, string cashier = "")
        {
            List<CardDisplay> cardDisplayList = new List<CardDisplay>();

            string cashierClause = (string.IsNullOrWhiteSpace(cashier) || cashier.ToLower() == "all") ? "" : "AND LCase(cashier) = '" + cashier.ToLower().Trim() + "'";
            var sql = $@"SELECT c.trans_num, card_type, last4_digit, SUM(total) AS sales, cashier, date_time
                            FROM Sales_Card c INNER JOIN Sales s ON c.trans_num = s.trans_num 
                            WHERE date_time BETWEEN #{dateFrom}# AND #{dateTo}# {cashierClause}
                            GROUP BY c.trans_num, card_type, last4_digit, cashier, date_time
                            ORDER BY date_time";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cardDisplay = new CardDisplay
                            {
                                TransNum = reader.GetString(reader.GetOrdinal("trans_num")),
                                CardType = reader.GetString(reader.GetOrdinal("card_type")),
                                Last4Digit = reader.GetInt16(reader.GetOrdinal("last4_digit")),
                                Total = reader.GetDecimal(reader.GetOrdinal("sales")),
                                Cashier = reader.GetString(reader.GetOrdinal("cashier")),
                                DateTime = reader.GetDateTime(reader.GetOrdinal("date_time"))
                            };
                            cardDisplayList.Add(cardDisplay);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return cardDisplayList;
        }

        #endregion NONCASH

        #region CASHTENDER

        public int InsertCashTender(string transNum, decimal amountPaid)
        {
            var sql = $@"INSERT INTO Cash_Tender (trans_num, amount_paid, date_time) 
                            VALUES (@transNum, @amountPaid, NOW())";

            int result = 0;

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    command.Parameters.AddWithValue("@amountPaid", amountPaid);
                    result = command.ExecuteNonQuery();
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public decimal GetCashTender(string transNum)
        {
            decimal result = 0;
            var sql = $@"SELECT amount_paid FROM Cash_Tender
                            WHERE trans_num = @transNum";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.Parameters.AddWithValue("@transNum", transNum);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = Convert.ToDecimal(reader["amount_paid"] ?? 0.00);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }

            return result;
        }

        public void DeleteOldCashTenderRecords()
        {
            var sql = $"DELETE FROM Cash_Tender WHERE date_time <= DateAdd('d', -{_recordRetentionDays}, Date())";

            try
            {
                _connection.Open();
                using (OleDbCommand command = new OleDbCommand(sql, _connection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message, exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion CASHTENDER
    }
}