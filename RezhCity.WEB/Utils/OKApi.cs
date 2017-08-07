using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RezhCity.WEB.Utils
{
    public class OKApi
    {
        private readonly string apiUrl = "https://api.ok.ru/fb.do";
        private readonly string applicationPublicKey = "CBALGKJLEBABABABA";
        private readonly string applicationSecretKey = "BC121FC4CA63B97869650F8D";
        private readonly string accessToken = "tkn180g0EbuCU4Jp4ODHTF0jYS4kO0IdKukMFEAogD5zf1POYvlY4sZTd3dCez9V9zuoI";
        private readonly string groupId = "54582661021938";
        private readonly string methodPost = "mediatopic.post";
        private readonly string methodGetUploadUrl = "photosV2.getUploadUrl";

        public async Task Post(string name, IEnumerable<String> images, string urlToDetail)
        {
            //get url to upload images and upload
            List<String> photoTokens = new List<String>();
            foreach (var image in images)
            {
                var uploadResult = await GetUploadUrl();
                var uploadUrl = uploadResult["upload_url"].ToString();
                var photoId = uploadResult["photo_ids"][0].ToString();

                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var response = Encoding.UTF8.GetString(client.UploadFile(uploadUrl, image));
                    var json = JsonConvert.DeserializeObject(response) as JObject;
                    photoTokens.Add(json["photos"][photoId]["token"].ToString());
                }
            }

            //create data object
            var attachments = new
            {
                media = new List<Object>()
                {
                    new { type = "photo", list = photoTokens.Select(t => new { id = t }) },
                    new { type = "text", text = name },
                    new { type = "link", url = urlToDetail }
                },
                onBehalfOfGroup = true
            };

            //data object to json
            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonAttacments = jsonSerializer.Serialize(attachments);


            Dictionary<String, String> parameters = new Dictionary<String, String>()
            {
                { "application_key", this.applicationPublicKey },
                { "attachment", HttpUtility.UrlEncode(jsonAttacments.ToString()) },
                { "gid", this.groupId },
                { "method", this.methodPost },
                { "type", "GROUP_THEME" }
            };

            StringBuilder builder = new StringBuilder(this.apiUrl).Append("?");
            parameters.Add("sig", this.CalcSignature(parameters));
            parameters.Add("access_token", this.accessToken);
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                builder.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
            }
            // removing last & added with cycle
            builder.Remove(builder.Length - 1, 1);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(builder.ToString());
                var result = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject(result) as JObject;
            }
        }

        private async Task<JObject> GetUploadUrl()
        {
            Dictionary<String, String> parameters = new Dictionary<String, String>()
            {
                { "application_key", this.applicationPublicKey },
                { "gid", this.groupId },
                { "method", this.methodGetUploadUrl },
                { "type", "GROUP_THEME" }
            };

            StringBuilder builder = new StringBuilder(this.apiUrl).Append("?");
            parameters.Add("sig", this.CalcSignature(parameters));
            parameters.Add("access_token", this.accessToken);
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                builder.Append(pair.Key).Append("=").Append(pair.Value).Append("&");
            }
            // removing last & added with cycle
            builder.Remove(builder.Length - 1, 1);

            JObject result = new JObject();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(builder.ToString());
                result = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync()) as JObject;
            }

            return result;
        }

        private string CalcSignature(Dictionary<String, String> parameters)
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in parameters.OrderBy(item => item.Key))
            {
                
                builder.Append(pair.Key).Append("=").Append(pair.Key == "attachment" ? HttpUtility.UrlDecode(pair.Value) : pair.Value);
            }
            string s = GetMD5Hash(this.accessToken + this.applicationSecretKey);
            return GetMD5Hash(builder.Append(s).ToString());
        }

        private string GetMD5Hash(string input)
        {
            var x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var bs = Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            var s = new StringBuilder();
            foreach (var b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }
    }
}