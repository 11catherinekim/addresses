using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Datalus.Web.Domain
{
    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LongCode { get; set; }
    }
}
