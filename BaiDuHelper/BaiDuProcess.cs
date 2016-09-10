using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Ivony.Html;
using Ivony.Html.Parser;
using Ivony.Html.Parser.Regulars;
using Ivony.Parser;
using Newtonsoft.Json;

namespace BaiDuHelper
{
    /// <summary>
    /// 百度各种操作处理
    /// </summary>
    public static class BaiDuProcess
    {
        private static HttpWebRequest CreateHttpWebRequest(string url)
        {
            return WebRequest.Create(url) as HttpWebRequest;
        }

        private static HttpWebResponse CreateHttpWebResponse(HttpWebRequest req)
        {
            return req.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 根据bduss创建CookieContainer
        /// </summary>
        /// <param name="bduss"></param>
        /// <returns></returns>
        private static CookieContainer CreateBdussCookieContainer(string bduss)
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("BDUSS", bduss, "/", "baidu.com"));
            return cookieContainer;
        }

        /// <summary>
        /// 获取所有当前账号所喜欢(关注)的贴报名
        /// </summary>
        /// <returns>贴吧名集合</returns>
        public static List<string> GetAllMyLikeTieBaName(string bduss)
        {
            List<string> tiebaNameList = new List<string>();
            //当前页
            int pageIndex = 0;
            //循环获取
            while (true)
            {
                pageIndex++;
                //获取当前页的html源码
                HttpWebRequest req = CreateHttpWebRequest("http://tieba.baidu.com/f/like/mylike?&pn=" + pageIndex);
                req.CookieContainer = CreateBdussCookieContainer(bduss);
                string myLikeTieBaHtml = new StreamReader(CreateHttpWebResponse(req).GetResponseStream(), Encoding.GetEncoding("GBK")).ReadToEnd();
                //从当前页的html源码中获取所有贴报名的a标签
                var aLinks = new JumonyParser().Parse(myLikeTieBaHtml).Find(".forum_table a[href^='/f?kw=']");
                //如果总数小于0则表示已经全部获取
                if (aLinks.Count() <= 0)
                {
                    break;
                }
                else
                {
                    aLinks.ToList().ForEach(p => tiebaNameList.Add(p.InnerText()));
                }
            }
            return tiebaNameList;
        }

        /// <summary>
        /// 获取tbs
        /// </summary>
        /// <param name="bduss"></param>
        /// <returns></returns>
        public static String GetTbs(string bduss)
        {
            HttpWebRequest req = CreateHttpWebRequest("http://tieba.baidu.com/dc/common/tbs");
            req.CookieContainer = CreateBdussCookieContainer(bduss);
            string json = new StreamReader(CreateHttpWebResponse(req).GetResponseStream()).ReadToEnd();
            TbsJson tbsJson = JsonConvert.DeserializeObject<TbsJson>(json);
            return tbsJson.is_login == 1 ? tbsJson.tbs : string.Empty;
        }


        /// <summary>
        /// 根据贴报名获取fid
        /// </summary>
        /// <param name="tiebaName"></param>
        /// <returns></returns>
        public static string GetFid(string tiebaName)
        {
            tiebaName = HttpUtility.UrlEncode(tiebaName);
            HttpWebRequest req = CreateHttpWebRequest("http://tieba.baidu.com/f/commit/share/fnameShareApi?ie=utf-8&fname=" + tiebaName);
            string json = new StreamReader(CreateHttpWebResponse(req).GetResponseStream()).ReadToEnd();
            return (JsonConvert.DeserializeObject<FidJson>(json)).data.fid.ToString();
        }

        /// <summary>
        /// 根据贴吧名签到指定贴吧
        /// </summary>
        public static SignResult TieBaSign(string tiebaName, string bduss)
        {
            //返回的结果变量,在此声明是为了在贴吧名还没有转码前为该变量的属性赋值
            SignResult signResult = new SignResult();
            signResult.TiebaName = tiebaName;

            string tbs = GetTbs(bduss);
            string fid = GetFid(tiebaName);
            tiebaName = HttpUtility.UrlEncode(tiebaName);
            HttpWebRequest req = CreateHttpWebRequest(string.Format("http://tieba.baidu.com/mo/q/sign?tbs={0}&kw={1}&is_like=1&fid={2}", tbs, tiebaName, fid));
            req.CookieContainer = CreateBdussCookieContainer(bduss);
            req.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 9_1 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13B143 Safari/601.1";
            string json = new StreamReader(CreateHttpWebResponse(req).GetResponseStream()).ReadToEnd();
            SignJson signJson = JsonConvert.DeserializeObject<SignJson>(json);

            
            if (signJson.no == 0)
            {
                signResult.Msg = signJson.data.add_sign_data.errmsg;
                signResult.IsSuccess = true;
                signResult.SignDays = signJson.data.add_sign_data.uinfo.cont_sign_num;
                signResult.SignRank = signJson.data.add_sign_data.uinfo.user_sign_rank;
            }
            else
            {
                signResult.Msg = signJson.data.msg;
                signResult.IsSuccess = false;
            }
            return signResult;
        }

        /// <summary>
        /// 根据贴吧名集合,签到所有贴吧
        /// </summary>
        public static List<SignResult> TieBaSign(List<string> tiebaNameList, string bduss)
        {
            List<SignResult> signResultList = new List<SignResult>();
            foreach (string tiebaName in tiebaNameList)
            {
               signResultList.Add(TieBaSign(tiebaName, bduss));
            }
            return signResultList;
        }

    }
}


