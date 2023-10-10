using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseSource.Dto
{
    public class ResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Error { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
