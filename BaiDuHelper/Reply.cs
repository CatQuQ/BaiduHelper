using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuHelper
{
    class ReplyJson
    {
        public int no { get; set; }
        public string error { get; set; }
        public Data data { get; set; }
    }
    class Data
    {
        public long tid { get; set; }
        public long pid { get; set; }
    }
}


