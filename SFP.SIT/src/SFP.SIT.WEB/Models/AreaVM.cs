using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFP.SIT.WEB.Models
{
    public class AreaVM
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<AreaVM> children { get; set; }
    }
}
