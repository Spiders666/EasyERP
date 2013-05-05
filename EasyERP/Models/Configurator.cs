using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class Configurator
    {
        private class Setting
        {
            public MaterialType MaterialType { get; set; }
            public int MaterialId { get; set; }
        }

        private List<Setting> configuration;

        private HttpContextBase httpContext;
        private const string ConfiguratorSessionKey = "ConfiguratorId";

        public static Configurator GetInstance(HttpContextBase httpContext)
        {
            Configurator configurator = new Configurator();
            configurator.httpContext = httpContext;

            if (httpContext.Session[ConfiguratorSessionKey] == null)
            {
                configurator.configuration = new List<Setting>();
                httpContext.Session[ConfiguratorSessionKey] = configurator.configuration;
            }
            else
            {
                configurator.configuration = (List<Setting>)httpContext.Session[ConfiguratorSessionKey];
            }

            return configurator;
        }

        public void SetMaterial(MaterialType materialType, int materialId)
        {
            Setting setting = new Setting
            {
                MaterialType = materialType,
                MaterialId = materialId
            };

            int index = configuration.FindIndex(c => c.MaterialType == materialType);

            if (index == -1)
            {
                configuration.Add(setting);
            }
            else
            {
                configuration[index] = setting;
            }

            httpContext.Session[ConfiguratorSessionKey] = configuration;
        }

        public int GetMaterialId(MaterialType materialType)
        {
            return configuration.Find(c => c.MaterialType == materialType).MaterialId;
        }

        public void RemoveMaterial(MaterialType materialType)
        {
            configuration.RemoveAt(configuration.FindIndex(c => c.MaterialType == materialType));
        }

        public bool isMaterialExists(MaterialType materialType)
        {
            return configuration.Exists(c => c.MaterialType == materialType);
        }
    }
}
