using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Common
{
    public static class HalBuilder
    {
        public enum HttpType
        {
            GET, POST, PUT, PATCH, DELETE
        }
        public class HalLink
        {
            public string href { get; set; }
            public string rel { get; set; }
            public HttpType type { get; set; }
        }
        public class HalEmbedded
        {
            public IDictionary<string, IEnumera>
        }
        public class HalRepresentation
        {
            public IEnumerable<HalLink> _links { get; set; }
            public __ _embedded { get; set; }
        }
        public 
    }
}
