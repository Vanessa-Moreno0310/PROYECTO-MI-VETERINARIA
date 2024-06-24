using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiVeterinaria.Models
{
    public class ResponseDb
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}