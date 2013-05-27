using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Models
{
    public class SessionSettings
    {
        private class Setting
        {
            public int MaterialTypeId { get; set; }
            public int MaterialId { get; set; }
        }

        private List<Setting> settings;

        private HttpContextBase httpContext;
        private const string SessionSettingsKey = "SessionSettingsId";

        public static SessionSettings GetInstance(HttpContextBase httpContext)
        {
            SessionSettings sessionSettings = new SessionSettings();
            sessionSettings.httpContext = httpContext;

            if (httpContext.Session[SessionSettingsKey] == null)
            {
                sessionSettings.settings = new List<Setting>();
                httpContext.Session[SessionSettingsKey] = sessionSettings.settings;
            }
            else
            {
                sessionSettings.settings = (List<Setting>)httpContext.Session[SessionSettingsKey];
            }

            return sessionSettings;
        }

        public void SetMaterial(int materialTypeId, int materialId)
        {
            Setting setting = new Setting
            {
                MaterialTypeId = materialTypeId,
                MaterialId = materialId
            };

            if (!isMaterialExists(materialTypeId))
            {
                settings.Add(setting);
            }

            int index = settings.FindIndex(c => c.MaterialTypeId == materialTypeId);
            settings[index] = setting;

            httpContext.Session[SessionSettingsKey] = settings;
        }

        public int GetMaterialId(int materialTypeId)
        {
            if (!isMaterialExists(materialTypeId))
            {
                return -1;
            }

            return settings.Find(c => c.MaterialTypeId == materialTypeId).MaterialId;
        }

        public bool isMaterialExists(int materialTypeId)
        {
            return settings.Exists(c => c.MaterialTypeId == materialTypeId);
        }
    }
}
