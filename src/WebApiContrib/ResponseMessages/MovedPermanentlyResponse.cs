using System;
using System.Net;

namespace TestNuGet.ResponseMessages
{
    public class MovedPermanentlyResponse : ResourceIdentifierBase
    {
        public MovedPermanentlyResponse() : base(HttpStatusCode.MovedPermanently)
        {
        }

        public MovedPermanentlyResponse(Uri resource) : base(HttpStatusCode.MovedPermanently, resource)
        {
        }
    }
}