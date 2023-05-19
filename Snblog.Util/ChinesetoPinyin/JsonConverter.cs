// using Microsoft.International.Converters.PinYinConverter; // 引用中文转拼音库
// using System.Text.Json;
// using System.Text.Json.Serialization;
//
// namespace Snblog.Util.ChinesetoPinyin {
//     public class PinYinConverter : JsonConverter<string> {
//         public override string Read(ref Utf8JsonReader reader,Type typeToConvert,JsonSerializerOptions options) {
//             return reader.GetString();
//         }
//
//         public override void Write(Utf8JsonWriter writer,string value,JsonSerializerOptions options) {
//             string output = "";
//             foreach (char c in value) {
//                 if (ChineseChar.IsValidChar(c)) {
//                     ChineseChar chineseChar = new(c); // 创建中文字符对象
//                     output += string.Concat(chineseChar.Pinyins[0].AsSpan(0,chineseChar.Pinyins[0].Length - 1)," ");
//                 } else {
//                     output += c + " ";
//                 }
//             }
//             writer.WriteStringValue(output.TrimEnd());
//         }
//     }
// }