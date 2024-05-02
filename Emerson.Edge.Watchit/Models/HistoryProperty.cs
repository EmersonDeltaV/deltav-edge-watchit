using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.Edge.Watchit.Models
{
    public class HistoryProperty
    {
        public AREA_NAME AREA_NAME { get; set; }
        public MODULE_NAME MODULE_NAME { get; set; }
    }

    public class AREA_NAME
    {
        public string value { get; set; }
    }

    public class MODULE_NAME
    {
        public string value { get; set; }
    }
}
