using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RezhCity.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RezhCity.WEB.Utils
{
    public class VKApi
    {
        private const string apiUrl = "https://api.vk.com/method/";
        private const string groupId = "143199984";
        private const string accessToken = "000ce478b21084cb64f85412aa7ca79d0b9ea6c3f9affc236497047f1cb4fb63407553f15c393bcc765cc";

        public async Task Post(string text, IEnumerable<String> images, string urlToDetail)
        {
            //get server url to upload photo
            var queryGetServer = $"{apiUrl}photos.getWallUploadServer?group_id={groupId}&access_token={accessToken}";
            JObject photoServerResponse;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(queryGetServer);
                var result = await response.Content.ReadAsStringAsync();
                photoServerResponse = JsonConvert.DeserializeObject(result) as JObject;
            }

            //send photos to server and get photo id to post on the wall 
            string photos = String.Empty;
            foreach (var image in images)
            {
                JObject photoResponse;
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var response = Encoding.UTF8.GetString(client.UploadFile(photoServerResponse["response"]["upload_url"].ToString(), image));
                    photoResponse = JsonConvert.DeserializeObject(response) as JObject;
                }

                var querySavePhoto = $"{apiUrl}photos.saveWallPhoto?access_token={accessToken}&server={photoResponse["server"]}&hash={photoResponse["hash"]}&group_id={groupId}&photo={photoResponse["photo"]}";
                JObject photoObject = new JObject();
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(querySavePhoto);
                    var result = await response.Content.ReadAsStringAsync();
                    photoObject = JsonConvert.DeserializeObject(result) as JObject;
                }

                photos += photoObject["response"][0]["id"] + ",";
            }
            photos += urlToDetail;

            //post advertisement to wall
            var queryPost = $"{apiUrl}wall.post?from_group=1&access_token={accessToken}&owner_id=-{groupId}&attachments={photos}&message={text}";
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(queryPost);
                var result = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
