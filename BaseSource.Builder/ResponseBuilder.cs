using System;
using Basesource.Constants;
using BaseSource.Dto;

namespace BaseSource.Builder
{
    public class ResponseBuilder<T>
    {
        private Response<T> response = new Response<T>();

        /// <summary>
        /// The <>ResponseBuilder</c> required <typeparamref name="T"/> to return data.
        /// </summary>
        public ResponseBuilder(T data)
        {
            response.Data = data;
        }

        /// <summary>
        /// The <>ResponseBuilder</c> doesn't required <typeparamref name="T"/> and data will return <code>null</code>.
        /// </summary>
        public ResponseBuilder()
        {
            response.Data = default(T);
        }

        public ResponseBuilder<T> WithStatusCode(UInt16 code)
        {
            response.StatusCode = code;
            return this;
        }

        public ResponseBuilder<T> WithMessage(string message)
        {
            response.Message = message;
            return this;
        }

        public ResponseBuilder<T> WithError()
        {
            response.Error = true;
            return this;
        }

        /// <summary>
        /// The <c>With201</c> method returns a response with a status code of 201 after successfully creating data.
        /// </summary>
        public ResponseBuilder<T> With201()
        {
            response.Message = ResponseConstant.Message201;
            response.StatusCode = 201;
            response.Error = false;
            return this;
        }

        /// <summary>
        /// The <c>With200</c> method returns a response with a status code of 200, indicating that the request has succeeded.
        /// </summary>
        public ResponseBuilder<T> With200()
        {
            response.Message = ResponseConstant.Message200;
            response.StatusCode = 200;
            response.Error = false;
            return this;
        }

        /// <summary>
        /// The <c>Build</c> building all properties and return <typeparamref name="T"/>.
        /// </summary>
        public Response<T> Build()
        {
            return response;
        }
    }
}
