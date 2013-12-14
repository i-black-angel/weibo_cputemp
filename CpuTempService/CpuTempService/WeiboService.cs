using System;
using System.Collections.Generic;
using System.Text;

namespace CpuTempService
{
    class WeiboService
    {
        private readonly string weibo_update_api = "https://api.weibo.com/2/statuses/update.json";
        private readonly string access_token = "2.00w5QeSEbnvPgEb9740119e1yX96MD";
        private const long clientID = 4288934543;

        public bool Statuses_Update(string status)
        {
            status = status.Length > 140 ? status.Substring(0, 140) : status;
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("?source={0}&access_token={1}&status={2}", 
                clientID, access_token, status);
            HttpMethod httpMethod = new HttpMethod();
            httpMethod.HttpPost(weibo_update_api, builder.ToString());
            return true;
        }
    }
}
