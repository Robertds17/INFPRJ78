using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.SpeechRecognition;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpeechRecognition {
    class Program {
        static void Main(string[] args) {

            Console.WriteLine("starting...");
            perform Perform = new perform();
            Perform.program();
        }
    }

    class perform : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public MicrophoneRecognitionClient MicClient;
        SpeechRecognitionMode Mode = SpeechRecognitionMode.LongDictation;
        string subscriptionkey = "09543b3217464199920fc826f930929c"; // << enter subscription key(personal)

        public void program() {
            CreateMicrophoneRecognitionClient();
        }

        void CreateMicrophoneRecognitionClient() {
            if (MicClient == null) {
                MicClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(
                    Mode,
                    "en-US",
                    subscriptionkey);
                MicClient.AuthenticationUri = "";

            }

            MicClient.StartMicAndRecognition();

            // results
            MicClient.OnMicrophoneStatus += OnMicrophoneStatus;

            MicClient.OnPartialResponseReceived += OnPartialResponseReceivedHandler;

            MicClient.OnResponseReceived += OnMicDictationResponseReceivedHandler;
            MicClient.OnConversationError += OnConversationErrorHandler;
        }

        public void OnMicrophoneStatus(object sender, MicrophoneEventArgs e) {
            Console.WriteLine("--- Microphone status change received by OnMicrophoneStatus() ---");
            Console.WriteLine("********* Microphone status: {0} *********", e.Recording);
            if (e.Recording) {
                Console.WriteLine("Please start speaking.");
            }
        }

        void OnPartialResponseReceivedHandler(object sender, PartialSpeechResponseEventArgs e) {
            Console.WriteLine("--- Partial result received by OnPartialResponseReceivedHandler() ---");
            Console.WriteLine("{0}", e.PartialResult);
            Console.WriteLine();
        }

        void OnMicDictationResponseReceivedHandler(object sender, SpeechResponseEventArgs e) {
            Console.WriteLine("--- OnMicDictationResponseReceivedHandler ---");
            if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.EndOfDictation ||
                e.PhraseResponse.RecognitionStatus == RecognitionStatus.DictationEndSilenceTimeout) {

                MicClient.EndMicAndRecognition();
            }
            WriteResponseResult(e);
        }

        void OnConversationErrorHandler(object sender, SpeechErrorEventArgs e) {
            Console.WriteLine("--- Error received by OnConversationErrorHandler() ---");
            Console.WriteLine("Error code: {0}", e.SpeechErrorCode.ToString());
            Console.WriteLine("Error text: {0}", e.SpeechErrorText);
        }

        void WriteResponseResult(SpeechResponseEventArgs e) {
            if (e.PhraseResponse.Results.Length == 0) {
                Console.WriteLine("No phrase response is available.");
            }
            else {
                Console.WriteLine("********* Final n-BEST Results *********");
                for (int i = 0; i < e.PhraseResponse.Results.Length; i++) {
                    Console.WriteLine(
                        "[{0}] Confidence={1}, Text=\"{2}\"",
                        i,
                        e.PhraseResponse.Results[i].Confidence,
                        e.PhraseResponse.Results[i].DisplayText);
                }

                Console.WriteLine();
            }
        }
        /// <summary>
        /// Helper function for INotifyPropertyChanged interface 
        /// </summary>
        /// <typeparam name="T">Property type</typeparam>
        /// <param name="caller">Property name</param>
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null) {
            var handler = this.PropertyChanged;
            if (handler != null) {
                handler(this, new PropertyChangedEventArgs(caller));
            }
            Console.WriteLine(caller.ToString());
        }
    }
}
