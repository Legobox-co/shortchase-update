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
    public class ShoppingCartService : IShoppingCartService
    {
        private DataContext _context;
        private readonly IErrorLogService errorLogService;

        public ShoppingCartService
        (
            DataContext context,
            IErrorLogService logService
        )
        {
            _context = context;
            this.errorLogService = logService;
        }

        public async Task<ShoppingCart> GetById(Guid id)
        {
            try
            {
                return await _context.ShoppingCarts.Where(c => c.Id == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<ShoppingCart> GetByUserId(Guid id)
        {
            try
            {
                return await _context.ShoppingCarts.Where(c => c.UserId == id).SingleOrDefaultAsync().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                throw;
            }
        }


        public async Task<bool> Insert(ShoppingCart item)
        {
            try
            {
                if (item != null)
                {
                    item.RowDate = DateTime.UtcNow;
                    _context.ShoppingCarts.Add(item);
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


        public async Task<bool> Update(ShoppingCart item)
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





        private async Task<bool> Create(Guid UserId)
        {
            try
            {
                ShoppingCart item = new ShoppingCart
                {
                    Items = "",
                    UserId = UserId,
                    RowDate = DateTime.UtcNow
                };
                _context.ShoppingCarts.Add(item);
                await _context.SaveChangesAsync().ConfigureAwait(false);


                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }



        public async Task<int?> CountItems(Guid UserId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null)
                {
                    bool result = await Create(UserId);
                    if (!result) throw new Exception("Could not create cart for user.");
                    else cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                }
                int totalItems = 0;
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    var items = cart.Items.Split(',');
                    totalItems = items.Length;
                }
                return totalItems;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<bool?> AddItemToCart(Guid UserId, Guid ItemId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                bool dataInCartHasChanged = false;
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    if (!cart.Items.ToUpper().Contains(ItemId.ToString().ToUpper()))
                    {
                        cart.Items = cart.Items + "," + ItemId.ToString();
                        dataInCartHasChanged = true;
                    }
                }
                else
                {
                    cart.Items = ItemId.ToString();
                    dataInCartHasChanged = true;
                }

                if (dataInCartHasChanged)
                {
                    _context.Entry(cart).State = EntityState.Modified;
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return dataInCartHasChanged;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }
        public async Task<bool> RemoveItemToCart(Guid UserId, Guid ItemId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    if (cart.Items.ToUpper().Contains(ItemId.ToString().ToUpper()))
                    {
                        string[] currentItems = cart.Items.Split(',');
                        string newListStr = "";
                        int countIterations = 0;
                        foreach (var item in currentItems)
                        {
                            ++countIterations;
                            string prependComma = ",";
                            if (countIterations == 1)
                            {
                                prependComma = "";
                            }
                            if (item.ToUpper() != ItemId.ToString().ToUpper())
                            {
                                newListStr += prependComma + item;
                            }
                        }
                        cart.Items = newListStr;

                        _context.Entry(cart).State = EntityState.Modified;
                        await _context.SaveChangesAsync().ConfigureAwait(false);


                    }
                }

                return true;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return false;
            }
        }

        public async Task<bool> CleanCart(Guid UserId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    cart.Items = "";
                    _context.Entry(cart).State = EntityState.Modified;
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


        public async Task<bool?> IsItemInCart(Guid UserId, Guid ItemId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                bool isInCart = false;
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    if (cart.Items.ToUpper().Contains(ItemId.ToString().ToUpper()))
                    {
                        isInCart = true;
                    }
                }

                return isInCart;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }
        public async Task<ICollection<ShoppingCartItemDto>> GetItemsInCart(Guid UserId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                ICollection<ShoppingCartItemDto> items = new List<ShoppingCartItemDto>();
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    items = await _context.BetListings.Include(i => i.Pick).Include(i => i.Postedby)
                        .Where(i => cart.Items.ToUpper().Contains(i.Id.ToString().ToUpper()))
                        .Select(item => new ShoppingCartItemDto
                        {
                            ListingId = item.Id,
                            ListingTitle = item.Pick.Team1 + " vs " + item.Pick.Team2,
                            Price = item.Price,
                            SoldById = item.PostedbyId,
                            SoldBy = item.Postedby.FirstName + " " + item.Postedby.LastName
                        }
                        ).ToListAsync().ConfigureAwait(false);

                }

                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }


        public async Task<ICollection<ShoppingCartItemDto>> GetOnlyValidItemsInCart(Guid UserId)
        {
            try
            {
                ShoppingCart cart = await _context.ShoppingCarts.Where(c => c.UserId == UserId).SingleOrDefaultAsync().ConfigureAwait(false);
                if (cart == null) throw new Exception("Error, there should be a cart by now.");
                ICollection<ShoppingCartItemDto> items = new List<ShoppingCartItemDto>();
                if (!string.IsNullOrWhiteSpace(cart.Items))
                {
                    items = await _context.BetListings.Include(i => i.Pick).Include(i => i.Postedby)
                        .Where(i => (!i.Deleted && !i.IsReported ) && cart.Items.ToUpper().Contains(i.Id.ToString().ToUpper()))
                        .Select(item => new ShoppingCartItemDto
                        {
                            ListingId = item.Id,
                            ListingTitle = item.Pick.Team1 + " vs " + item.Pick.Team2,
                            Price = item.Price,
                            SoldById = item.PostedbyId,
                            SoldBy = item.Postedby.FirstName + " " + item.Postedby.LastName
                        }
                        ).ToListAsync().ConfigureAwait(false);

                    string newCartString = "";
                    if (items.Count > 0) {
                        int countIterations = 0;
                        foreach (var item in items)
                        {
                            ++countIterations;
                            string prependComma = ",";
                            if (countIterations == 1)
                            {
                                prependComma = "";
                            }
                            newCartString += prependComma + item.ListingId.ToString();
                        }

                        
                    }

                    if (newCartString.ToUpper() != cart.Items.ToUpper())
                    {

                        cart.Items = newCartString;
                        _context.Entry(cart).State = EntityState.Modified;
                        await _context.SaveChangesAsync().ConfigureAwait(false);
                    }
                }

                return items;
            }
            catch (Exception e)
            {
                await errorLogService.InsertException(e).ConfigureAwait(false);
                return null;
            }
        }

    }
}