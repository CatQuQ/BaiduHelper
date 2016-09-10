using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiDuHelper
{
    class SignJson
    {
        public int no { get; set; }
        public string error { get; set; }
        public SingData data { get; set; }
    }

    class SingData
    {
        public string msg { get; set; }
        public Add_Sign_Data add_sign_data { get; set; }
        public Forum_Sign_Info_Data forum_sign_info_data { get; set; }
    }

    class Add_Sign_Data
    {
        public int errno { get; set; }
        public string errmsg { get; set; }
        public int sign_version { get; set; }
        public int is_block { get; set; }
        public Finfo finfo { get; set; }
        public Uinfo uinfo { get; set; }
    }

    class Finfo
    {
        public Forum_Info forum_info { get; set; }
        public Current_Rank_Info current_rank_info { get; set; }
    }

    class Forum_Info
    {
        public int forum_id { get; set; }
        public string forum_name { get; set; }
    }

    class Current_Rank_Info
    {
        public int sign_count { get; set; }
    }

    class Uinfo
    {
        public long user_id { get; set; }
        public int is_sign_in { get; set; }
        public int user_sign_rank { get; set; }
        public int sign_time { get; set; }
        public int cont_sign_num { get; set; }
        public int total_sign_num { get; set; }
        public int cout_total_sing_num { get; set; }
        public int hun_sign_num { get; set; }
        public int total_resign_num { get; set; }
        public int is_org_name { get; set; }
    }

    class Forum_Sign_Info_Data
    {
        public bool is_on { get; set; }
        public bool is_filter { get; set; }
        public int sign_count { get; set; }
        public int sign_rank { get; set; }
        public int member_count { get; set; }
        public int generate_time { get; set; }
        public string dir_rate { get; set; }
        public int sign_day_count { get; set; }
    }

}
