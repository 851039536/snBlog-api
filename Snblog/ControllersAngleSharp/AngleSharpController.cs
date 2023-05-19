using Snblog.Service.AngleSharp;
using Snblog.Util.GlobalVar;

namespace Snblog.ControllersAngleSharp
{
    [ApiExplorerSettings(GroupName = "AngleSharp")] 
    [ApiController]
    [Route("angleSharp")]
    public class AngleSharpController : ControllerBase
    {
        private readonly HotNewsAngleSharp _angle; //IOC依赖注入
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="angle"></param>
        public AngleSharpController(HotNewsAngleSharp angle)
        {
            _angle = angle;
        }
        #endregion
        /// <summary>
        /// 自定义爬取内容
        /// </summary>
        /// <param name="url">网站：https://www.cnblogs.com/</param>
        /// <param name="selector">selector：#post_list</param>
        /// <param name="selectorall">selectorall：div.post-item-text > a</param>
        /// <returns></returns>
        [HttpGet("GeneralCrawl")]
        public async Task<IActionResult> GeneralCrawl(string url,string selector,string selectorall)
        {
            return Ok(await _angle.GeneralCrawl(url,selector,selectorall));
        }

        /// <summary>
        /// 读取博客园最新内容（如选项为空读取默认值）此项为参考示例
        /// </summary>
        /// <param name="url">博客网站：https://www.cnblogs.com/</param>
        /// <param name="selector">selector：#post_list</param>
        /// <param name="selectorall">selectorall：div.post-item-text > a</param>
        /// <returns></returns>
        [HttpGet("GetCnblogs")]
        public async Task<IActionResult>GetCnblogs(string url, string selector, string selectorall)
        {
            return Ok(await _angle.GetCnblogs(url, selector, selectorall));
        }
        /// <summary>
        /// 读取项目名称
        /// </summary>
        /// <returns></returns>
        [HttpGet("GiteeItem")]
        public async Task<IActionResult> GiteeItem()
        {
            return Ok(await _angle.GiteeItem());
        }
        [HttpGet("Daka")]
        public async Task<IActionResult> Daka()
        {
            return Ok(await HotNewsAngleSharp.Daka());
        }
        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="path">备份路径默认null</param>
        /// <returns></returns>
        [HttpPost("SqlBackups")]
        public ActionResult SqlBackups(string path ="null")
        {
            return Ok(HotNewsAngleSharp.SqlBackups(path));
        }
        /// <summary>
        /// 还原数据
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pwd"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        [HttpPost("SqlRestore")]
        public ActionResult SqlRestore(string ip = "localhost", string user = "root", string pwd = "woshishui", string database = "snblog")
        {
            return Ok(HotNewsAngleSharp.SqlRestore(ip, user, pwd, database));
        }

        /// <summary>
        /// 测试TOKEN是否存在
        /// </summary>
        /// <returns></returns>
        [HttpGet("TOKEN")]
        [Authorize(Roles = Permissions.Name)]
        public ActionResult TOKEN()
        {
            return Ok();
        }


        [HttpPost("FilePaths")]
        public ActionResult FilePaths(string filePathByForeach = "D:\\TE-Test")
        {
            var jx = new List<string>();
            var cx = new List<string>();
            string url = default;
            try
            {
                DirectoryInfo theFolder = new DirectoryInfo(filePathByForeach);
                DirectoryInfo[] dirInfo = theFolder.GetDirectories();//获取所在目录的文件夹

                //遍历文件夹
                foreach (DirectoryInfo NextFolder in dirInfo)
                {
                    if (!NextFolder.FullName.Contains("$RECYCLE"))
                    {
                        var result = $"{filePathByForeach}\\{NextFolder.Name}";
                        theFolder = new DirectoryInfo(result);
                        GetFiles(theFolder, "*.exe*", ref cx, false);
                        foreach (var item in cx)
                        {
                            url = item.ToString();
                        }
                        jx.Add(NextFolder.Name.ToString() + "," + $"{filePathByForeach}\\{NextFolder.Name}\\{url}");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(jx);
        }

 
        [HttpPost("StartPath")]
        public ActionResult StartPath(string path)
        {
            //TFile.TOpenFile(@"D:\sw\netCore\Snblog\Snblog\bin\Debug\net5.0\MECH",1);
            return Ok();
        }

        /// <summary>
        /// 查找指定文件夹下指定后缀名的文件
        /// </summary>
        /// <param name="directory">文件路径</param>
        /// <param name="pattern">匹配值</param>
        /// <param name="fileList">返回值</param>
        /// <param name="sign">是否返回当前目录的子目录</param>
        public static void GetFiles(DirectoryInfo directory, string pattern, ref List<string> fileList, bool sign)
        {
            if (directory.Exists || pattern.Trim() != string.Empty)
            {
                try
                {
                    foreach (DirectoryInfo info in directory.GetDirectories())
                    {
                        if (!info.FullName.Contains("$RECYCLE"))
                        {
                            fileList.Add(info.Name.ToString());
                        }
                    }
                    foreach (FileInfo info in directory.GetFiles(pattern))
                    {
                        fileList.Add(info.Name.ToString());
                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                if (sign)
                {
                    foreach (DirectoryInfo info in directory.GetDirectories())//获取文件夹下的子文件夹
                    {
                        GetFiles(info, pattern, ref fileList, sign);//递归调用该函数，获取子文件夹下的文件
                    }
                }

            }
        }
    }



}
