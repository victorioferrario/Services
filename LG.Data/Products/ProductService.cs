using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.PDMS;

namespace LG.Data.Products
{
    public static class ProductService
    {
        public async static Task<LG.Data.Models.Products.Entity> Products(LG.Data.Models.Products.Entity model)
        {
            return await LG.Data.Core.Products.ProductDataService.Products(model);
        }
        public async static Task<LG.Data.Models.Products.Entity> GetProducts(LG.Data.Models.Products.Entity model)
        {
            return await LG.Data.Core.Products.ProductDataService.GetProducts(model);
        }

        public async static Task<List<ProductInfo>> GetProductsByAccountID(int AccountID)
        {
            return await LG.Data.Core.Products.ProductDataService.GetProductsByAccountID(AccountID);
        }
    }
}
