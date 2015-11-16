using System.Collections.Generic;

namespace Torchlight.Mvc5.Common.Libs.ModelAssets.MenuAssets
{
    public class TorchlightMenuModel
    {
        public TorchlightMenuModel()
        {
            IsEnabled = true;
            IsRsReport = false;

            RouteName = "Default";

            SubMenus = new List<TorchlightMenuModel>();
        }

        public MenuType Type { get; set; }

        public string Key { get; set; }

        public bool HasUpperDivider { get; set; }
        public bool HasLowerDivider { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsRsReport { get; set; }
        public bool IsDropDownItem { get; set; }

        public string KeyName { get; set; }
        public string RouteName { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public string ReportPath { get; set; }
        public string ReportText { get; set; }

        public string Target { get; set; }
        public string UrlPage { get; set; }
        public string UrlFolder { get; set; }
        public string Text { get; set; }

        public List<TorchlightMenuModel> SubMenus { get; set; }

        public string VirtualUrl
        {
            get
            {
                if (Type == MenuType.Report)
                    return string.Format("~/{0}/{1}?report={2}&title={3}", UrlFolder, UrlPage, ReportPath, ReportText);

                if (Type == MenuType.Webform)
                    return UrlFolder == "."
                               ? string.Format("~/{0}", UrlPage)
                               : string.Format("~/{0}/{1}", UrlFolder, UrlPage);

                // MVC link
                if (Type == MenuType.Mvc)
                {
                    return Action == null
                               ? string.Format("~/{0}", Controller)
                               : string.Format("~/{0}/{1}", Controller, Action);
                }

                return UrlPage;
            }
        }

        public enum MenuType
        {
            Webform,
            Mvc,
            Report,
            Script
        }
    }
}
