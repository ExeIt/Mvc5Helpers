using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;

namespace Torchlight.Mvc5.Common.Libs.Helpers
{    
    public static class AntiTamperExtensions
    {
        //private const string MachinePurpose = "MyApp:Username:{0}";
        //private const string Anonymous = "<anonymous>";

        //static string GetMachineKeyPurpose(IPrincipal user)
        //{
        //    return string.Format(MachinePurpose, user.Identity.IsAuthenticated ? user.Identity.Name : Anonymous);
        //}

        /// <summary>
        /// Adds all necessary anti-tamper tokens for the given model.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static MvcHtmlString AntiTamperTokens<TModel>(this HtmlHelper<TModel> htmlHelper, TModel model)
        {
            var html = new StringBuilder();

            foreach (var property in model.GetAntiTamperPropertyInfos())
            {
                // generate a hidden field with the anti-tamper token

                var fieldName = "__Tamper." + property.Name;
                var data = Encoding.UTF8.GetBytes(property.GetValue(model, null).ToString());
                ////string[] myPurposes = new string[] { "antim tamper" };

               //var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal);

                //var encryption = MachineKey.Protect(data, "anti-tamper");

                //html.AppendFormat("<input id=\"{1}\" name=\"{1}\" type=\"hidden\" value=\"{0}\" />",
                //    MachineKey.Protect(data, purpose), fieldName);
                html.AppendFormat("<input id=\"{1}\" name=\"{1}\" type=\"hidden\" value=\"{0}\" />",
                    MachineKey.Encode(data, MachineKeyProtection.All), fieldName);
            }

            return new MvcHtmlString(html.ToString());
        }
        
        /// <summary>
        /// Validates the anti tamper tokens.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="formData">The form data.</param>
        public static void ValidateAntiTamperTokens(object value, NameValueCollection formData)
        {
            foreach (var property in value.GetAntiTamperPropertyInfos())
            {
                // extract the encrypted value from the form data and compare it to the value on the view model
                var fieldName = "__Tamper." + property.Name;

                var previousValue = formData[fieldName];
                
                if (String.IsNullOrEmpty(previousValue))
                    throw new HttpRequestException(String.Format("Property {0} has been tampered with",
                        property.Name));

                try
                {
                    previousValue = DecryptData(previousValue);
                    
                    if (!Equals(Convert.ChangeType(previousValue, property.PropertyType),
                        property.GetValue(value, null)))

                        throw new HttpRequestException(String.Format("Property {0} has been tampered with", property.Name));
                }
                catch (ArgumentException ex)
                {
                    throw new HttpRequestException(String.Format("Property {0} has been tampered with", property.Name), ex);
                }
            }
        }

        public static string DecryptData(string encrptedValue)
        {
            var data = MachineKey.Decode(encrptedValue, MachineKeyProtection.All);
            return data != null ? Encoding.UTF8.GetString(data) : null;

            //var dataInBytes = Encoding.ASCII.GetBytes(encrptedValue);
            //var purpose = GetMachineKeyPurpose(Thread.CurrentPrincipal);

            //byte[] data;

            //try
            //{
            //    data = MachineKey.Unprotect(dataInBytes, purpose);
            //}
            //catch (Exception ex)
            //{
            //    var x = ex.InnerException;
            //    throw;
            //}

            //return data != null ? Encoding.UTF8.GetString(data) : null;
        }

        private static readonly Dictionary<Type, List<PropertyInfo>> AntiTamperProperties =
            new Dictionary<Type, List<PropertyInfo>>();
        
        /// <summary>
        /// Gets and caches the anti tamper property infos.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetAntiTamperPropertyInfos<TModel>(this TModel model)
        {
            var type = model.GetType();

            lock (AntiTamperProperties)
            {
                if (!AntiTamperProperties.ContainsKey(type))
                {
                    var properties = new List<PropertyInfo>();

                    foreach (var property in type.GetProperties())
                    {
                        // we're only interested in properties that have the attribute
                        var att = (TamperProtectAttribute)Attribute.GetCustomAttribute(property, typeof(TamperProtectAttribute));

                        if (att != null)
                        {
                            properties.Add(property);
                        }
                    }

                    AntiTamperProperties.Add(type, properties);
                }
                
                return AntiTamperProperties[type];
            }
        }
    }
    
    /// <summary>
    /// Represents an attribute that is used to prevent tampering with data in a request
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]

    public class ValidateAntiTamperTokensAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            foreach (object value in filterContext.ActionParameters.Values)
            {
                AntiTamperExtensions.ValidateAntiTamperTokens(value, filterContext.HttpContext.Request.Form);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
