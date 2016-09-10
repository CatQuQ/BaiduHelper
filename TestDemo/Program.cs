using System;
using System.Collections.Generic;
using BaiDuHelper;


namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string bduss =
                "Q2Z3NNUTZNWDcyRHdsaH5RYzNwaVZwVG5vcDNlenJIaDZmc0tMeGxZeWpTUHRYQVFBQUFBJCQAAAAAAAAAAAEAAACh4JGhwtzUqAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAKO701eju9NXNm";
       
            List<string> tbName = BaiDuProcess.GetAllMyLikeTieBaName(bduss);

            List<SignResult> signResultList = BaiDuProcess.TieBaSign(tbName,bduss);

            signResultList.ForEach(p => Console.WriteLine(p.IsSuccess+" "+p.Msg+" "+p.SignDays+" "+p.SignRank));
                       
            Console.ReadKey();
        }
    }
}
