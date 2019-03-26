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
            public string Href { get; set; }
            public string Type { get; set; }

            public HalLink(string href, HttpType type)
            {
                Href = href;
                Type = Enum.GetName(typeof(HttpType), type);
            }
        }

        public class HalRepresentation
        {
            //public IEnumerable<HalLink> _links { get; set; }
            public IDictionary<string, HalLink> _links { get; set; }
            public IDictionary<string, object> _embedded { get; set; }

            public HalRepresentation()
            {
                _links = new Dictionary<string, HalLink>();
                _embedded = new Dictionary<string, object>();
            }
            public void AddEmbeddedResource(string name, IEnumerable<object> collection)
            {
                _embedded.Add(new KeyValuePair<string, object>(name, collection));
            }
            public void AddEmbeddedResource(string name, object resource)
            {
                // TODO: Change this to the proper HAL format
                //  This means the resource fields will be at the base level of the HAL object
                //  _embedded will consist of the embedded object fields within the given object
                _embedded.Add(new KeyValuePair<string, object>(name, resource));
            }
            public void AddLink(string rel, string href, HttpType type)
            {
                _links.Add(new KeyValuePair<string, HalLink>(rel, new HalLink(href, type)));
            }
        }
    }
}
