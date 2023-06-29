using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Benday.YamlDemoApp.WebUi
{
    public static partial class ExtensionMethods
    {
        public static string GetReferer(this HttpContext context)
        {
            return GetHeaderValue(context, "Referer");
        }
        
        public static string GetUserAgent(this HttpContext context)
        {
            return GetHeaderValue(context, "User-Agent");
        }
        
        public static string GetRequestIpAddress(this HttpContext context)
        {
            if (context == null ||
            context.Connection == null ||
            context.Connection.RemoteIpAddress == null)
            {
                return null;
            }
            else
            {
                return context.Connection.RemoteIpAddress.ToString();
            }
        }
        
        public static string GetHeaderValue(this HttpContext context, string headerName)
        {
            if (headerName == null)
            {
                throw new ArgumentNullException(nameof(headerName), "Argument cannot be null.");
            }
            
            if (context == null ||
            context.Request == null ||
            context.Request.Headers == null ||
            context.Request.Headers.ContainsKey(headerName) == false)
            {
                return null;
            }
            else
            {
                var value = context.Request.Headers[headerName];
                
                return value.ToString();
            }
        }
        
        public static IHtmlContent EditorForTemplateRow<TModel, TResult>(
            this IHtmlHelper<ViewModelRowCollection<TModel>> htmlHelper, HtmlEncoder encoder,
            Expression<Func<ViewModelRowCollection<TModel>, TResult>> expression)
            where TModel : new()
        {
            var writer = new System.IO.StringWriter();
            
            var builder = writer.GetStringBuilder();
            
            var templateRowId = htmlHelper.Id("templateRow");
            
            var rowTemplateIndex = Int32.MaxValue;
            
            builder.Append("<script id=\"");
            builder.Append(templateRowId);
            builder.Append("\" type=\"text/x-custom-template\">");
            builder.AppendLine();
            
            htmlHelper.EditorFor(model => model[rowTemplateIndex]).WriteTo(writer, encoder);
            
            builder.Replace($"[{rowTemplateIndex}]", "[ROWID]");
            builder.Replace($"_{rowTemplateIndex}__", "_ROWID__");
            builder.Replace($"\"{rowTemplateIndex}\"", "\"0\"");
            
            builder.AppendLine("</script>");
            
            return htmlHelper.Raw(builder.ToString());
        }
        
        public static IHtmlContent EditorForTemplateRowTable<TModel, TResult>(
            this IHtmlHelper<ViewModelRowCollection<TModel>> htmlHelper, HtmlEncoder encoder,
            Expression<Func<ViewModelRowCollection<TModel>, TResult>> expression)
            where TModel : new()
        {
            HtmlContentBuilder builder = new HtmlContentBuilder();
            
            for(int index = 0; index < htmlHelper.ViewData.Model.Count; index++)
            {
                builder.AppendHtml(htmlHelper.EditorFor(model => model[index]));
            }
            
            builder.AppendHtml(EditorForTemplateRow(htmlHelper, encoder, expression));
            
            return builder;
        }
    }
}