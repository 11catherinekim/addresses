using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Datalus.Data;
using Datalus.Web.Models.Requests;
using Datalus.Web.Models.Requests.Tests;
using Datalus.Web.Domain;

namespace Datalus.Web.Services
{
    public class AddressesService : BaseService
    {
        public static int AddAddresses(AddressesAddRequest Addresses)
        {
            int id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   if (Addresses.Latitude != null)
                   {
                       paramCollection.AddWithValue("@Latitude", Addresses.Latitude);
                   }
                   if (Addresses.Longitude != null)
                   {
                       paramCollection.AddWithValue("@Longitude", Addresses.Longitude);
                   }
                   paramCollection.AddWithValue("@AddressLine1", Addresses.AddressLine1);
                   paramCollection.AddWithValue("@AddressLine2", Addresses.AddressLine2);
                   paramCollection.AddWithValue("@City", Addresses.City);
                   paramCollection.AddWithValue("@StateProvinceID", Addresses.StateProvinceId);
                   paramCollection.AddWithValue("@ZipCode", Addresses.ZipCode);
                   paramCollection.AddWithValue("@CountryId", Addresses.CountryId);
                   paramCollection.AddWithValue("@UserId", UserService.GetCurrentUserId());

                   SqlParameter newIdParameter = new SqlParameter("@id", System.Data.SqlDbType.Int);
                   newIdParameter.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(newIdParameter);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@id"].Value.ToString(), out id);
               }
               );          
            return id;
        }

        public static void UpdateAddresses(AddressesUpdateRequest Addresses)
        {

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Update"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Latitude", Addresses.Latitude);
                   paramCollection.AddWithValue("@Longitude", Addresses.Longitude);
                   paramCollection.AddWithValue("@AddressLine1", Addresses.AddressLine1);
                   paramCollection.AddWithValue("@AddressLine2", Addresses.AddressLine2);
                   paramCollection.AddWithValue("@City", Addresses.City);
                   paramCollection.AddWithValue("@StateProvinceId", Addresses.StateProvinceId);
                   paramCollection.AddWithValue("@ZipCode", Addresses.ZipCode);
                   paramCollection.AddWithValue("@CountryId", Addresses.CountryId);
                   paramCollection.AddWithValue("@UserId", UserService.GetCurrentUserId());
                   paramCollection.AddWithValue("@Id", Addresses.Id);
               }, returnParameters: null
               );
        }


        public static Address GetAddress(int id)
        {
            Address a = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@id", id);
               }, map: delegate (IDataReader reader, short set)
               {
                   a = MapAddress(reader);
               }
               );
            return a;
        }

        private static Address MapAddress(IDataReader reader)
        {
            Address a = new Address();
            int startingIndex = 0;
            a.Id = reader.GetSafeInt32(startingIndex++);
            a.DateAdded = reader.GetSafeDateTime(startingIndex++);
            a.DateModified = reader.GetSafeDateTime(startingIndex++);
            a.Latitude = reader.GetSafeDecimal(startingIndex++);
            a.Longitude = reader.GetSafeDecimal(startingIndex++);
            a.AddressLine1 = reader.GetSafeString(startingIndex++);
            a.AddressLine2 = reader.GetSafeString(startingIndex++);
            a.City = reader.GetSafeString(startingIndex++);
            a.StateProvinceId = reader.GetSafeInt32(startingIndex++);
            a.ZipCode = reader.GetSafeString(startingIndex++);
            a.CountryId = reader.GetSafeInt32(startingIndex++);
            a.StateProvincesName = reader.GetSafeString(startingIndex++);
            a.CountriesName = reader.GetSafeString(startingIndex++);
            return a;
        }

        public static List<Address> GetAllAddresses()
        {
            List<Address> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_SelectAll"
               , inputParamMapper: null
               , map: delegate (IDataReader reader, short set)
               {
                   Address address = MapAddress(reader);

                   if (list == null)
                   {
                       list = new List<Address>();
                   }

                   list.Add(address);
               }
               );
            return list;
        }

        // Countries Service
        public static List<Country> CountriesGetAll() {
            List <Country> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Countries_SelectAll"
                , inputParamMapper: null
                , map: delegate(IDataReader reader, short set)
                {
                Country bread = new Country();
                int startingIndex = 0;

                bread.CountryId = reader.GetSafeInt32(startingIndex++);
                bread.Name = reader.GetSafeString(startingIndex++);
                bread.Code = reader.GetSafeString(startingIndex++);
                bread.LongCode = reader.GetSafeString(startingIndex++);

                if(list == null)
                {

                    list = new List<Country>();
                }
                list.Add(bread);
            } 
            );
            return list;
        }

        //States Service
        public static List<StateProvince> GetStates(int countryId)
        {
            List <StateProvince> list = null;
            DataProvider.ExecuteCmd(GetConnection, "dbo.StateProvinces_SelectById"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@CountryId", countryId);
                }, map: delegate (IDataReader reader, short set)
                {
                    StateProvince water = new StateProvince();
                    int startingIndex = 0;
                    water.StateProvinceId = reader.GetSafeInt32(startingIndex++);
                    water.CountryId = reader.GetSafeInt32(startingIndex++);
                    water.StateProvinceCode = reader.GetSafeString(startingIndex++);
                    water.CountryRegionCode = reader.GetSafeString(startingIndex++);
                    water.IsOnlyStateProvinceFlag = reader.GetSafeBool(startingIndex++);
                    water.Name = reader.GetSafeString(startingIndex++);
                    water.TerritoryID = reader.GetSafeInt32(startingIndex++);
                    if(list == null)
                    {
                        list = new List<StateProvince>();
                    }
                    list.Add(water);
                }
                );
            return list;
        }


        public static void DeleteAddress(int id)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_Delete"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   paramCollection.AddWithValue("@Id", id);
               }, returnParameters: null);


        }
    }
}
