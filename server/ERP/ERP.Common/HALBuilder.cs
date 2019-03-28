using System;
using System.Collections.Generic;
using System.Reflection;
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

        public class HalResource
        {

        }

        public class HalCollection
        {

        }

        public class HalRepresentation
        {
            private IDictionary<string, object> FullRepresentation { get; set; }
            private IDictionary<string, HalLink> _links { get; set; }
            private IDictionary<string, object> _embedded { get; set; }

            public HalRepresentation()
            {
                FullRepresentation = new Dictionary<string, object>();
                _links = new Dictionary<string, HalLink>();
                _embedded = new Dictionary<string, object>();
            }

            public HalRepresentation AddLink(string rel, string href, HttpType type)
            {
                this._links.Add(new KeyValuePair<string, HalLink>(rel, new HalLink(href, type)));
                return this;
            }

            public HalRepresentation AddEmbeddedResource(string name, IEnumerable<object> collection, string itemBaseLink)
            {
                var collectionWithLinks = collection.Select(item => new { item, });
                this._embedded.Add(new KeyValuePair<string, object>(name, collection));
                return this;
            }
            public HalRepresentation AddEmbeddedResource(string name, object resource)
            {
                // TODO: Change this to the proper HAL format
                //  This means the resource fields will be at the base level of the HAL object
                //  _embedded will consist of the embedded object fields within the given object
                this._embedded.Add(new KeyValuePair<string, object>(name, resource));
                return this;
            }

            public HalRepresentation AddBaseResource(object resource)
            {
                this.FullRepresentation = resource.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(resource, null));
                return this;
            }

            public IDictionary<string, object> GetRepresentation()
            {
                FullRepresentation.Add("_links", _links);
                FullRepresentation.Add("_embedded", _embedded);
                return FullRepresentation;
            }
        }
    }
}
