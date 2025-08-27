using FoodApp.Core.DB_Context;
using FoodApp.Core.Entities;
using FoodApp.Models;
using FoodApp.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Repository.Implementation
{
    public class CartRepo : Repository<Cart>, ICartRepo
    {
        private FoodAppContext _context;
        public CartRepo(FoodAppContext context): base(context) 
        {
            _context = context;
        }

        public bool CartExists(Guid cartId)
        {
            try
            {
                var cart = _context.Carts.FirstOrDefault(x => x.Id == cartId);
                return cart != null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CheckoutModel Checkout(int productId, string cartId)
        {
            decimal? total = 0.0m;
            CheckoutModel checkML = new CheckoutModel();
            try
            {
                var id = new Guid(cartId);
                bool idExist = CartExists(id);
                var productList = _context.Products.Where(x => x.IsDeleted == false).ToList();
                if (idExist)
                {
                    if (productId != 0)
                    {
                        var cartItem = _context.CartItems.Where(x => x.ProductId == productId).Select(item => new CheckoutModel
                        {
                            Products = _context.Products.Where(x => x.Id == productId).Select(pd => new ProductModel
                            {
                                Id = pd.Id,
                                ProductName = pd.ProductName,
                                ProductDescription = pd.ProductDescription,
                                Price = pd.Price,
                                Currency = pd.Currency,
                                ImageURL = pd.ImageURL,
                                //Quantity = item.Quantity,
                            }).ToList(),

                            OrderSummary = new OrderSummaryModel
                            {
                                TotalAmount = (item.UnitPrice * item.Quantity),
                                TaxAmount = (item.UnitPrice * item.Quantity) * 0.18m,
                                PayingAmount = (item.UnitPrice * item.Quantity) * 1.18m
                            }
                        }).FirstOrDefault();
                    }
                    else
                    {
                        var prdML = new List<ProductModel>();
                        var cartItem = _context.CartItems.Where(x => x.CartId == id).ToList();
                        foreach(var item in cartItem)
                        {
                            var product = productList.FirstOrDefault(x => x.Id == item.ProductId);
                            if(product != null)
                            {
                                prdML.Add(new ProductModel
                                {
                                    Id = product.Id,
                                    ProductName = product.ProductName,
                                    ProductDescription = product.ProductDescription,
                                    Price = product.Price,
                                    Currency = product.Currency,
                                    ImageURL = product.ImageURL,
                                    Quantity = item.Quantity,
                                });

                                total += (item.UnitPrice * item.Quantity);
                            }
                        }

                        var order = new OrderSummaryModel
                        {
                            TotalAmount = total,
                            TaxAmount = total * 0.18m,
                            PayingAmount = total + (total * 0.18m)
                        };

                        checkML = new CheckoutModel
                        {
                            Products = prdML,
                            OrderSummary = order,
                        };
                    } 
                    return checkML;
                   
                }
                else
                {
                    return checkML;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CheckoutModel CheckoutCheckoutForHome(int productId)
        {
            try
            {
                var product = new Product();
                var prod = new List<ProductModel>();

                if (productId != 0)
                {
                    product = _context.Products.FirstOrDefault(x => x.IsDeleted == false && x.Id == productId);
                    if (product == null) return new CheckoutModel();
                    prod.Add(new ProductModel
                    {
                        Id = product.Id,
                        ProductName = product?.ProductName,
                        ProductDescription = product?.ProductDescription,
                        Price = product?.Price,
                        Currency = product?.Currency,
                        ImageURL = product?.ImageURL,
                    });
                    var item = new CheckoutModel
                    {
                        Products = prod,
                       

                        OrderSummary = new OrderSummaryModel
                        {
                            TaxAmount = (product.Price * 1),
                            TotalAmount = (product.Price * 1) * 0.18m,
                            PayingAmount = (product.Price * 1) * 1.18m
                        }
                    };

                    return item;
                }
                else
                {
                    return new CheckoutModel();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        //public bool AddItemToCart(int productId, string CartId)
        //{
        //    throw new NotImplementedException();
        //}

        public ProductModel GetProduct(int productId)
        {
            try
            {
                var product = _context.Products.Where(x => x.IsDeleted == false && x.Id == productId).Select(pd => new ProductModel
                {
                    Id = pd.Id,
                    ProductName = pd.ProductName,
                    ProductDescription = pd.ProductDescription,
                    Price = pd.Price,
                    Currency = pd.Currency,
                    ImageURL = pd.ImageURL,
                    Quantity = pd.Quantity, 
                }).FirstOrDefault();
                return product;
               
            }
            catch(Exception)
            {
                throw;
            }
        }

        public List<CartItemModel> GetProducts(Guid cartId)
        {
            try
            {
                var cartExist = _context.CartItems.Where(c => c.CartId == cartId && c.IsActive == true).Select(item => new CartItemModel
                {
                    id = item.Id,
                    CartId = item.CartId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Products = _context.Products.Where(x => x.IsDeleted == false && x.Id == item.ProductId).Select(pd => new ProductModel
                    {
                        Id = pd.Id,
                        ProductName = pd.ProductName,
                        ProductDescription = pd.ProductDescription,
                        Price = pd.Price,
                        Currency = pd.Currency,
                        ImageURL = pd.ImageURL,
                        Quantity = pd.Quantity,

                    }).FirstOrDefault(),
                }).ToList();
                return cartExist;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
