using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models.Catalog;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Catalog")]
    public class CatalogController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Catalog/GetAllProducts
        [AllowAnonymous]
        [Route("GetAllProducts")]
        public IList<ProductViewModel> GetAllProducts()
        {
            var products = db.Products.Select(a => new ProductViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Thumbnail = a.Thumbnail,
                Price = a.Price
            });

            return products.ToList();
        }

        // GET: api/Catalog/GetAllCategories
        [AllowAnonymous]
        [Route("GetAllCategories")]
        public IList<CategoryViewModel> GetAllCategories()
        {
            var categories = db.Categories.Select(a => new CategoryViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });

            return categories.ToList();
        }

        // POST: api/Catalog/GetProductsByCategory
        [AllowAnonymous]
        [Route("GetProductsByCategory")]
        [HttpPost]
        public IList<ProductViewModel> GetProductsByCategory(GetProductsByCategoryBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return new List<ProductViewModel>();
            }

            var products = db.Products.Where(a => a.CategoryId == model.CategoryId).Select(a => new ProductViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Thumbnail = a.Thumbnail,
                Price = a.Price
            });

            return products.ToList();
        }

        // POST: api/Catalog/GetProducts
        [AllowAnonymous]
        [Route("GetProducts")]
        [HttpPost]
        public IList<ProductViewModel> GetProducts(GetProductsBindingModel model)
        {
            var productList = new List<ProductViewModel>();

            if (!ModelState.IsValid)
            {
                return productList;
            }

            if (string.Compare(model.Filter, "new", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                productList = db.Products.OrderByDescending(a => a.CreateTime).Select(a => new ProductViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Thumbnail = a.Thumbnail,
                    Price = a.Price
                }).Skip(model.StartIndex).Take(model.Offset).ToList();
            }
            else if (string.Compare(model.Filter, "hotest", StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                productList = db.Products.OrderByDescending(a => a.SalesCount).Select(a => new ProductViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Thumbnail = a.Thumbnail,
                    Price = a.Price
                }).Skip(model.StartIndex).Take(model.Offset).ToList();
            }
            else if (string.Compare(model.Filter, "keyword", StringComparison.InvariantCultureIgnoreCase) == 0 && !string.IsNullOrEmpty(model.Keyword))
            {
                productList =
                    db.Products.Where(a => a.Name.Contains(model.Keyword)).Select(a => new ProductViewModel()
                    {
                        Id = a.Id,
                        Name = a.Name,
                        Description = a.Description,
                        Thumbnail = a.Thumbnail,
                        Price = a.Price
                    }).Skip(model.StartIndex).Take(model.Offset).ToList();
            }
            else
            {
                productList = db.Products.Select(a => new ProductViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Thumbnail = a.Thumbnail,
                    Price = a.Price
                }).Skip(model.StartIndex).Take(model.Offset).ToList();
            }

            return productList;
        }

        // POST: api/Catalog/GetProductDetail
        [AllowAnonymous]
        [Route("GetProductDetail")]
        [HttpPost]
        public ProductDetailViewModel GetProductsByCategory(GetProductDetailBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            var product = db.Products.Where(a => a.Id == model.ProductId).Include(a => a.ProductImages).Select(a => new ProductDetailViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Thumbnail = a.Thumbnail,
                Price = a.Price,
                SalesCount = a.SalesCount,
                CreateTime = a.CreateTime,
                Stock = a.Stock,
                ProductImages = a.ProductImages
            }).FirstOrDefault();

            return product;
        }

        // POST: api/Catalog/AddProduct
        [Route("AddProduct")]
        [HttpPost]
        public IHttpActionResult AddProduct(AddProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = new Product()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Thumbnail = model.ImageUrl,
                Price = model.Price
            };

            db.Products.Add(product);
            db.SaveChanges();

            return Ok(product);
        }

        // POST: api/Catalog/AddCategory
        [Route("AddCategory")]
        [HttpPost]
        public IHttpActionResult AddCategory(AddCategoryBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Category category = new Category()
            {
                Name = model.Name,
                Description = model.Description,
            };

            db.Categories.Add(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
