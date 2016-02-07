using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LG.Services.AMS;
using LG.Services.GPH;


namespace LG.Data.Core.Auth
{
    internal static class AuthenticationOperations
    {
        internal static async Task<LG.Data.Models.Identity.UserEntity>
           IdentityLogin(String username, String password)
        {
            return await Login(username, password);
        }

        internal static async Task<LG.Data.Models.Identity.UserEntity>
            Login(String username, String password)
        {
            var result = new LG.Data.Models.Identity.UserEntity();
            var serviceResult = await AuthenticationOperations.GetLogin(username, password);

            if (!serviceResult.ReturnStatus.IsError)
            {

                if (serviceResult.LoginInfo.RID.HasValue
                    && serviceResult.LoginInfo.IsActive.HasValue
                    && serviceResult.LoginInfo.IsActive.Value)
                {
                    var dataEntity = await LoadUserData(serviceResult.LoginInfo.RID.Value);
                    result = new LG.Data.Models.Identity.UserEntity
                    {
                        RolodexItemId = serviceResult.LoginInfo.RID.Value,
                        RolodexItemType = AuthenticationDataTransformation.GetRolodexItemType(
                            dataEntity.GeneralInfo.BEntity.RTypeEnum.GetHashCode()),
                        IsActive = dataEntity.GeneralInfo.BEntity.IsActive,
                        IsTesting = dataEntity.GeneralInfo.BEntity.IsTesting,
                        Person = new LG.Data.Models.Shared.PersonInfo()
                        {
                            FName = dataEntity.GeneralInfo.PersonInfo.FName,
                            MName = dataEntity.GeneralInfo.PersonInfo.MName,
                            LName = dataEntity.GeneralInfo.PersonInfo.LName,
                            Dob =
                                dataEntity.GeneralInfo.PersonInfo.Dob.HasValue
                                    ? dataEntity.GeneralInfo.PersonInfo.Dob.Value
                                    : new DateTime(1900, 1, 1),
                            Gender = dataEntity.GeneralInfo.PersonInfo.Gender.HasValue ? dataEntity.GeneralInfo.PersonInfo.Gender.Value : 1
                        }
                    };
                    if (dataEntity.GeneralInfo.BEntity.EmailAddresses.Any())
                    {
                        result.ListEmails = new List<LG.Data.Models.Shared.EmailAddress>();
                        foreach (var email
                            in dataEntity.GeneralInfo.BEntity.EmailAddresses)
                        {
                            result.ListEmails.Add(
                                AuthenticationDataTransformation.GetEmailAddress(email));
                        }
                    }
                    if (dataEntity.GeneralInfo.BEntity.Phones.Any())
                    {
                        result.ListPhones = new List<LG.Data.Models.Shared.PhoneBase>();
                        foreach (var phone
                            in dataEntity.GeneralInfo.BEntity.Phones)
                        {
                            result.ListPhones.Add(
                                AuthenticationDataTransformation.GetPhone(phone));
                        }
                    }
                    if (dataEntity.GeneralInfo.BEntity.Addresses.Any())
                    {
                        result.ListAddresses = new List<LG.Data.Models.Shared.Address>();
                        foreach (var address
                            in dataEntity.GeneralInfo.BEntity.Addresses)
                        {
                            result.ListAddresses.Add(
                                AuthenticationDataTransformation.GetAddress(address));
                        }
                    }
                }
            }
            return result;
        }
        internal static async Task<LG.Services.GPH.GetUserGeneralInfoResponse> LoadUserData(Int64 RID)
        {
            string message;
            var client = LG.Services.ClientConnection.GetGphConnection();
            try
            {
                client.Open();
                var result = await client.GetUserGeneralInfoAsync(new GetUserGeneralInfoRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    RID = RID
                });
                return result;
            }
            catch (Exception exception)
            {
                message = exception.ToString();
                client.Abort();
            }
            finally
            {
                if (client.State != System.ServiceModel.CommunicationState.Closed)
                {
                    client.Close();
                }
            }
            return new GetUserGeneralInfoResponse()
            {
                ReturnStatus = new LG.Services.GPH.ReturnStatus()
                {
                    IsError = true,
                    ErrorMessage = String.Format("Failed:{0}", message)
                }
            };
        }
        internal static async Task<LG.Services.AMS.AuthenticateUserResponse>
            GetLogin(String username, String password)
        {
            var client = LG.Services.ClientConnection.GetAmsConnection();
            try
            {
                client.Open();
                var response = await client.AuthenticateUserAsync(new AuthenticateUserRequest()
                {
                    MessageGuid = Guid.NewGuid(),
                    UserName = username,
                    Password = password
                });
                client.Close();
                return response;
            }
            catch (Exception ex)
            {
                client.Abort();
                client.Close();
                return new AuthenticateUserResponse()
                {
                    ReturnStatus = new LG.Services.AMS.ReturnStatus()
                    {
                        IsError = true,
                        ErrorMessage = ex.ToString()
                    }
                };
            }
        }
    }
    internal static class AuthenticationDataTransformation
    {
        internal static LG.Data.Models.Enums.RolodexItemType GetRolodexItemType(int value)
        {
            var result = LG.Data.Models.Enums.RolodexItemType.Undefined;
            LG.Data.Models.Enums.Helpers.ParseUtility.TryParse<LG.Data.Models.Enums.RolodexItemType>(value, out result);
            return result;
        }

        internal static LG.Data.Models.Shared.EmailAddress GetEmailAddress(
            LG.Services.GPH.EmailAddress email)
        {
            var data = email.EmailAddressUsages.First();
            return new LG.Data.Models.Shared.EmailAddress()
            {
                Email = email.Email,
                IsPrimary = data.IsPrimary,
                EmailUsageEnum = GetEmailAddressUsage(data.EmailAddressUsageEnum.GetHashCode()),
                Id = email.ID.HasValue ? email.ID.Value : 0
            };
        }
        internal static LG.Services.EMS.EmailAddressUsageEnum GetEmailAddressUsage(
            int value)
        {
            var result = LG.Services.EMS.EmailAddressUsageEnum.Undefined;
            LG.Data.Models.Enums.Helpers.ParseUtility.TryParse<LG.Services.EMS.EmailAddressUsageEnum>(
                value.ToString(CultureInfo.InvariantCulture), out result);
            return result;
        }
        internal static LG.Data.Models.Shared.PhoneBase GetPhone(
            LG.Services.GPH.Phone phone)
        {
            var data = phone.PhoneUsages.First();
            return new LG.Data.Models.Shared.PhoneBase()
            {
                PhoneNumber = phone.PhoneNumber,
                IsPrimary = data.IsPrimary,
                PhoneUsageEnum = GetPhoneUsageEnum(data.PhoneUsageEnum.GetHashCode()),
                PhoneId = phone.ID.HasValue ? phone.ID.Value : 0,
                CountryCode = phone.PhoneCountryCode,
                PhoneExtension = phone.PhoneExtension
            };
        }
        internal static LG.Services.EMS.PhoneUsageEnum GetPhoneUsageEnum(int value)
        {
            var result = LG.Services.EMS.PhoneUsageEnum.Undefined;
            LG.Data.Models.Enums.Helpers.ParseUtility.TryParse<LG.Services.EMS.PhoneUsageEnum>(
                value.ToString(CultureInfo.InvariantCulture),
                out result);
            return result;
        }

        internal static LG.Data.Models.Shared.Address GetAddress(
          LG.Services.GPH.Address address)
        {
            var data = address.AddressUsages.First();
            return new LG.Data.Models.Shared.Address()
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode,
                IsPrimary = data.IsPrimary,
                AddressUsageEnum = data.AddressUsageEnum.GetHashCode(),
                ID = address.ID.HasValue ? address.ID.Value : 0
            };
        }
        internal static LG.Services.CMS.AddressUsageEnum GetAddressUsageEnum(
            int value)
        {
            var result = LG.Services.CMS.AddressUsageEnum.Undefined;
            LG.Data.Models.Enums.Helpers.ParseUtility.TryParse<LG.Services.CMS.AddressUsageEnum>(
                value.ToString(CultureInfo.InvariantCulture),
                out result);
            return result;
        }

    }
}
