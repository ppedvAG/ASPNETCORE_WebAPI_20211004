using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using MyFirstRestfulService.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Formatter
{
    //Dieser Formatter ist typisiert und kann es gerade so, mit einem Datentyp
    public class VCardInputFormatter : TextInputFormatter
    {
        //https://docs.microsoft.com/de-de/aspnet/core/web-api/advanced/custom-formatters?view=aspnetcore-5.0
        public VCardInputFormatter()
        {
            //Microsoft.Net.Http.Headers;
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8); //Encoding -> System.Text
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(Contact);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;

            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<InputFormatterResult>>();

            using var reader = new StreamReader(httpContext.Request.Body, encoding);

            string nameLine = null;
            string idLine = null;

            try
            {
                await ReadLineAsync("BEGIN:VCARD", reader, context, logger);
                await ReadLineAsync("VERSION:", reader, context, logger);
                nameLine = await ReadLineAsync("N:", reader, context, logger);


                var split = nameLine.Split(";".ToCharArray());
                //Manuelles Modelbinding (via InputFormatter) 

                var contact = new Contact
                {
                    Lastname = split[0].Substring(2),
                    Firstname = split[1]
                };

                string fnValue = await ReadLineAsync("FN:", reader, context, logger);
                idLine = await ReadLineAsync("UID:", reader, context, logger);
                var splitId = idLine.Split(":");
                contact.Id = Convert.ToInt32(splitId[1]);
                await ReadLineAsync("END:VCARD", reader, context, logger);

                logger.LogInformation("nameLine = {nameLine}", nameLine);

                return await InputFormatterResult.SuccessAsync(contact);
            }
            catch
            {
                logger.LogError("Read failed: nameLine = {nameLine}", nameLine);
                return await InputFormatterResult.FailureAsync();
            }
        }

        private static async Task<string> ReadLineAsync(string expectedText, StreamReader reader, InputFormatterContext context,
           ILogger logger)
        {
            string line = await reader.ReadLineAsync();

            if (!line.StartsWith(expectedText))
            {

                //BEGIN:VCARD
                //VERSION:
                //FN:
                //LN:
                var errorMessage = $"Looked for '{expectedText}' and got '{line}'";

                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                logger.LogError(errorMessage);

                throw new Exception(errorMessage);  //Wird diese nicht entfernt
            }

            return line;
        }


    }
}
