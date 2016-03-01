using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datalus.Web.Domain
{
    public class Address
    {
        public int Id { get; set; }


        /*
        private int _id;

  
        public int getID()
        {
            return _id;
        }

        public void setID(int value)
        {
            _id = value;
        }
        */


        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set;}
        public int StateProvinceId { get; set;}
        public int CountryId { get; set;}
        public string ZipCode { get; set;}
        public string StateProvincesName { get; set; }
        public string CountriesName { get; set; }
    }
}
