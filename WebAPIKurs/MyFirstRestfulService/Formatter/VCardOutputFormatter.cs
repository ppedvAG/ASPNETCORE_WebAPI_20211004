using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Formatter
{
    //https://docs.microsoft.com/de-de/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-5.0
    public class VCardOutputFormatter : TextOutputFormatter
    {
        public VCardOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(Contact).IsAssignableFrom(type) || typeof(IEnumerable<Contact>).IsAssignableFrom(type);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<VCardOutputFormatter>>();
            
            
            
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<Contact> contacts)
            {
                foreach (var contact in contacts)
                {
                    //Gleich geht es hier weiter :-) 
                    FormatVCard(buffer, contact, logger);
                }
            }
            else
            {
                FormatVCard(buffer, (Contact)context.Object, logger);
            }

            await httpContext.Response.WriteAsync(buffer.ToString());
        }


        private static void FormatVCard(StringBuilder buffer, Contact contact, ILogger logger)
        {
            buffer.AppendLine("BEGIN:VCARD");
            buffer.AppendLine("VERSION:2.1");
            buffer.AppendLine($"N:{contact.Lastname};{contact.Firstname}");
            buffer.AppendLine($"FN:{contact.Firstname} {contact.Lastname}");
            buffer.AppendLine($"UID:{contact.Id}");
            buffer.AppendLine("END:VCARD");

            logger.LogInformation("Writing {FirstName} {LastName}",
                contact.Firstname, contact.Lastname);
        }
    }
}
