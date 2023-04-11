using Microsoft.AspNetCore.Mvc;
using Snblog.Util.ChinesetoPinyin;
using System;
using System.IO;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

//默认的约定集将应用于程序集中的所有操作：
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Snblog.Controllers {
    /// <summary>
    /// 转换
    /// </summary>
   //[Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("transition")]
    public class TransitionController : ControllerBase {

        [HttpGet("pinyin")]
        public IActionResult ChinesetoPinyin(string value) {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.Converters.Add(new PinYinConverter());
            string output = JsonSerializer.Serialize(value,options);
            Console.WriteLine(output); //输出： "hàn zì zhuǎn pīn yīn"
            return Ok(output);
        }
        [HttpGet("base64")]
        public IActionResult ToBaseString(string imagePath = "C:\\Users\\ch190006\\Desktop\\123.jpg") {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            Console.WriteLine(base64String);
            return Ok(base64String);
        }

        //[HttpGet("baseimg")]
        //public IActionResult ToBaseImg(string value) {
        //    string base64String = "base64-encoded-string"; // 请将此处替换为实际的 Base64 编码字符串
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    using (var ms = new MemoryStream(imageBytes)) {
        //        var image = System.Drawing.Image.FromStream(ms);
        //        image.Save("image.png",System.Drawing.Imaging.ImageFormat.Png); // 将图像保存为 PNG 格式的文件
        //    }
        //    return Ok(base64String);
        //}
    }
}
