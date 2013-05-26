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

            int index = settings.FindIndex(c => c.MaterialTypeId == materialTypeId);

            if (index == -1)
            {
                settings.Add(setting);
            }
            else
            {
                settings[index] = setting;
            }

            httpContext.Session[SessionSettingsKey] = settings;
        }

        public int GetMaterialId(int materialTypeId)
        {
            return settings.Find(c => c.MaterialTypeId == materialTypeId).MaterialId;
        }

        public void RemoveMaterial(int materialTypeId)
        {
            settings.RemoveAt(settings.FindIndex(c => c.MaterialTypeId == materialTypeId));
        }

        public bool isMaterialExists(int materialTypeId)
        {
            return settings.Exists(c => c.MaterialTypeId == materialTypeId);
        }
    }
}
