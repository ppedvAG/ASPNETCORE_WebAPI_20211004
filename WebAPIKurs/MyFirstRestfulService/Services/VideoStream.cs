using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyFirstRestfulService.Services
{
    public class VideoStream : IVideoStream
    {
        private HttpClient _client;

        public VideoStream(HttpClient client)
        {
            _client = client;
        }


        public async Task<Stream> GetVideoByName(string name)
        {
            string url = string.Empty;

            switch(name)
            {
                case "fugees":
                    url = "http://gartner.gosimian.com/assets/videos/Fugees_ReadyOrNot_278-WIREDRIVE.mp4";
                    break;
                case "xyz":
                    url = "http://gartner.gosimian.com/assets/videos/George_Michael_MV-WIREDRIVE.mp4";
                    break;
                default:
                    url = "abc";
                    break;
            }

            return await _client.GetStreamAsync(url);
        }
    }
}
