using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Web;
using System.Threading;
using System.Net.Http;

namespace SpeechSample
{
    
    public class Authentication
    {
        public static readonly string FetchTokenUri = "https://api.cognitive.microsoft.com/sts/v1.0";
        private string subscriptionKey;
        private string token;
        private Timer accessTokenRenewer;

        private const int RefreshTokenDuration = 9;

        public Authentication(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
            this.token = FetchToken(FetchTokenUri, subscriptionKey).Result;

            
            accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback),
                                           this,
                                           TimeSpan.FromMinutes(RefreshTokenDuration),
                                           TimeSpan.FromMilliseconds(-1));
        }

        public string GetAccessToken()
        {
            return this.token;
        }

        private void RenewAccessToken()
        {
            this.token = FetchToken(FetchTokenUri, this.subscriptionKey).Result;
            Console.WriteLine("Renewed token.");
        }

        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
            }
            finally
            {
                try
                {
                    accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
                }
            }
        }

        private async Task<string> FetchToken(string fetchUri, string subscriptionKey)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);
                uriBuilder.Path += "/issueToken";

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
    public class APIcalls{
        private string _Reckey;
        private string _Bingkey;
        public APIcalls(string Reckey, string BingKey){
            _Reckey = Reckey;
            _Bingkey = BingKey;
        }
        
        public string Bing(string file){
            string[] args = new string[] {"https://speech.platform.bing.com/speech/recognition/conversation/cognitiveservices/v1?language=en-US&format=detailed",file };            
            if ((args.Length < 2) || (string.IsNullOrWhiteSpace(args[0])))
            {
                Console.WriteLine("Arg[0]: Specify the endpoint to hit https://speech.platform.bing.com/recognize");
                Console.WriteLine("Arg[1]: Specify a valid input wav file.");
                
            }

            Authentication auth = new Authentication(_Bingkey);

            string requestUri = args[0];

            string host = @"speech.platform.bing.com";
            string contentType = @"application/octet-stream";

            string audioFile = args[1];
            string responseString;
            FileStream fs = null;

            try
            {
                var token = auth.GetAccessToken();              

                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
                request.SendChunked = true;
                request.Accept = @"application/json;text/xml";
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Host = host;
                request.ContentType = contentType;
                request.Headers["Authorization"] = "Bearer " + token;

                using (fs = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
                {

                    /*
                     * Open a request stream and write 1024 byte chunks in the stream one at a time.
                     */
                    byte[] buffer = null;
                    int bytesRead = 0;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        /*
                         * Read 1024 raw bytes from the input audio file.
                         */
                        buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        // Flush
                        requestStream.Flush();
                    }

                    
                     //Get the response from the service.
                     
                    Console.WriteLine("Response:");
                    using (WebResponse response = request.GetResponse())
                    {
                        Console.WriteLine(((HttpWebResponse)response).StatusCode);

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = sr.ReadToEnd();
                        }                        
                        return responseString;
                    }
                }
            }
            catch (WebException ex)
            {                           
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                return ex.Message;

            }
        }

        public string Createid(){
            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles?";

            string responseString;

            try{ 

                HttpWebRequest request = null;  
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";

                request.Method ="POST";

                request.ProtocolVersion = HttpVersion.Version11;

                request.Headers["Ocp-Apim-Subscription-Key"] =_Reckey;

                request.ContentType ="application/json";


                using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"locale\":\"en-us\",}";
                    

                    requestStream.Write(json);
                    requestStream.Flush();
                    requestStream.Close();

                }


                using (WebResponse response = request.GetResponse()){
                    Console.WriteLine(((HttpWebResponse)response).StatusCode);

                    using (StreamReader sr = new StreamReader(response.GetResponseStream())){
                        responseString = sr.ReadToEnd();
                    }                    
                    return responseString;
                    
                }
            }
            catch (WebException ex)
            {                           
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                return ex.Message;

            }

        }
        public string CreateVoiceWithIdentification(string identificationProfileId, string file){                        

            string requestUri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles/"+ identificationProfileId+"/enroll?shortAudio=true";           
            string contentType = "application/octet-stream";
           
            string audioFile = file;
            string responseString;
            FileStream fs = null;

            try
            {                
                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
                request.SendChunked = true;
                request.Accept = @"application/json;text/xml";
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;                
                request.ContentType = contentType;
                request.Headers["Ocp-Apim-Subscription-Key"] = _Reckey;

                using (fs = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
                {

                    /*
                     * Open a request stream and write 1024 byte chunks in the stream one at a time.
                     */
                    byte[] buffer = null;
                    int bytesRead = 0;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        /*
                         * Read 1024 raw bytes from the input audio file.
                         */
                        buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        // Flush
                        requestStream.Flush();
                    }

                    Console.WriteLine("Response:");
                    using (WebResponse response = request.GetResponse())
                    {
                        Console.WriteLine(((HttpWebResponse)response).StatusCode);

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = sr.ReadToEnd();
                        }                        
                        return responseString;
                    }
                }
            }
            catch (WebException ex)
            {                           
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                return ex.Message;

            }

        } 
        public string GetAllProfiles(){
            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";

            string responseString;

            try{ 

                HttpWebRequest request = null;  
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";
                request.Method ="GET";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Headers["Ocp-Apim-Subscription-Key"] =_Reckey;                             


                using (WebResponse response = request.GetResponse()){
                    Console.WriteLine(((HttpWebResponse)response).StatusCode);

                    using (StreamReader sr = new StreamReader(response.GetResponseStream())){
                        responseString = sr.ReadToEnd();
                    }                    
                    return responseString;
                }
            }
            catch (WebException ex)
            {                           
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                return ex.Message;

            }

        }
        public string FindSpeakers(string Ids, string file){
            string requestUri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identify?identificationProfileIds="+ Ids+"&shortAudio=true";           
            string contentType = "application/octet-stream";
           
            string audioFile = file;
            string responseString;
            FileStream fs = null;

            try
            {                
                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
                request.SendChunked = true;
                request.Accept = @"application/json;text/xml";
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;                
                request.ContentType = contentType;
                request.Headers["Ocp-Apim-Subscription-Key"] = _Reckey;

                using (fs = new FileStream(audioFile, FileMode.Open, FileAccess.Read))
                {

                    /*
                     * Open a request stream and write 1024 byte chunks in the stream one at a time.
                     */
                    byte[] buffer = null;
                    int bytesRead = 0;
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        /*
                         * Read 1024 raw bytes from the input audio file.
                         */
                        buffer = new Byte[checked((uint)Math.Min(1024, (int)fs.Length))];
                        while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        // Flush
                        requestStream.Flush();
                    }

                    Console.WriteLine("Response:");
                    using (WebResponse response = request.GetResponse())
                    {                        
                        Console.WriteLine(((HttpWebResponse)response).StatusCode);

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            responseString = sr.ReadToEnd();
                        }                        
                        return responseString;
                    }
                }
            }
            catch (WebException ex)
            {                           
                using (var stream = ex.Response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
                return ex.Message;

            }
        }

    }

    class Program
    {
        
        static void Main(string[] args )
        {
            APIcalls Calls = new APIcalls("Speakerkey","Bingkey");
            Console.WriteLine(Calls.Bing("byebye.wav"));
            //Calls.Createid();
            //Console.WriteLine(Calls.CreateVoiceWithIdentification("f81024d2-33c8-4f39-87b4-4af93f2d109b","my_voice.wav"));
            //Console.WriteLine(Calls.FindSpeakers("f81024d2-33c8-4f39-87b4-4af93f2d109b,65bf55bf-1dc4-4545-966c-7fb5d750cd00","byebye.wav"));
            //Console.WriteLine(Calls.GetAllProfiles());

        }
    }
}
