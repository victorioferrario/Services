using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models.Enums;

namespace LG.Data.Models.Products
{



    public class Price
    {
        public Decimal ClientPrice { get; set; }
        public Decimal MemberPrice { get; set; }

    }
    public class ProductModel : LG.Data.Models.BaseModel
    {
        public Price Price { get; set; }

        public Decimal ClientPrice { get; set; }
        public Decimal MemberPrice { get; set; }


        public Int32? ID { get; set; }
        public String ProductID { get; set; }
        public String ProductLabel { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsAvailableByDefault { get; set; }
        public ProductModel()
        {
            Price = new Price();
        }
    }
    public class ProductModelBase :LG.Data.Models.BaseModel
    {
        public System.Int64 MSPRID { get; set; }

        public ProductModelBase()
        {
            MSPRID = 100;
        }
        public System.Int64? GroupRID { get; set; }
        public System.Int64? ClientRID { get; set; }
        public System.Int32? MembershipPlanID { get; set; }
       
    }

    public class Entity : ProductModelBase
    {
        public Entity()
        {
            EventAction = ProductAction.Get;
        }
        public LG.Data.Models.Products.ProductModel Product { get; set; }
        public LG.Data.Models.Enums.ProductAction EventAction { get; set; }
        public List<LG.Services.PDMS.ProductInfo> ListOfProductInfo { get; set; }
        public List<LG.Services.PDMS.ProductSettingsRecord> ProductSettingsRecords { get; set; }
    }
}
