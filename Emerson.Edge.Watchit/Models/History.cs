using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emerson.Edge.Watchit.Models
{
    public class History
    {
        public string entityId { get; set; }
        public HistoryProperty? Properties { get; set; }
        public FieldHistory FieldHistory { get; set; }
    }
}
