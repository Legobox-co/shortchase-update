using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Shortchase.Dtos;
using Shortchase.Entities;
using Shortchase.Helpers;
using Shortchase.Authorization;

namespace Shortchase.Services
{
    public class OrderService : IOrderService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public OrderService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ICollection<Order>> GetAll()
        {
            try
            {
                return await _context.Orders.ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Order> GetById(Guid id)
        {
            try
            {
                return await _context.Orders.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> Insert(Order item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.Orders.Add(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }




        public async Task<bool> Update(Order item)
        {
            try
            {
                if (item != null)
                {
                    _context.Entry(item).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);

                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                Order item = await _context.Orders.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
                if (item != null)
                {
                    var res = _context.Orders.Remove(item);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<int> CountTotalByUser(Guid id)
        {
            try
            {
                return await _context.Orders.CountAsync(i => i.UserId == id && (i.PaymentStatus == OrderPaymentStatus.Paid || i.PaymentStatus == OrderPaymentStatus.Pending)).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<int> CountTotal()
        {
            try
            {
                return await _context.Orders.CountAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> PlaceNewOrder(Order order, ICollection<OrderItem> orderItems, ShoppingCart cart, User user)
        {
            try
            {
                if (order != null)
                {
                    if (orderItems != null && orderItems.Count > 0)
                    {
                        _context.Orders.Add(order);
                        foreach (var item in orderItems)
                        {
                            _context.OrderItems.Add(item);
                        }

                        cart.Items = "";
                        _context.Entry(cart).State = EntityState.Modified;

                        if (order.WalletBalanceAfterPurchase < 0)
                        {
                            user.WalletBalance = 0.00m;
                        }
                        else {
                            user.WalletBalance = order.WalletBalanceAfterPurchase;
                        }
                        
                        _context.Entry(user).State = EntityState.Modified;

                        await _context.SaveChangesAsync().ConfigureAwait(false);

                        return true;
                    }
                    else throw new Exception("No order items to place order.");
                }
                else throw new Exception("No order to add.");
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }


        public async Task<bool> HasPurchased(Guid UserId, Guid ItemId)
        {
            try
            {
                return await _context.Orders.Include(i => i.Items).AnyAsync(i => i.UserId == UserId && i.Items.Any(u => u.BetListingId == ItemId) && (i.PaymentStatus == OrderPaymentStatus.Paid || i.PaymentStatus == OrderPaymentStatus.Pending)).ConfigureAwait(false);

            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<ICollection<OrderManagerOrderDto>> GetAllOrdersFromUser(Guid UserId)
        {
            try
            {
                ICollection<OrderManagerOrderDto> orders = new List<OrderManagerOrderDto>();

                orders = await _context.Orders.Where(i => i.UserId == UserId).Select(i => new OrderManagerOrderDto
                {
                    OrderDate = i.RowDate,
                    OrderNumber = i.OrderNumber,
                    OrderStatus = i.PaymentStatus,
                    OrderTotal = i.TotalAfterTax,
                    OrderValue = i.TotalBeforeTaxAndFees,
                    ServiceFee = i.ServiceFee,
                    ServiceFeePercent = i.ServiceFeePercent,
                    Tax = i.EstimatedTax,
                    OrderId = i.Id,
                    TaxPercent = i.EstimatedTaxPercent,
                    DiscountTotal = i.Discount
                }).ToListAsync().ConfigureAwait(false);

                if (orders.Count > 0)
                {
                    foreach (var order in orders)
                    {
                        order.Items = await _context.OrderItems.Include(x => x.BetListing.Postedby).Include(x => x.BetListing).Include(x => x.BetListing.Pick).Include(x => x.BetListing.Category).Include(x => x.BetListing.SubCategory).Where(x => x.OrderId == order.OrderId).Select(x => new OrderManagerOrderItemsDto
                        {
                            Image = x.BetListing.Category.MarketplaceImage,
                            Odds = x.BetListing.Odds,
                            Pick = x.ListingTitle,
                            PostedByDate = x.RowDate,
                            PostedByName = x.SoldBy,
                            Price = x.Price,
                            StartTime = x.BetListing.Pick.StartTime,
                            Title = x.BetListing.Title,
                            Subcategory = x.BetListing.SubCategoryId.HasValue ? x.BetListing.SubCategory.Name : "",
                            UserProfilePicture = x.BetListing.Postedby.ProfilePicture
                        }).ToListAsync().ConfigureAwait(false);
                    }
                }

                return orders;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }



        public async Task<ICollection<OrderManagerOrderDto>> GetAllOrdersToUser(Guid UserId)
        {
            try
            {
                ICollection<OrderManagerOrderDto> orders = new List<OrderManagerOrderDto>();
                orders = await _context.Orders.Include(i => i.Items).Where(i => i.Items.Any(u => u.BetListing.PostedbyId == UserId)).Select(i => new OrderManagerOrderDto
                {
                    OrderDate = i.RowDate,
                    OrderNumber = i.OrderNumber,
                    OrderStatus = i.PaymentStatus,
                    OrderTotal = i.TotalAfterTax,
                    OrderValue = i.TotalBeforeTaxAndFees,
                    ServiceFee = i.ServiceFee,
                    ServiceFeePercent = i.ServiceFeePercent,
                    Tax = i.EstimatedTax,
                    OrderId = i.Id,
                    OrderUserId = i.UserId,
                    TaxPercent = i.EstimatedTaxPercent,
                    DiscountTotal = i.Discount
                }).ToListAsync().ConfigureAwait(false);

                if (orders.Count > 0)
                {
                    ICollection<User> users = await _context.Users.Where(i => orders.Select(x => x.OrderUserId.ToString()).Contains(i.Id.ToString())).ToListAsync().ConfigureAwait(false);
                    foreach (var order in orders)
                    {
                        User user = users.Where(i => i.Id == order.OrderUserId).SingleOrDefault();
                        order.Items = await _context.OrderItems.Include(x => x.BetListing.Postedby).Include(x => x.BetListing).Include(x => x.BetListing.Pick).Include(x => x.BetListing.Category).Where(x => x.OrderId == order.OrderId && x.BetListing.PostedbyId == UserId).Select(x => new OrderManagerOrderItemsDto
                        {
                            Image = x.BetListing.Category.MarketplaceImage,
                            Odds = x.BetListing.Odds,
                            Pick = x.ListingTitle,
                            PostedByDate = x.RowDate,
                            PostedByName = user.FirstName + " " + user.LastName,
                            Price = x.Price,
                            StartTime = x.BetListing.Pick.StartTime,
                            Title = x.BetListing.Title,
                            UserProfilePicture = user.ProfilePicture
                        }).ToListAsync().ConfigureAwait(false);
                    }
                }

                return orders;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }



        public async Task<ICollection<Order>> GetAllOrdersForBackend()
        {
            try
            {
                return await _context.Orders.Include(i => i.User).Include(i => i.Items).ThenInclude(x => x.BetListing).ThenInclude(y => y.Postedby).ToListAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

        public async Task<OrderDetailsDto> GetOrderDetails(Guid id)
        {
            try
            {
                OrderDetailsDto data = new OrderDetailsDto
                {
                    Order = await _context.Orders.Include(i => i.User).Include(i => i.Items).Where(i => i.Id == id).SingleOrDefaultAsync().ConfigureAwait(false),
                    
                };
                data.OrderItems = await _context.OrderItems.Include(i => i.BetListing).Include(i => i.BetListing.Bookmaker).Include(i => i.BetListing.Tip).Include(i => i.BetListing.Market).Include(i => i.BetListing.Pick).Include(i => i.BetListing.Postedby).Where(i => i.OrderId == id).ToListAsync().ConfigureAwait(false);

                return data;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }
        
        public async Task<OrderDetailsReceiptDto> GetOrderDetailsForReceipt(Guid id)
        {
            try
            {
                OrderDetailsReceiptDto data = new OrderDetailsReceiptDto
                {
                    Order = await _context.Orders.Include(i => i.User).Include(i => i.Items).Where(i => i.Id == id).SingleOrDefaultAsync().ConfigureAwait(false),
                    
                };
                data.OrderItems = (await _context.OrderItems.Include(i => i.BetListing).Include(i => i.BetListing.Bookmaker).Include(i => i.BetListing.Tip).Include(i => i.BetListing.Market).Include(i => i.BetListing.Pick).Include(i => i.BetListing.Postedby).Where(i => i.OrderId == id).ToListAsync().ConfigureAwait(false)).GroupBy(i => i.SoldBy).ToList();

                return data;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }




        public async Task<bool> UpdateOrderItemPayouts(Guid orderId, Guid userId, Guid payoutId)
        {
            try
            {
                ICollection<OrderItem> items = await _context.OrderItems.Include(i => i.BetListing).Where(i => i.OrderId == orderId).ToListAsync().ConfigureAwait(false);
                if (items == null) throw new Exception("No order found.");

                if (items.Count > 0) {
                    foreach (var item in items) {
                        if (item.BetListing.PostedbyId == userId) {
                            item.PayoutId = payoutId;
                            _context.Entry(item).State = EntityState.Modified;
                        }
                    }
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<decimal> GetTotalAmount()
        {
            try
            {
                decimal total = await _context.Orders.Where(x => x.PaymentStatus == OrderPaymentStatus.Paid || x.PaymentStatus == OrderPaymentStatus.Pending).SumAsync(i => i.TotalAfterTax).ConfigureAwait(false);
                return total;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> UpdateOrderItemPayouts(Guid orderId, ICollection<UserPayout> NewPayouts)
        {
            try
            {
                ICollection<OrderItem> items = await _context.OrderItems.Include(i => i.BetListing).Where(i => i.OrderId == orderId).ToListAsync().ConfigureAwait(false);
                if (items == null) throw new Exception("No order found.");

                if (NewPayouts.Count > 0)
                {
                    foreach (var item in NewPayouts)
                    {
                        _context.UserPayouts.Add(item);
                        ICollection<OrderItem> itemsInPayout = items.Where(i => i.BetListing.PostedbyId == item.UserId).ToList();
                        if (itemsInPayout.Count > 0) {
                            foreach (var payoutItem in itemsInPayout) {
                                payoutItem.PayoutId = item.Id;
                                _context.Entry(payoutItem).State = EntityState.Modified;
                            }
                        }
                    }
                }

                await _context.SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }
    }
}