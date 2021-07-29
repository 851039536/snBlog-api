using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.Extensions.Logging;

namespace Snblog.Service.AngleSharp
{
    public class HotNewsAngleSharp
    {
        private readonly ILogger<HotNewsAngleSharp> _logger;
        public HotNewsAngleSharp(ILogger<HotNewsAngleSharp> logger)
        {
            _logger = logger;
        }

        public async Task<List<string>> Cnblogs(string url, string selector, string selectorall)
        {
            if (url == null && selector == null && selectorall == null)
            {
                url = "https://www.cnblogs.com/";
                selector = "#post_list";
                selectorall = "div.post-item-text > a";
                _logger.LogInformation("Cnblogs 默认赋值");

            }
            List<string> resultData = await reptile(url, selector, selectorall);
            return resultData;
        }

        /// <summary>
        /// 自定义爬取网站内容
        /// </summary>
        /// <param name="url">网站</param>
        /// <param name="selector">信息头</param>
        /// <param name="selectorall">信息具体内容</param>
        /// <returns></returns>
        public async Task<List<string>> GeneralCrawl(string url, string selector, string selectorall)
        {
             List<string> resultData =new List<string>();
            if (url == null && selector == null && selectorall == null)
            {
               resultData.Add("内容不能为空");
            }
            else
            {
                  resultData = await reptile(url, selector, selectorall);
            }
            return resultData;
        }

        private async Task<List<string>> reptile(string url, string selector, string selectorall)
        {
            int num = 1;
            List<string> resultData = new List<string>();
            // 设置配置以支持文档加载
            var config = Configuration.Default.WithDefaultLoader();
            // 地址
            var urls = url;
            // 请求
            var document = await BrowsingContext.New(config).OpenAsync(urls);
            if (selector != null)
            {
                // 根据css选择器获取html元素
                var container = document.QuerySelector(selector);
                var matches = container.QuerySelectorAll(selectorall);

                foreach (var item in matches)
                {
                   
                    if (num<=10)
                    {
                        num++;
                        resultData.Add(item.InnerHtml + "-" + item.GetAttribute("href"));
                    }
                    
                }
            }
            else
            {
                var matches = document.QuerySelectorAll(selectorall);
                foreach (var item in matches)
                {
                    
                    if (num <= 10)
                    {
                        num++;
                        _logger.LogInformation(item.InnerHtml + "," + item.GetAttribute("href"));
                        resultData.Add(item.InnerHtml + "," + item.GetAttribute("href"));
                    }
            
                }
            }

            return resultData;
        }

        public async Task<List<string>> GiteeItem()
        {
            List<string> resultData = new List<string>();
            // 设置配置以支持文档加载
            var config = Configuration.Default.WithDefaultLoader();
            // 地址
            var address = "https://gitee.com/kaiouyang-sn";
            // 请求
            var document = await BrowsingContext.New(config).OpenAsync(address);
            // 根据css选择器获取html元素
            var container = document.QuerySelector("#popular-pinned-projects");
            var matches = container.QuerySelectorAll("div.header > a");
            // We are only interested in the text - select it with LINQ
            foreach (var item in matches)
            {
                _logger.LogInformation(item.InnerHtml + "," + item.GetAttribute("href"));
                resultData.Add(item.InnerHtml + "," + item.GetAttribute("href"));
            }
            return resultData;
        }
    }
}
