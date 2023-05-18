using Snblog.Util.ChinesetoPinyin;
using System.Text.Json;

namespace Snblog.Controllers
{
    /// <summary>
    /// 转换
    /// </summary>
   //[Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "V1")] //版本控制
    [ApiController]
    [Route("transition")]
    public class TransitionController : ControllerBase {

        /// <summary>
        /// 拼音
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet("pinyin")]
        public IActionResult ChinesetoPinyin(string value) {
            JsonSerializerOptions options = new();
            options.Converters.Add(new PinYinConverter());
            string output = JsonSerializer.Serialize(value,options);
            Console.WriteLine(output); //输出： "hàn zì zhuǎn pīn yīn"
            return Ok(output);
        }
        /// <summary>
        /// ToBaseString
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        [HttpGet("base64")]
        public IActionResult ToBaseString(string imagePath = "C:\\Users\\ch190006\\Desktop\\123.jpg") {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            Console.WriteLine(base64String);
            return Ok(base64String);
        }
    }
}
