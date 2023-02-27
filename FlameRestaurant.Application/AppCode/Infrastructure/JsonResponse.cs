using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlameRestaurant.Application.AppCode.Infrastructure
{
    public class JsonResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public object Value { get; set; }
    }
}
