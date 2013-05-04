using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Configurator
    {
        private HttpContextBase httpContext { get; set; }
        private const string ConfiguratorSessionKey = "ConfiguratorId";

        public static Configurator GetInstance(HttpContextBase httpContext)
        {
            Configurator configurator = new Configurator();
            configurator.httpContext = httpContext;

            if (httpContext.Session[ConfiguratorSessionKey] == null)
            {
                httpContext.Session[ConfiguratorSessionKey] = new List<Material>();
            }

            return configurator;
        }

        public List<Material> GetConfiguration()
        {
            return (List<Material>)httpContext.Session[ConfiguratorSessionKey];
        }

        public void AddToConfiguration(Material material)
        {
            List<Material> materials = GetConfiguration();

            if (materials.Find(m => m.Id == material.Id) == null)
            {
                materials.Add(material);
            }

            httpContext.Session[ConfiguratorSessionKey] = materials;
        }

        public void RemoveFromConfiguration(Material material)
        {
            List<Material> materials = GetConfiguration();
            materials.Remove(material);

            httpContext.Session[ConfiguratorSessionKey] = materials;
        }
    }
}
