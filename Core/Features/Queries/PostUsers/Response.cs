using System;
using Core.Features.Queries.Authentications;

namespace Core.Features.Queries.PostUsers
{
    public class Response
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public PostLoginResponse? Data { get; set; }

        public Response(string message, int status)
        {
            Message = message;
            Status = status;
        }

        public Response(string message, int status, PostLoginResponse? data)
        {
            Message = message;
            Status = status;
            Data = data;
        }
    }
}

