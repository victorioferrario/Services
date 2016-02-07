using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using LG.Data.Models;
using LG.Data.Models.Users;
using LG.Services;
using LG.Services.AMS;
using LG.Services.EMS;
using LG.Services.UMS;

using ReturnStatus = LG.Services.EMS.ReturnStatus;

namespace LG.Data.Core.Users
{
    public static class UsersDataService
    {
        public static LG.Data.Models.Shared.ValidEmail VerifyEmail(String email)
        {
            return LG.Data.Core.Shared.VerfiyInformation.VerifyEmail(email);
        }
        public static List<Int64> VerifyEmailList(String email)
        {
            return LG.Data.Core.Shared.VerfiyInformation.VerifyEmailList(email);
        }
        public static async Task<UserModel> Get(UserModel user)
        {
            return await GetUserDetailInfoTask(user);
        }
        
        

        public static async Task<UserModel> UpdateAsync(UserModel user)
        {
            user.EventAction = Models.Enums.Action.Update;
            var response = await
                Shared.SaveContactInformation.SaveEmailTask(user)
                .ContinueWith(nextTask =>
                    Shared.SaveContactInformation.SavePhoneTask(user)
                ).ContinueWith(nextTask =>
                    Shared.SaveContactInformation.SavePersonInfoTask(user));
            return response.Result;
        }

        public static async Task<UserModel> UpdateAsync(
            UserModel user,
            Int64 RID,
            Models.Enums.Action action)
        {

            user.EventAction = Models.Enums.Action.Add; 
            user.UserRID = RID;

            var response
                = LG.Data.Core.Shared.SaveContactInformation.SavePersonInfoTask(
                user);

            var response2
                = LG.Data.Core.Shared.SaveContactInformation.SaveEmailTask(
                user);

            var response3
                = LG.Data.Core.Shared.SaveContactInformation.SavePhoneTask(
                user);

            await response;
            await response2;
            await response3;
            if (response.IsCompleted
                && response2.IsCompleted
                && response3.IsCompleted)
            {
                return await Get(new UserModel()
                {
                    ClientRID = user.ClientRID,
                    UserRID = RID
                });
            }
            if (response.IsFaulted
                || response2.IsFaulted
                || response3.IsFaulted)
            {
                return await Get(new UserModel()
                {
                    ClientRID = user.ClientRID,
                    UserRID = RID
                });
            }
            return null;
        }

        public static async Task<UserModel> CreateAsync(UserModel entity)
        {
            return await CreateBEntityTask(entity);
        }
        public static async Task<UserModel> CreateAndSaveAsync(UserModel entity)
        {
            return await CreateAsync(entity).ContinueWith(
                (taskAsync) =>
                    AddToClientTask(taskAsync.Result)).ContinueWith(
                        (taskAsync2) =>
                            UpdateAsync(entity,
                            taskAsync2.Result.Result.UserRID, Models.Enums.Action.Add).Result
                );
        }
        public static async Task<UserModel> CreateAndSavePrimaryAsync(UserModel entity)
        {
            var save = await CreateAsync(entity).ContinueWith(
                (taskAsync) =>
                    AddToClientTask(taskAsync.Result)).ContinueWith(
                        (taskAsync2) =>
                            UpdateAsync(entity,
                            taskAsync2.Result.Result.UserRID, Models.Enums.Action.Add)
                );

            if (save.IsCompleted)
            {
                return await AddAsPrimaryTask(new UserModel()
                {
                    ClientRID = save.Result.ClientRID,
                    UserRID = save.Result.UserRID
                });

            }
            else
            {
                return null;
            }
        }
        public static async Task<UserModel> SavePrimaryAsync(UserModel entity)
        {
            return await
                AddAsPrimaryTask(new UserModel()
            {
                ClientRID = entity.ClientRID,
                UserRID = entity.UserRID
            });
        }



        public static async Task<UserListModel> AddAsync(UserModel user)
        {
            if (!AddUser(user).IsError)
            {
                return await AddToClientTask(
                    user).ContinueWith(
                        prevTask => GetClientUsersTask(user)).Result;
            }
            else
            {
                return null;
            }
        }
        public static async Task<UserListModel> AddAsync(UserModel user, Int64 ClientRID)
        {
            user.ClientRID = ClientRID;
            if (!AddUser(user).IsError)
            {
                return await AddToClientTask(
                    user).ContinueWith(
                        prevTask => GetClientUsersTask(user)).Result;
            }
            else
            {
                return null;
            }
        }


        public static UserModel AddUser(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = client.AddUserToClient(
                    new AddClientUserRequest
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        ClientRID = user.ClientRID,
                        UserType = user.UserTypeEnum,
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    UserRID = user.UserRID,
                    ClientRID = user.ClientRID,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }
        }
        public static async Task<UserListModel> AddUserToCorporation(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.AddUserToCorporationAsync(
                    new AddCorporationUserRequest()
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        CorporationRID = user.CorporationRID,
                        UserType = user.UserTypeEnum,
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });
                client.Close();
                return await GetCorporationListAsync();
            }
            catch (Exception ex)
            {
                client.Close();
                return null;
            }
        }
        public static async Task<UserListModel> RemoveCorporationUser(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.RemoveUserFromCorporationAsync(
                    new RemoveUserFromCorporationRequest()
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        CorporationRID = user.CorporationRID,
                        UserType = user.PrevUserTypeEnumInt == 1
                        ? LG.Services.UMS.UserTypeEnum.Admin
                        : LG.Services.UMS.UserTypeEnum.User,
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });

                client.Close();

                return await GetCorporationListAsync();
            }
            catch (Exception ex)
            {
                client.Close();
                return null;
            }
        }
        public static async Task<UserModel> UpdateCorporateUserAsync(UserModel user)
        {
            user.EventAction = Models.Enums.Action.Update;
            var response = await
                Shared.SaveContactInformation.SaveEmailTask(user)
                .ContinueWith(nextTask =>
                    Shared.SaveContactInformation.SavePhoneTask(user)
                ).ContinueWith(nextTask =>
                    Shared.SaveContactInformation.SavePersonInfoTask(user));
            return response.Result;
        }
        public static async Task<UserListModel> GetListAsync(UserModel entity)
        {
            return await GetClientUsersTask(entity);
        }
        public static async Task<UserListModel> GetCorporationListAsync()
        {
            return await GetCorporationUsersTask();
        }

        public static UserModel RemoveAsync(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = client.RemoveUserFromClient(
                    new RemoveUserFromClientRequest
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        ClientRID = user.ClientRID,
                        UserType = user.PrevUserTypeEnumInt == 2 ? UserTypeEnum.User : UserTypeEnum.Admin,
                        PropBag = "<PropBag></PropBag>"
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }
            //return await RemoveUserFromClientTask(user);
        }

        #region [@  PRIVATE METHODS     @]

        /// <summary>
        /// This method loads all Users for a client
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task<UserListModel> GetClientUsersTask(UserBase user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.GetClientUsersAsync(new GetClientUsersRequest
                {
                    MessageGuid = Guid.NewGuid(),
                    ClientRID = user.ClientRID
                });
                client.Close();
                return new UserListModel
                {
                    List = response.Users,
                    IsError = false,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserListModel
                {
                    IsError = false,
                    Message = "Failde:" + ex
                };
            }
        }
        /// <summary>
        /// This method loads all Users for a corporation
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static async Task<UserListModel> GetCorporationUsersTask()
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.GetCorporationUsersAsync(new GetCorporationUsersRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    CorporationRID = 10
                });
                client.Close();
                return new UserListModel
                {
                    List = response.Users,
                    IsError = false,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserListModel
                {
                    IsError = false,
                    Message = "Failde:" + ex
                };
            }
        }
        /// <summary>
        /// This method attaches a User to a Client
        /// </summary>
        /// <param name="user">Models.Users.UserModel</param>
        /// <returns>Task=>System.Boolean</returns>
        private static async Task<UserModel> AddToClientTask(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.AddUserToClientAsync(
                    new AddClientUserRequest
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        ClientRID = user.ClientRID,
                        UserType = user.UserTypeEnum,
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    UserRID = user.UserRID,
                    ClientRID = user.ClientRID,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }
        }
        private static async Task<UserModel> AddToClientTask(UserModel user, Int64 RID)
        {

            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                user.UserRID = RID;
                var response = await client.AddUserToClientAsync(
                    new AddClientUserRequest
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        ClientRID = user.ClientRID,
                        UserType = user.UserTypeEnumInt == 1 ? UserTypeEnum.User : UserTypeEnum.Admin,
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }
        }

        private static async Task<UserModel> AddAsPrimaryTask(UserModel user)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = await client.AddExistingBEntityAsContactAsync(
                    new AddExistingBEntityAsContactRequest()
                    {
                        MessageGuid = new Guid(),
                        ContactForRID = user.ClientRID,
                        ContactRID = user.UserRID,
                        ContactUsages = new List<ContactUsage>()
                        {
                            new ContactUsage()
                            {
                                IsActive = true,
                                ContactUsageEnum = ContactUsageEnum.PrimaryContact}
                        },
                        PropBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>"
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }

        }

        /// <summary>
        /// This method removes a User from a Client
        /// </summary>
        /// <param name="user">Models.Users.UserModel</param>
        /// <returns>Task=>System.Boolean</returns>
        private static async Task<UserModel> RemoveUserFromClientTask(UserModel user)
        {
            var client = ClientConnection.GetConnection();
            try
            {
                client.Open();
                var response = await client.RemoveUserFromClientAsync(
                    new RemoveUserFromClientRequest
                    {
                        MessageGuid = new Guid(),
                        UserRID = user.UserRID,
                        ClientRID = user.ClientRID,
                        UserType = user.UserTypeEnum,
                        PropBag = _propBag
                    });
                client.Close();
                return new UserModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }

        }


        private static async Task<LoadBEntityDataResponse> GetUserDetailTask(UserModel user)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = await client.LoadBEntityDataAsync(new LoadBEntityDataRequest
                {
                    MessageGuid = new Guid(),
                    RID = user.UserRID

                });
                client.Close();

                return response;
            }
            catch (Exception ex)
            {
                client.Close();
                return new LoadBEntityDataResponse
                {
                    ReturnStatus = new ReturnStatus
                    {
                        IsError = true,
                        ErrorMessage = ex.ToString(),
                        GeneralMessage = ex.Message
                    }
                };
            }
        }
        private static async Task<UserModel> GetUserDetailInfoTask(UserModel user)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = await client.LoadBEntityDataAsync(new LoadBEntityDataRequest
                {
                    MessageGuid = new Guid(),
                    RID = user.UserRID
                });
                client.Close();

                return LG.Data.Models.Users.ParseFactory.ParseBEntity(response);
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel()
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
        }
        private static async Task<UserModel> CreateBEntityTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var response = await client.CreateBEntityAsync(new CreateBEntityRequest
                {
                    MessageGuid = new Guid(),
                    RType = entity.UserTypeEnum == UserTypeEnum.Admin ? RTypeEnum.Administrator : RTypeEnum.Contact,
                    IsActive = true,
                    IsTesting = false,
                    PropBag = _propBag
                });
                client.Close();
                return new UserModel
                {
                    ClientRID = entity.ClientRID,
                    UserRID = response.RID.HasValue ? response.RID.Value : 0,
                    IsError = !response.RID.HasValue,
                    Message = response.RID.HasValue ? "Success" : "Error: Null RID" + response.ReturnStatus.ErrorMessage
                };
            }
            catch (Exception ex)
            {
                client.Close();
                return new UserModel
                {
                    IsError = true,
                    Message = "Error" + ex
                };
            }
        }
        private static async Task<UserListModel> CreatePrimaryContact(UserModel user)
        {
            var client = ClientConnection.GetEmsConnection();
            try
            {
                client.Open();
                var data = await client.AddNewBEntityAsContactAsync(
                    new AddNewBEntityAsContactRequest
                    {
                        MessageGuid = new Guid(),
                        Contact = new ContactInput
                        {
                            IsActive = true,
                            ContactForRID = user.ClientRID,
                            PersonInfo = new PersonInfoInput
                            {
                                FName = user.GeneralInfo.FName,
                                MName = user.GeneralInfo.MName,
                                LName = user.GeneralInfo.LName,
                            },
                            Phones = new List<PhoneInput>
                            {
                                new PhoneInput
                                {
                                    PhoneCountryCode = "01",
                                    PhoneNumber = user.Phone.PhoneNumber,
                                    PhoneExtension = "",
                                    PhoneUsages = new List<PhoneUsage>
                                    {
                                        new PhoneUsage
                                        {
                                            IsPrimary = true,
                                            PhoneUsageEnum = PhoneUsageEnum.Business
                                        }
                                    }
                                }
                            },
                            EmailAddresses = new List<EmailAddressInput>
                            {
                                new EmailAddressInput
                                {
                                    Email =user.EmailAddress.Email,
                                    EmailAddressUsages = new List<EmailAddressUsage>
                                    {
                                        new EmailAddressUsage
                                        {
                                            IsPrimary = true,
                                            EmailAddressUsageEnum = EmailAddressUsageEnum.Group
                                        }
                                    }
                                }
                            },
                            ContactUsages = new List<ContactUsage>
                            {
                                new ContactUsage
                                {
                                    IsActive = true,
                                    ContactUsageEnum = ContactUsageEnum.PrimaryContact
                                }
                            }
                        },
                        PropBag = _propBag
                    }).ContinueWith(
                         prevTask =>
                         {

                             user.UserRID = prevTask.Result.ContactRID;
                             var instanceOfUser = new UserModel
                             {
                                 ClientRID = user.ClientRID,
                                 UserRID = prevTask.Result.ContactRID,
                                 UserTypeEnum = UserTypeEnum.Admin,
                                 UserTypeEnumInt = UserTypeEnum.Admin.GetHashCode()
                             };

                             return AddAsync(instanceOfUser).ContinueWith(prevTask2
                                => GetClientUsersTask(instanceOfUser).Result);


                         });

            }
            catch (Exception ex)
            {
                client.Close();
                return new UserListModel
                {
                    IsError = true,
                    Message = ex.ToString()
                };
            }
            return GetClientUsersTask(new UserModel
            {
                ClientRID = user.ClientRID
            }).Result;
        }

        private static async Task<BaseModel> AddPrimaryContactTask(UserModel entity)
        {
            var client = ClientConnection.GetEmsConnection();
            client.Open();
            try
            {
                var response = await client.AddExistingBEntityAsContactAsync(
                    new AddExistingBEntityAsContactRequest
                    {
                        MessageGuid = new Guid(),
                        ContactRID = entity.UserRID,
                        ContactForRID = entity.ClientRID,
                        ContactUsages = new List<ContactUsage>
                        {
                            new ContactUsage
                            {
                                IsActive = true,
                                ContactUsageEnum = ContactUsageEnum.PrimaryContact
                            }
                        },
                        PropBag = _propBag
                    });
                client.Close();
                return new BaseModel
                {
                    IsError = response.ReturnStatus.IsError,
                    Message = response.ReturnStatus.IsError ? response.ReturnStatus.ErrorMessage : response.ReturnStatus.GeneralMessage
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
        /// Static propBag, to log what module is executing code.
        /// </summary>
        private static String _propBag = "<PropBag><Lib>LG.Data.Core</Lib><Class>LG.Data.Core.Users.UsersDataService</Class></PropBag>";

        #endregion

    }
}