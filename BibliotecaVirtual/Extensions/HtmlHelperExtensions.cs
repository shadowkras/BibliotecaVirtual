using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaVirtual.Extensions
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Retorna o nome da Action que invocou a view.
        /// </summary>
        /// <param name="htmlHelper">Classe de HtmlHelper.</param>
        /// <returns></returns>
        public static string ActionName(this IHtmlHelper htmlHelper)
        {
            var routeValues = htmlHelper.ViewContext.RouteData.Values;

            if (routeValues.ContainsKey("action"))
            {
                return (string)routeValues["action"];
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
