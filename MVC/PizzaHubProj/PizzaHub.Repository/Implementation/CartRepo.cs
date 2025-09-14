using Microsoft.EntityFrameworkCore;
using PizzaHub.Core.DB_Context;
using PizzaHub.Core.Entities;
using PizzaHub.Models;
using PizzaHub.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHub.Repository.Implementation
{
    public class CartRepo :Repository<Cart>, ICartRepo
    {
        private PizzaHubContext _context;
        public CartRepo(PizzaHubContext context): base(context) 
        {
            _context = context;
        }

        public int AddItemToCart(Guid cartId, int productId)
        {
            int result = 0;
            try
            {
                var cartExist = CartExist(cartId);
                var product = GetProduct(productId);

                if (cartExist == null)
                {
                    var addCart = new Cart
                    {
                        Id = cartId,
                        CreatedDate = DateTime.Now,
                        UserId = 1,  // dynamic
                        IsActive = true,
                    };

                    CartItem item = new CartItem
                    {
                        CartId = cartId,
                        ProductId = productId,
                        Quantity = 1,  // dynamic
                        UnitPrice = product?.UnitPrice,
                        IsActive = true,
                        CreatedDate = DateTime.Now
                    };

                     addCart.CartItems.Add(item);
                    _context.Carts.Add(addCart);
                    _context.SaveChanges();
                    result = 1;
                }
                else
                {
                    var productExist = _context.CartItems.FirstOrDefault(c => c.CartId == cartId && c.ProductId == productId);
                    if(productExist == null)
                    {
                        CartItem item = new CartItem
                        {
                            CartId = cartId,
                            ProductId = product.Id,
                            Quantity = 1,
                            UnitPrice = product?.UnitPrice,
                            CreatedDate = DateTime.Now,
                            IsActive = true,
                        };
                        _context.CartItems.Add(item);
                        _context.SaveChanges();
                        result = 2;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public CheckoutModel CheckoutForCart(string cartId, int? productId)
        {
            try
            {
                decimal? total = 0.0m;
                var id = new Guid(cartId);
                var cartExist = CartExist(id);
                if(cartExist != null && productId != 0)
                {
                    var cartItems = _context.CartItems.Where(x => x.CartId == id && x.ProductId == productId).Select(item => new CheckoutModel
                    {
                        Products = _context.Products.Where(x => x.Enabled == true && x.Id == productId).Select(pd => new ProductModel
                        {
                            Id = pd.Id,
                            ProductName = pd.ProductName,
                            ProductDescription = pd.ProdcutDescription,
                            ImageUrl = pd.ImageUrl,
                            Currency = pd.Currency,
                            UnitPrice = pd.UnitPrice,
                        }).ToList(),
                        Order = new OrderSummary
                        {
                            TotalAmount =  (item.UnitPrice * item.Quantity),
                            TaxAmount =    (item.UnitPrice * item.Quantity) * 0.18m,
                            PayingAmount =  (item.UnitPrice * item.Quantity) * 1.18m
                        }
                    }).FirstOrDefault();
                    return cartItems;
                }
                else if(cartExist != null && productId == 0)
                {
                    var prd = new List<ProductModel>();
                    var order_summary = new OrderSummary();
                    var productsList = _context.Products.Where(x => x.Enabled == true).ToList();
                    var cartItems = _context.CartItems.Where(ct => ct.CartId == id).ToList();
                    if(cartExist != null && cartItems.Count > 0)
                    {
                        foreach(var item in cartItems)
                        {
                            var product = productsList.FirstOrDefault(x => x.Id == item.ProductId);
                            if(product != null)
                            {
                                prd.Add(new ProductModel
                                {
                                    Id = product.Id,
                                    ProductName = product.ProductName,
                                    ProductDescription = product.ProdcutDescription,
                                    ImageUrl = product.ImageUrl,
                                    Currency = product.Currency,
                                    UnitPrice = product.UnitPrice,
                                });

                                total += (item.UnitPrice * item.Quantity);
                                
                            }
                        }

                        order_summary = new OrderSummary
                        {
                            TotalAmount = total,
                            TaxAmount = total * 0.18m,
                            PayingAmount = (total + (total * 0.18m)),
                        };
                    }
                    var checkout = new CheckoutModel
                    {
                        Products = prd,
                        Order = order_summary,
                    };
                    return checkout;
                }
                else
                {
                    return new CheckoutModel();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteItem(int productId)
        {
            bool flag = false;
            try
            {
                var item = _context.CartItems.FirstOrDefault(x => x.ProductId == productId);
                if(item != null)
                {
                     item.IsActive = false;
                    _context.CartItems.Update(item);
                    _context.SaveChanges();
                    flag = true;
                }
                return flag;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<CartItemModel> GetCartItems()
        {
            try
            {
                var productItems = _context.CartItems.Where(itm => itm.IsActive == true).Select(ct => new CartItemModel
                {
                    Id = ct.Id,
                    CartId = ct.CartId,
                    UnitPrice = ct.UnitPrice,
                    Quantity = ct.Quantity,
                    Products = _context.Products.Where(x => x.Id == ct.ProductId).Select(p => new ProductModel
                    {
                        Id = p.Id,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProdcutDescription,
                        Currency = p.Currency,
                        ImageUrl = p.ImageUrl,  
                    }).ToList()
                }).ToList();
                return productItems;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateQuantity(string CartId, int productId, int qty)
        {
            try
            {
                var id = new Guid(CartId);
                var item = _context.CartItems.FirstOrDefault(x => x.CartId == id && productId == x.ProductId);
                if(item != null)
                {
                   item.Quantity += qty;
                   _context.CartItems.Update(item);
                   _context.SaveChanges();
                }
                return (int)item.Quantity;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        private Cart CartExist(Guid cartId)
        {
            try
            {
                var cartExist = _context.Carts.Include(c => c.CartItems).FirstOrDefault(c => c.Id == cartId);
                return cartExist;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private Product GetProduct(int productId)
        {
            try
            {
                var getProduct = _context.Products.FirstOrDefault(c => c.Id == productId);
                return getProduct;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
