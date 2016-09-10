using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuHelper
{
    /// <summary>
    /// 签到结果
    /// </summary>
    public class SignResult
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 签到是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 连续签到天数
        /// </summary>
        public int SignDays { get; set; }

        /// <summary>
        /// 签到排行(今天第几个签到)
        /// </summary>
        public int SignRank { get; set; }
        
        /// <summary>
        /// 当前签到的贴吧名
        /// </summary>
        public string TiebaName { get; set; }
      
    }
}
