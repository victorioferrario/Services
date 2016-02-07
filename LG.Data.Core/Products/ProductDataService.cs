using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Enums;
using LG.Data.Models.Products;
using LG.Services.PDMS;

namespace LG.Data.Core.Products
{

    public static class ProductDataService
    {

        private async static Task<LG.Data.Models.BaseModel> StoreProduct(LG.Data.Models.Products.Entity model, int index)
        {
            var result = new BaseModel();
            var client = LG.Services.ClientConnection.GetPdmsConnection();
            try
            {
                client.Open();
                var resultStore = await client.StoreProductInfoAsync(new StoreProductInfoRequest()
                {
                    MSPRID = model.MSPRID,
                    GroupRID = model.GroupRID,
                    ClientRID = model.ClientRID,
                    MembershipPlanID = model.MembershipPlanID,
                    IsAvailableByDefault = model.ProductSettingsRecords[index].IsAvailableByDefault,
                    IsActive = model.ProductSettingsRecords[index].IsActive,
                    ProductID = model.ProductSettingsRecords[index].ProductID,
                    ProductLabel = model.ProductSettingsRecords[index].ProductLabel,
                    ClientPrice = model.ProductSettingsRecords[index].ClientPrice,
                    MemberPrice = model.ProductSettingsRecords[index].MemberPrice,
                    MessageGuid = Guid.NewGuid(),
                    CorporationRID = model.CorporationRID,
                    PropBag = PropBag
                });
                result.IsError = false;
                result.Message = "Saved";
            }
            catch (Exception ex)
            {
                client.Abort();
                result.IsError = true;
                result.Message = "Error" + ex.ToString();
            }
            finally
            {
                client.Close();
            }
            return result;
        }
        private async static Task<LG.Data.Models.BaseModel> StoreClientLevelProduct(LG.Data.Models.Products.Entity model, int index)
        {
            var result = new BaseModel();
            var client = LG.Services.ClientConnection.GetPdmsConnection();
            try
            {
                client.Open();
                var resultStore = await client.StoreProductInfoAsync(new StoreProductInfoRequest()
                {
                    MSPRID = model.MSPRID,
                    GroupRID = null,
                    MembershipPlanID = null,
                    ClientRID = model.ClientRID,
                    IsAvailableByDefault = model.ProductSettingsRecords[index].IsAvailableByDefault,
                    IsActive = model.ProductSettingsRecords[index].IsActive,
                    ProductID = model.ProductSettingsRecords[index].ProductID,
                    ProductLabel = model.ProductSettingsRecords[index].ProductLabel,
                    ClientPrice = model.ProductSettingsRecords[index].ClientPrice,
                    MemberPrice = model.ProductSettingsRecords[index].MemberPrice,
                    MessageGuid = Guid.NewGuid(),
                    CorporationRID = model.CorporationRID,
                    PropBag = PropBag
                });
                result.IsError = false;
                result.Message = "Saved";
            }
            catch (Exception ex)
            {
                client.Abort();
                result.IsError = true;
                result.Message = "Error" + ex.ToString();
            }
            finally
            {
                client.Close();
            }
            return result;
        }
        public async static Task<LG.Data.Models.Products.Entity> Products(LG.Data.Models.Products.Entity model)
        {
            var client = LG.Services.ClientConnection.GetPdmsConnection();
            switch (model.EventAction)
            {
                case ProductAction.Load:
                    #region  [  SERVICE LOGIC   ]
                    try
                    {
                        client.Open();
                        var result5 = await client.LoadProductInfoAsync(new LoadProductInfoRequest()
                        {
                            MSPRID = model.MSPRID,
                            GroupRID = model.GroupRID,
                            ClientRID = model.ClientRID,
                            MessageGuid = Guid.NewGuid(),
                            CorporationRID = model.CorporationRID,
                            MembershipPlanID = model.MembershipPlanID,
                        });
                        model.ListOfProductInfo = result5.ListOfProductInfos;
                    }
                    catch (Exception ex)
                    {
                        client.Abort();
                        model.IsError = true;
                        model.Message = ex.Message;
                    }
                    finally
                    {
                        client.Close();
                    } break;
                    #endregion

                case ProductAction.Get:
                    #region  [  SERVICE LOGIC   ]
                    try
                    {
                        client.Open();
                        var resultGet = await client.GetProductSettingsRecordsAsync(new GetProductSettingsRecordsRequest()
                        {
                            MSPRID = model.MSPRID,
                            GroupRID = model.GroupRID,
                            ClientRID = model.ClientRID,
                            MessageGuid = Guid.NewGuid(),
                            CorporationRID = model.CorporationRID,
                            MembershipPlanID = model.MembershipPlanID,
                        });
                        model.ProductSettingsRecords = resultGet.ProductSettingsRecords;
                    }
                    catch (Exception ex)
                    {
                        client.Abort();
                        model.IsError = true;
                        model.Message = ex.Message;
                    }
                    finally
                    {
                        client.Close();
                    } break;
                    #endregion
                case ProductAction.Store:
                    #region  [  SERVICE LOGIC   ]
                    try
                    {
                        client.Open();
                        var resultStore = await client.StoreProductInfoAsync(new StoreProductInfoRequest()
                        {
                            MSPRID = model.MSPRID,
                            GroupRID = model.GroupRID,
                            ClientRID = model.ClientRID,
                            MembershipPlanID = model.MembershipPlanID,
                            IsAvailableByDefault = model.Product.IsAvailableByDefault,
                            IsActive = model.Product.IsActive,
                            ProductID = model.Product.ProductID,
                            ProductLabel = model.Product.ProductLabel,
                            ClientPrice = model.Product.ClientPrice,
                            MemberPrice = model.Product.MemberPrice,
                            MessageGuid = Guid.NewGuid(),
                            CorporationRID = model.CorporationRID,
                            PropBag = PropBag
                        });
                        model.EventAction = ProductAction.Get; 
                        model = await Products(model);
                    }
                    catch (Exception ex)
                    {
                        client.Abort();
                        model.IsError = true;
                        model.Message = ex.Message;
                    }
                    finally
                    {
                        client.Close();
                    }
                    break;
                    #endregion
                case ProductAction.StoreMultiple:
                    #region  [  SERVICE LOGIC   ]


                    var phone = StoreProduct(model, 0);
                    await phone;

                    if (phone.IsCompleted)
                    {
                        var email = StoreProduct(model, 1);
                        await email;
                        if (email.IsCompleted)
                        {
                            var video = StoreProduct(model, 2);
                            await video;
                            if (video.IsCompleted)
                            {

                                model.EventAction = ProductAction.StoreClientLevel;
                                model = await Products(model);

                                model.EventAction = ProductAction.Get;
                                model = await Products(model);

                                model.Message = String.Format(
                                                  "Phone:{0}|IsError:{1}," +
                                                  "Email:{2}|IsError:{3}, " +
                                                  "Video:{4}|IsError:{5}",
                                        phone.Result.Message, phone.Result.IsError,
                                        email.Result.Message, email.Result.IsError,
                                        video.Result.Message, video.Result.IsError);

                            }
                        }
                    }
                    break;
                    #endregion
                case ProductAction.StoreClientLevel:
                    #region  [  SERVICE LOGIC   ]
                    var phone2 = StoreClientLevelProduct(model, 0);

                    await phone2;
                    if (phone2.IsCompleted)
                    {
                        var email2 = StoreClientLevelProduct(model, 1);
                        await email2;
                        if (email2.IsCompleted)
                        {
                            var video2 = StoreClientLevelProduct(model, 2);
                            if (video2.IsCompleted)
                            {
                                model.EventAction = ProductAction.Get;
                                model = await Products(model);
                                model.Message = String.Format(
                                                  "Phone:{0}|IsError:{1}," +
                                                  "Email:{2}|IsError:{3}, " +
                                                  "Video:{4}|IsError:{5}",
                                        phone2.Result.Message, phone2.Result.IsError,
                                        email2.Result.Message, email2.Result.IsError,
                                        video2.Result.Message, video2.Result.IsError);
                            }
                        }
                    }
                    break;
                    #endregion
                case ProductAction.UpdateLabel:
                    #region  [  SERVICE LOGIC   ]
                    try
                    {
                        client.Open();
                        var resultUpdate = await client.UpdateProductLabelAsync(
                            new UpdateProductLabelRequest()
                            {
                                MSPRID = model.MSPRID,
                                GroupRID = model.GroupRID,
                                ClientRID = model.ClientRID,
                                MembershipPlanID = model.MembershipPlanID,
                                ProductID = model.Product.ProductID,
                                ProductLabel = model.Product.ProductLabel,
                                MessageGuid = Guid.NewGuid(),
                                CorporationRID = model.CorporationRID,
                                PropBag = PropBag
                            });
                        model.EventAction = ProductAction.Get; model = await Products(model);
                    }
                    catch (Exception ex)
                    {
                        client.Abort();
                        model.IsError = true;
                        model.Message = ex.Message;
                    }
                    finally
                    {
                        client.Close();
                    }
                    break;
                    #endregion
                case ProductAction.ToggleStatus:
                    #region  [  SERVICE LOGIC   ]
                    try
                    {
                        client.Open();
                        var resultUpdate = await client.ToggleProductActivationStatusAsync(
                            new ToggleProductActivationStatusRequest()
                            {
                                MSPRID = model.MSPRID,
                                GroupRID = model.GroupRID,
                                ClientRID = model.ClientRID,
                                MembershipPlanID = model.MembershipPlanID,
                                ProductID = model.Product.ProductID,
                                MessageGuid = Guid.NewGuid(),
                                CorporationRID = model.CorporationRID,
                                PropBag = PropBag
                            });
                        model.EventAction = ProductAction.Get; model = await Products(model);
                    }
                    catch (Exception ex)
                    {
                        client.Abort();
                        model.IsError = true;
                        model.Message = ex.Message;
                    }
                    finally
                    {
                        client.Close();
                    }
                    break;
                    #endregion
            }
            return model;
        }


        public async static Task<LG.Data.Models.Products.Entity> GetProducts(LG.Data.Models.Products.Entity model)
        {
            var client = LG.Services.ClientConnection.GetPdmsConnection();
            var result = await client.GetProductSettingsRecordsAsync(
                new GetProductSettingsRecordsRequest()
            {
                MSPRID = model.MSPRID,
                GroupRID = model.GroupRID,
                ClientRID = model.ClientRID,
                CorporationRID = model.CorporationRID,
                MembershipPlanID = model.MembershipPlanID,
                MessageGuid = Guid.NewGuid()
            });
            model.ProductSettingsRecords = result.ProductSettingsRecords; return model;
        }

        public async static Task<List<ProductInfo>> GetProductsByAccountID(int AccountID)
        {
            var client = LG.Services.ClientConnection.GetPdmsConnection();
            var result = await client.GetProductByMemberAccountAsync(
                new GetProductByMemberAccountRequest()
                {
                    AccountID = AccountID,
                    MessageGuid = Guid.NewGuid()
                });
            return result.ListOfProductInfos;
        }

        public static String PropBag { get { return "<PropBag></PropBag>"; } }
    }
}
