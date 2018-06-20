using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APICalls
{
    public class APICalls
    {
        
        private string _BingKey;
        private string _RecKey;

        public APICalls()
        {
            _BingKey = "";
            _RecKey = "";
        }

        public string Bing(string file)
        {
            Authentication.Authentication auth = new Authentication.Authentication(_BingKey);

            string requestUri = "https://speech.platform.bing.com/speech/recognition/conversation/cognitiveservices/v1?language=en-US&format=detailed";

            string host = @"speech.platform.bing.com";
            string contentType = @"application/octet-stream";

            string audioFile = file;
            string responseString;
            FileStream fs = null;

            try
            {
                var token = auth.GetAccessToken();
                Console.WriteLine("try");
                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
                request.SendChunked = true;
                request.Accept = @"application/json;text/xml";
                request.Method = "POST";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Host = host;
                request.ContentType = contentType;
                request.Headers["Authorization"] = "Bearer " + token;
                Console.WriteLine("Filestream");
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

        public string CreateProfile()
        {
            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles?";

            string responseString;

            try
            {

                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";

                request.Method = "POST";

                request.ProtocolVersion = HttpVersion.Version11;

                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;

                request.ContentType = "application/json";


                using (StreamWriter requestStream = new StreamWriter(request.GetRequestStream()))
                {
                    string json = "{\"locale\":\"en-us\",}";


                    requestStream.Write(json);
                    requestStream.Flush();
                    requestStream.Close();

                }


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

        public string GetAllProfiles()
        {
            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles";

            string responseString;

            try
            {

                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";
                request.Method = "GET";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;


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

        public string EnrollProfile(string identificationProfileId, string file)
        {

            string requestUri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles/" + identificationProfileId + "/enroll?shortAudio=true";
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
                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;

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
        
        public string DeleteProfile(string identificationProfileId) 
        {
            var uri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identificationProfiles/" + identificationProfileId;

            string responseString;

            try
            {

                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";
                request.Method = "DELETE";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;


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

        public string FindSpeakers(string Ids, string file)
        {
            string requestUri = "https://westus.api.cognitive.microsoft.com/spid/v1.0/identify?identificationProfileIds=" + Ids + "&shortAudio=true";
            
            string contentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";

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
                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;

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
                        return response.Headers["Operation-Location"];
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

        public string GetOperationStatus(string URI)
        {
            var uri = URI;

            string responseString;

            try
            {

                HttpWebRequest request = null;
                request = (HttpWebRequest)HttpWebRequest.Create(uri);

                request.Accept = @"application/json;text/xml";
                request.Method = "GET";
                request.ProtocolVersion = HttpVersion.Version11;
                request.Headers["Ocp-Apim-Subscription-Key"] = _RecKey;


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

        public string ReturnSpeaker(string Ids,string file)
        {
            string jsonrepsonse = FindSpeakers(Ids, file);
            Console.WriteLine(jsonrepsonse[0]);
            if (jsonrepsonse[0].ToString() != "h")
            {
                return "error";
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
                string jsonresponsestatus = GetOperationStatus(jsonrepsonse);

                dynamic json = JsonConvert.DeserializeObject(jsonresponsestatus);

                while (json.status == "running" || json.status == "notstarted")
                {


                    jsonresponsestatus = GetOperationStatus(jsonrepsonse);
                    json = JsonConvert.DeserializeObject(jsonresponsestatus);

                }
                if (json.status == "failed")
                {
                    return "error while getting status";
                }
                else
                {
                    json = JsonConvert.DeserializeObject(jsonresponsestatus);

                    if (json.processingResult.identifiedProfileId == "00000000-0000-0000-0000-000000000000")
                    {
                        return "Could not recognize speaker";
                    }
                    else
                    {
                        string speaker = json.processingResult.identifiedProfileId;

                        return speaker;
                    }

                    //"00000000-0000-0000-0000-000000000000";
                    //json.processingResult.identifiedProfileId
                }



            }
        }
    }

}
    


