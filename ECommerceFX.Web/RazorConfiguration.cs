using System.Collections.Generic;
using FluentValidation;
using Nancy.ViewEngines.Razor;
using ECommerceFX.Data;

namespace ECommerceFX.Web
{
    public class RazorConfiguration : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return typeof (User).Assembly.FullName;
            yield return GetType().Assembly.FullName;
            yield return typeof (IValidator).Assembly.FullName;
        }

        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "System.Linq";
            yield return "Nancy.ViewEngines.Razor";
        }

        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}