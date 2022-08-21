using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ResponseDto
    {
        public bool IsExitoso { get; set; } = true;
        public object Result { get; set; }
        public string Mensaje { get; set; }
        public List<string> Error { get; set; }
    }
}