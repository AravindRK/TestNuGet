﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TestNuGet.Conneg;

namespace TestNuGet.MessageHandlers
{
    public class NotAcceptableMessageHandler : DelegatingHandler
    {
	    private readonly IContentNegotiator contentNegotiator;
        private readonly IEnumerable<MediaTypeFormatter> mediaTypeFormatters;

	    public NotAcceptableMessageHandler(HttpConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

	        this.contentNegotiator = configuration.Services.GetContentNegotiator();
		    this.mediaTypeFormatters = configuration.Formatters;
        }

        public NotAcceptableMessageHandler(HttpConfiguration configuration, HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

	        this.contentNegotiator = configuration.Services.GetContentNegotiator();
		    this.mediaTypeFormatters = configuration.Formatters;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
	        var result = contentNegotiator.Negotiate(mediaTypeFormatters, request.Headers.Accept);
            if (result == null)
                return Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.NotAcceptable));

            return base.SendAsync(request, cancellationToken);
        }
    }
}
