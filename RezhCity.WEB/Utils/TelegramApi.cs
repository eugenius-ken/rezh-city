using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RezhCity.WEB.Utils
{
    public class TelegramApi
    {
        private static readonly string Token = "374098133:AAH1ZEsjtdU_Ca8TNiOdxuCphUY2Pu8GN7g";
        private static readonly string apiUrl = "https://api.telegram.org/bot" + Token + "/";
        private static readonly int ChatId = 82437443;

        public async Task SendNotification(string text)
        {
            var url = apiUrl + "sendMessage?chat_id=" + ChatId + "&text=" + text + "&parse_mode=Markdown";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
            }
        }
    }
}