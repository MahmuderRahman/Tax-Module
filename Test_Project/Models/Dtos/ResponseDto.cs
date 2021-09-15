using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Project.Models.Dtos
{
    public class ResponseDto
    {
        public ResponseDto()
        {
            Succeeded = false;
        }
        public bool Succeeded { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}