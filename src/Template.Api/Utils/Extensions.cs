using System.Collections.Generic;
using System.Linq;
using Halcyon.HAL;
using Halcyon.Web.HAL;
using Microsoft.AspNetCore.Http;
using Template.Api.Customers;

namespace Template.Api.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Convert to Hal Responses
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static IEnumerable<HALResponse> ToHalResponses(
            this IEnumerable<CustomerInListDto> customers,
            HttpRequest request)
        {
            return
                customers
                    .Select(s =>
                        new HALResponse(s)
                            .AddSelfLink(request)
                            .AddEmbeddedCollection(
                                "_links",
                                new[]
                                {
                                    AddLink("self", $"/api/Customers/{s.Id}", "GET")
                                })
                            .AddEmbeddedCollection(
                                "_actions",
                                new[]
                                {
                                    AddLink("register", $"api/Customers/{s.Id}", "PUT"),
                                    AddLink("register", $"api/Customers/{s.Id}/movies", "POST")
                                }))
                    .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HALResponse ToHalResponse<T>(this T result, HttpRequest request)
        {
            return
                new HALResponse(result)
                    .AddSelfLink(request);
        }

        private static Link AddLink(string embeddedName, string link)
        {
            return new Link(embeddedName, link);
        }
        private static Link AddLink(string embeddedName, string link, string method)
        {
            return new Link(embeddedName, link, method: method);
        }
    }
}
