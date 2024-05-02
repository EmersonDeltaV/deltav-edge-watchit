using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.Edge.Watchit.Models
{
    public class FieldHistory
    {
        public string FieldName { get; set; }
        public List<FieldValue>? FieldValue { get; set; }
    }
}
