using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Shared;
using LG.Data.Models.Users;
using LG.Services;
using LG.Services.ACS;
using LG.Services.AMS;
using LG.Services.EMS;
using AddressUsage = LG.Services.EMS.AddressUsage;
using ReturnStatus = LG.Services.EMS.ReturnStatus;

namespace LG.Data.Core.Shared
{


    internal static class VerfiyInformation
    {
        internal static ValidEmail
            VerifyEmail(string value)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = client.LoadBEntitiesHavingEmailAddress(new LoadBEntitiesHavingEmailAddressRequest()
                {
                    EmailAddress = value,
                    MessageGuid = Guid.NewGuid()
                });
                var responseBoolean = response.ListOfBEntities.Count < 1;
                return new ValidEmail()
                {
                    IsError = false,
                    Valid = response.ListOfBEntities.Count < 1,
                    Message = String.Format("The email:{0} is {1}", value, responseBoolean ? " valid" : "invalid"),
                    Value = value
                };
            }
            catch (Exception ex)
            {
                return new ValidEmail()
                {
                    Valid = false,
                    IsError = true,
                    Value = value,
                    Message = String.Format("The email:{0} is {1} <br/> {2}", value, "invalid, an error occurred.", ex.Message)
                };
            }
        }
        internal static List<Int64> VerifyEmailList(string value)
        {
            var result = new List<Int64>();
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = client.LoadBEntitiesHavingEmailAddress(new LoadBEntitiesHavingEmailAddressRequest()
                {
                    EmailAddress = value,
                    MessageGuid = Guid.NewGuid()
                });
                if (response.ListOfBEntities.Count > 0)
                {
                    result.AddRange(
                        response.ListOfBEntities.Select(item => item.RID));
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }
    }
    public static class SaveContactInformation
    {
        public static async Task<BaseModel> SaveAddressTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    var response = await client.AddAddressAsync(
                        new AddAddressRequest()
                        {
                            Address = new LG.Services.EMS.Address()
                            {
                                AddressLine1 = entity.Addresses[0].AddressLine1,
                                AddressLine2 = entity.Addresses[0].AddressLine2,
                                City = entity.Addresses[0].City,
                                State = entity.Addresses[0].State,
                                ZipCode = entity.Addresses[0].ZipCode,
                                CountryCode = "US",
                                AddressUsages = new List<AddressUsage>()
                                {
                                    new AddressUsage()
                                    {
                                        AddressUsageEnum = entity.Addresses[0].AddressUsageEnumValue,
                                        IsPrimary =  entity.Addresses[0].IsPrimary
                                    }
                                }
                            },
                            MessageGuid = Guid.NewGuid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                else
                {
                    #region [@  Update  @]
                    var response = await client.UpdateAddressAsync(
                         new UpdateAddressRequest()
                         {
                             NewAddress = new LG.Services.EMS.Address()
                             {
                                 AddressLine1 = entity.Addresses[0].AddressLine1,
                                 AddressLine2 = entity.Addresses[0].AddressLine2,
                                 City = entity.Addresses[0].City,
                                 State = entity.Addresses[0].State,
                                 ZipCode = entity.Addresses[0].ZipCode,
                                 CountryCode = "US",
                                 AddressUsages = new List<AddressUsage>()
                                {
                                    new AddressUsage()
                                    {
                                        AddressUsageEnum = entity.Addresses[0].AddressUsageEnumValue,
                                        IsPrimary =  entity.Addresses[0].IsPrimary
                                    }
                                }
                             },
                             IsPrimary = entity.Addresses[0].IsPrimary,
                             AddressIDToUpdate = entity.Addresses[0].ID,
                             NewAddressUsageEnum = entity.Addresses[0].AddressUsageEnumValue,
                             AddressUsageEnumToUpdate = entity.Addresses[0].AddressUsageEnumToUpdate,
                             MessageGuid = Guid.NewGuid(),
                             PropBag = PropBag,
                             RID = entity.UserRID,
                         });
                    var response2 = await client.RemoveAddressAsync(new RemoveAddressRequest()
                    {
                        MessageGuid = Guid.NewGuid(),
                        RID = entity.UserRID,
                        AddressID = entity.Addresses[0].ID,
                        PropBag = "<PropBag>As of 10/20/2015, Removing address after updating address</PropBag>"
                    });
                    client.Close();
                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }

        public static async Task<BaseModel> SaveEmailTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    #region [@  entity.EmailAddress       @]

                    if (entity.EmailAddress != null)
                    {
                        var response = await client.AddEmailAddressAsync(
                        new AddEmailAddressRequest
                        {
                            EmailAddress = new LG.Services.EMS.EmailAddress
                            {
                                Email = entity.EmailAddress.Email,
                                EmailAddressUsages = new List<EmailAddressUsage>
                                {
                                    new EmailAddressUsage
                                    {
                                        EmailAddressUsageEnum = entity.EmailAddress.EmailUsageEnum,
                                        IsPrimary = true
                                    }
                                }
                            },
                            MessageGuid = Guid.NewGuid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                               ? response.ReturnStatus.ErrorMessage
                               : response.ReturnStatus.GeneralMessage
                        };
                    }

                    #endregion
                    #region [@  entity.EmailAddresses     @]

                    if (entity.EmailAddresses != null)
                    {
                        var response = await client.AddEmailAddressAsync(
                        new AddEmailAddressRequest
                        {
                            EmailAddress = new LG.Services.EMS.EmailAddress
                            {
                                Email = entity.EmailAddresses[0].Email,
                                EmailAddressUsages = new List<EmailAddressUsage>
                                {
                                    new EmailAddressUsage
                                    {
                                        EmailAddressUsageEnum = entity.EmailAddresses[0].EmailUsageEnum,
                                        IsPrimary = true
                                    }
                                }
                            },
                            MessageGuid = Guid.NewGuid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                               ? response.ReturnStatus.ErrorMessage
                               : response.ReturnStatus.GeneralMessage
                        };
                    }

                    #endregion

                    #endregion
                }
                else
                {
                    #region [@  Update  @]


                    if (entity.EmailAddresses != null)
                    {

                        var response = await client.UpdateEmailAddressAsync(new UpdateEmailAddressRequest
                        {
                            NewEmailAddress = new LG.Services.EMS.EmailAddress
                            {
                                Email = entity.EmailAddresses[0].Email,
                                EmailAddressUsages = new List<EmailAddressUsage>
                            {
                                new EmailAddressUsage
                                {
                                    EmailAddressUsageEnum = entity.EmailAddresses[0].EmailUsageEnum, IsPrimary = true
                                }
                            }
                            },
                            EmailAddressIDToUpdate = entity.EmailAddress.Id,
                            EmailAddressUsageEnumToUpdate = EmailAddressUsageEnum.Personal,
                            NewEmailAddressUsageEnum = EmailAddressUsageEnum.Personal,
                            IsPrimary = true,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                                    ? response.ReturnStatus.ErrorMessage
                                    : response.ReturnStatus.GeneralMessage
                        };

                    }

                    if (entity.EmailAddress != null)
                    {
                        var response2 = await client.UpdateEmailAddressAsync(new UpdateEmailAddressRequest
                        {
                            NewEmailAddress = new LG.Services.EMS.EmailAddress
                            {
                                Email = entity.EmailAddress.Email,
                                EmailAddressUsages = new List<EmailAddressUsage>
                                {
                                    new EmailAddressUsage
                                    {
                                        EmailAddressUsageEnum = entity.EmailAddress.EmailUsageEnum,
                                        IsPrimary = true
                                    }
                                }
                            },
                            EmailAddressIDToUpdate = entity.EmailAddress.Id,
                            EmailAddressUsageEnumToUpdate = EmailAddressUsageEnum.Personal,
                            NewEmailAddressUsageEnum = EmailAddressUsageEnum.Personal,
                            IsPrimary = true,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response2.ReturnStatus.IsError,
                            Message = response2.ReturnStatus.IsError
                                ? response2.ReturnStatus.ErrorMessage
                                : response2.ReturnStatus.GeneralMessage
                        };
                    }
                    return null;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
            return null;
        }



        public static async Task<BaseModel> SavePhoneTask(LG.Data.Models.Shared.Contact entity, Int32 index)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.ContactAction.Add)
                {
                    #region [@  Add     @]

                    var response = await client.AddPhoneAsync(
                        new AddPhoneRequest
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.RID,
                            Phone = entity.Phones[index],
                            PropBag = PropBag
                        });
                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                            ? response.ReturnStatus.ErrorMessage
                            : response.ReturnStatus.GeneralMessage
                    };
                }
                else
                {
                    #region [@  Update     @]

                    var response = await client.UpdatePhoneAsync(
                        new UpdatePhoneRequest
                        {
                            MessageGuid = new Guid(),
                            RID = entity.RID,
                            NewPhone = entity.Phones[index],
                            IsPrimary = entity.Phones[index].PhoneUsages[0].IsPrimary,
                            PhoneIDToUpdate = entity.Phones[index].ID,
                            NewPhoneUsageEnum = entity.Phones[index].PhoneUsages[0].PhoneUsageEnum,
                            PhoneUsageEnumToUpdate = entity.Phones[index].PhoneUsages[0].PhoneUsageEnum,
                            PropBag = PropBag
                        });
                    client.Close();
                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                            ? response.ReturnStatus.ErrorMessage
                            : response.ReturnStatus.GeneralMessage
                    };
                }
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        public static async Task<BaseModel> SavePhoneTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Add)
                {
                    #region [@  Add     @]

                    if (entity.Phone != null)
                    {
                        var response2 = await client.AddPhoneAsync(
                        new AddPhoneRequest
                        {
                            MessageGuid = Guid.NewGuid(),
                            RID = entity.UserRID,
                            Phone = new Phone
                            {
                                PhoneCountryCode = "1",
                                PhoneExtension = entity.Phone.PhoneExtension,
                                PhoneNumber = entity.Phone.PhoneNumber,
                                PhoneUsages = new List<PhoneUsage>
                                {
                                    new PhoneUsage
                                    {
                                        IsPrimary = true,
                                        PhoneUsageEnum = entity.Phone.PhoneUsageEnum,
                                    }
                                }
                            },
                            PropBag = PropBag
                        });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response2.ReturnStatus.IsError,
                            Message = response2.ReturnStatus.IsError
                                ? response2.ReturnStatus.ErrorMessage
                                : response2.ReturnStatus.GeneralMessage
                        };
                    }
                    if (entity.Phones != null)
                    {


                        var response = await client.AddPhoneAsync(
                            new AddPhoneRequest
                            {
                                MessageGuid = Guid.NewGuid(),
                                RID = entity.UserRID,
                                Phone = new Phone
                                {
                                    PhoneCountryCode = "1",
                                    PhoneExtension = entity.Phones[0].PhoneExtension,
                                    PhoneNumber = entity.Phones[0].PhoneNumber,
                                    PhoneUsages = new List<PhoneUsage>
                                {
                                    new PhoneUsage
                                    {
                                        IsPrimary = true,
                                        PhoneUsageEnum = entity.Phones[0].PhoneUsageEnum,
                                    }
                                }
                                },
                                PropBag = PropBag
                            });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                        };

                    }


                    #endregion

                    return null;

                }
                else
                {
                    #region [@  Update     @]

                    if (entity.Phones != null)
                    {
                        var response = await client.UpdatePhoneAsync(
                            new UpdatePhoneRequest
                            {
                                MessageGuid = new Guid(),
                                RID = entity.UserRID,
                                NewPhone = new Phone
                                {
                                    PhoneCountryCode = "1",
                                    PhoneExtension = "",
                                    PhoneNumber = entity.Phones[0].PhoneNumber,
                                    PhoneUsages = new List<PhoneUsage>
                                    {
                                        new PhoneUsage
                                        {
                                            IsPrimary = entity.Phones[0].IsPrimary,
                                            PhoneUsageEnum = entity.Phones[0].PhoneUsageEnum,
                                        }
                                    }
                                },
                                IsPrimary = entity.Phones[0].IsPrimary,
                                PhoneIDToUpdate = entity.Phones[0].PhoneId,
                                NewPhoneUsageEnum = entity.Phones[0].PhoneUsageEnum,
                                PhoneUsageEnumToUpdate = entity.Phones[0].PhoneUsageEnumToUpdate,
                                PropBag = PropBag
                            });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                        };
                    }
                    if (entity.Phone != null)
                    {
                        var response = await client.UpdatePhoneAsync(
                            new UpdatePhoneRequest
                            {
                                MessageGuid = new Guid(),
                                RID = entity.UserRID,
                                NewPhone = new Phone
                                {
                                    PhoneCountryCode = "1",
                                    PhoneExtension = "",
                                    PhoneNumber = entity.Phone.PhoneNumber,
                                    PhoneUsages = new List<PhoneUsage>
                                    {
                                        new PhoneUsage
                                        {
                                            IsPrimary = entity.Phone.IsPrimary,
                                            PhoneUsageEnum = entity.Phone.PhoneUsageEnum,
                                        }
                                    }
                                },
                                IsPrimary = entity.Phone.IsPrimary,
                                PhoneIDToUpdate = entity.Phone.PhoneId,
                                NewPhoneUsageEnum = entity.Phone.PhoneUsageEnum,
                                PhoneUsageEnumToUpdate = entity.Phone.PhoneUsageEnumToUpdate,
                                PropBag = PropBag
                            });
                        client.Close();
                        return new BaseModel
                        {
                            IsError = response.ReturnStatus.IsError,
                            Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                        };
                    }

                    #endregion



                    return null;
                }
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        public static async Task<UserModel> SavePersonInfoTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
              

                if (!entity.GeneralInfo.Gender.HasValue)
                {
                    entity.GeneralInfo.Gender = 1;
                }
                client.Open();
                var response = await client.SavePersonInfoAsync(
                new SavePersonInfoRequest
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = entity.UserRID,
                    FName = entity.GeneralInfo.FName,
                    MName = entity.GeneralInfo.MName,
                    LName = entity.GeneralInfo.LName,
                    DOB = entity.GeneralInfo.Dob,
                    Gender = entity.GeneralInfo.Gender,
                    PropBag = PropBag,
                });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.IsError ? response.ReturnStatus.ErrorMessage
                    : response.ReturnStatus.GeneralMessage
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }

        public static async Task<BaseModel> SaveCreditCardInfoTask(LG.Services.ACS.CreditCardInfo_Input entity)
        {
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();
              
                    var response = await client.SaveCreditCardInformationAsync(
                        new SaveCreditCardInformationRequest()
                        {
                            MessageGuid = new Guid(),
                            CreditCardInfo_Input = entity,
                            PropBag = PropBag
                        });
                    client.Close();
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message =
                            response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }

        public static async Task<BaseModel> UpdateCreditCardInfoTask(LG.Services.ACS.CreditCardInfo_Input entity)
        {
            var client = ClientConnection.GetAcsConnection();
            try
            {
                client.Open();

                var response = await client.SaveCreditCardInformationAsync(
                    new SaveCreditCardInformationRequest()
                    {
                        MessageGuid = new Guid(),
                        CreditCardInfo_Input = entity,
                        PropBag = PropBag
                    });
             
                client.Close();
                return new BaseModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message =
                        response.ReturnStatus.IsError
                            ? response.ReturnStatus.ErrorMessage
                            : response.ReturnStatus.GeneralMessage
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }


        /// <summary>
        /// This method creates a BEntity object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<BaseModel> SaveSecurityInfoTask(UserModel entity)
        {
            var client = ClientConnection.GetAmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Add)
                {
                    var response = await client.CreateLoginAsync(
                        new CreateLoginRequest
                        {
                            MessageGuid = new Guid(),
                            RID = entity.UserRID,
                            UserName = entity.SecurityInfo.Username,
                            PlainPassword = entity.SecurityInfo.Password,
                            IsTemporaryPassword = false,
                            IsActive = true,
                            DTUTC_PasswordExpires = new DateTime(1970, 1, 1),
                            PropBag = PropBag
                        });
                    client.Close();
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message =
                            response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                else
                {
                    var response = await client.UpdatePasswordAsync(
                       new UpdatePasswordRequest
                       {
                           MessageGuid = new Guid(),
                           RID = entity.UserRID,
                           Password = entity.SecurityInfo.Password,
                           IsTemporaryPassword = false,
                           DTUTC_PasswordExpires = new DateTime(1970, 1, 1),
                           PropBag = PropBag
                       });
                    client.Close();
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message =
                            response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }

            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        /// <summary>
        /// Static propBag, to log what module is executing code.
        /// </summary>
        public static String PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Shared.SaveContactInformation</Class></PropBag>";
    }

    public static class RemoveContactInformation
    {
        public static String PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Shared.RemoveContactInformation</Class></PropBag>";
        public static async Task<BaseModel> RemoveAddressTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Remove)
                {
                    #region [@  Add     @]

                    var response = await client.RemoveAddressAsync(
                        new RemoveAddressRequest()
                        {
                            AddressID = entity.Addresses[0].ID,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                return new BaseModel
                {
                    IsError = true,
                    Message = "EventAction not defined"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        public static async Task<BaseModel> RemoveAddressUsageTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Remove)
                {
                    #region [@  Add     @]

                    var response = await client.RemoveAddressUsageAsync(
                        new RemoveAddressUsageRequest()
                        {
                            AddressID = entity.Addresses[0].ID,
                            AddressUsageEnum = entity.Addresses[0].AddressUsageEnumToUpdate,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });

                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                return new BaseModel
                {
                    IsError = true,
                    Message = "EventAction not defined"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        public static async Task<BaseModel> RemovePhoneTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Remove)
                {
                    #region [@  Add     @]

                    var response = await client.RemovePhoneAsync(
                        new RemovePhoneRequest()
                        {
                            PhoneID = entity.Phones[0].PhoneId,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                return new BaseModel
                {
                    IsError = true,
                    Message = "EventAction not defined"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }

        public static async Task<BaseModel> RemovePhoneUsageTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                if (entity.EventAction == Models.Enums.Action.Remove)
                {
                    #region [@  Add     @]

                    var response = await client.RemovePhoneUsageAsync(
                        new RemovePhoneUsageRequest()
                        {
                            PhoneID = entity.Phones[0].PhoneId,
                            PhoneUsageEnum = entity.Phones[0].PhoneUsageEnum,
                            MessageGuid = new Guid(),
                            PropBag = PropBag,
                            RID = entity.UserRID,
                        });
                    client.Close();

                    #endregion
                    return new BaseModel
                    {
                        IsError = response.ReturnStatus.IsError,
                        Message = response.ReturnStatus.IsError
                                ? response.ReturnStatus.ErrorMessage
                                : response.ReturnStatus.GeneralMessage
                    };
                }
                return new BaseModel
                {
                    IsError = true,
                    Message = "EventAction not defined"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new BaseModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }

    }

}
