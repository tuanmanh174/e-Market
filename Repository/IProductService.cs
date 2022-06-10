using DataAccess;
using DTO;
using DTO.Product;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess
{
    public interface IProductService
    {
        List<ProductModel> GetListProduct(string ProductName);
        ProductModel GetProductById(int ProductId);
        Response Insert(ProductModel pro);
        Response Update(int ProductId);
        Response Delete(int ProductId);

    }

    public class ProductService : IProductService
    {
        //private ILog _logger;
        private AppDbContext _context;
        public Response response = new Response();
        public ProductService(AppDbContext context)
        {
            _context = context;
            //_logger = loggger;
        }

        public List<ProductModel> GetListProduct(string ProductName)
        {
            var productName = ProductName.ToLower();
            List<ProductModel> lstProduct = new List<ProductModel>();
            if (ProductName == "")
            {
                lstProduct = _context.Products.Select(x => new ProductModel
                {
                    Content = x.Content,
                    LinkImage1 = x.LinkImage1,
                    LinkImage2 = x.LinkImage2,
                    LinkImage3 = x.LinkImage3,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                }).ToList();
            }
            else
            {
                lstProduct = _context.Products.Select(x => new ProductModel
                {
                    Content = x.Content,
                    LinkImage1 = x.LinkImage1,
                    LinkImage2 = x.LinkImage2,
                    LinkImage3 = x.LinkImage3,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                }).Where(x => productName.Contains(x.ProductName.ToLower())).ToList();
            }

            return lstProduct;
        }
        public ProductModel GetProductById(int ProductId)
        {
            var Product = _context.Products.Select(x => new ProductModel
            {
                Content = x.Content,
                LinkImage1 = x.LinkImage1,
                LinkImage2 = x.LinkImage2,
                LinkImage3 = x.LinkImage3,
                Price = x.Price,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            }).Where(x => x.ProductId == ProductId).FirstOrDefault();
            return Product;
        }
        public Response Insert(ProductModel pro)
        {
            try
            {
                Product prd = new Product();
                prd.ProductName = pro.ProductName;
                prd.LinkImage1 = pro.LinkImage1;
                prd.LinkImage2 = pro.LinkImage2;
                prd.LinkImage3 = pro.LinkImage3;
                prd.Content = pro.Content;
                prd.Price = pro.Price;
                _context.Products.Add(prd);
                _context.SaveChanges();
                response.Message = "Bạn đã thêm mới thành công";
                response.Status = 200;
            }
            catch (Exception ex)
            {
                response.Message = "Bạn đã thêm mới không thành công";
                response.Status = -100;
                ex.ToString();
            }
            
            return response;
        }
        public Response Update(int ProductId)
        {
            return new Response();
        }
        public Response Delete(int ProductId)
        {
            return new Response();
        }
    }
}
