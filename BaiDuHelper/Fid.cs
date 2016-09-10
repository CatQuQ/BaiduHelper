using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuHelper
{
    class FidJson
    {
        public int no { get; set; }
        public string error { get; set; }
        public FidData data { get; set; }
    }

    class FidData
    {
        public int fid { get; set; }
        public int can_send_pics { get; set; }
    }

}
