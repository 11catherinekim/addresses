using Datalus.Web.Domain;
using Datalus.Web.Models.Requests;
using Datalus.Web.Models.Requests.Tests;
using Datalus.Web.Models.Responses;
using Datalus.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Datalus.Web.Controllers.Api
{
    [RoutePrefix("api/addresses")]
    public class AddressesApiController : BaseApiController
    {
        [Route, HttpPost]
        public HttpResponseMessage Add(AddressesAddRequest model)
        {
            try
            {
                if (!IsModelValid(model))
                {
                    return GetInvalidResponse(model);
                }

                ItemResponse<int> response = new ItemResponse<int>();
                response.Item = AddressesService.AddAddresses(model);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return GetErrorResponse(ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(AddressesUpdateRequest model, int id)
        {
            if (ModelState.IsValid)
            {
                AddressesService.UpdateAddresses(model);
                SuccessResponse response = new SuccessResponse();
                return Request.CreateResponse(response);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage SelectByIdAddresses(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            Address a = AddressesService.GetAddress(id);

            ItemResponse<Address> response = new ItemResponse<Address>();
            response.Item = a;
            return Request.CreateResponse(response);
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAllAddresses()
        {
            ItemsResponse<Address> response = new ItemsResponse<Address>();
            response.Items = AddressesService.GetAllAddresses();
            return Request.CreateResponse(response);
        }

        //countries/9/stateprovices
        [Route("StateProvinces/country/{countryId:int}"), HttpGet]
        public HttpResponseMessage GetStateProvincesByCountryId(int countryId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }


            //ItemsResponse <StateProvince> stateProvinceList = AddressesService.GetStates(countryId);
            ItemsResponse<StateProvince> response = new ItemsResponse<StateProvince>();
            response.Items = AddressesService.GetStates(countryId);
            return Request.CreateResponse(response);
        }

        [Route("Countries"), HttpGet]
        public HttpResponseMessage GetCountries()
        {
          
            ItemsResponse<Country> response = new ItemsResponse<Country>();
            response.Items = AddressesService.CountriesGetAll();
            return Request.CreateResponse(response);
        }


        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage DeleteAddresses(int id)
        
        {
            AddressesService.DeleteAddress(id);
            SuccessResponse response = new SuccessResponse();
            return Request.CreateResponse(response);
        }  
       

    }
}


