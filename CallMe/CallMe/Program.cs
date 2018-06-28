using System;
using System.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace CallMe.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ask for phone number and name
            Console.WriteLine("Enter a phone number:");
            var toNumber = new PhoneNumber($"+1{Console.ReadLine()}");
            Console.WriteLine("Enter a name:");
            var name = Console.ReadLine();
            var fromNumber = new PhoneNumber(ConfigurationManager.AppSettings["TwilioFromNumber"]);

            //set the client up
            InitTwilioClient();

            //send a text
            SendSms(toNumber, fromNumber, name);

            //take a nap... zzzz
            System.Threading.Thread.Sleep(10000);

            //make a call
            MakeCall(toNumber, fromNumber);
        }

        private static void InitTwilioClient()
        {
            var twilioSid = ConfigurationManager.AppSettings["TwilioSid"];
            var twilioToken = ConfigurationManager.AppSettings["TwilioToken"];
            TwilioClient.Init(twilioSid, twilioToken);
        }

        private static void SendSms(PhoneNumber toNumber, PhoneNumber fromNumber, string name)
        {
            MessageResource.Create(toNumber, from: fromNumber, body: $"Hello {name}!");
        }

        private static void MakeCall(PhoneNumber toNumber, PhoneNumber fromNumber)
        {
            var url = new Uri("https://s3.amazonaws.com/harper-public/welcome.xml");
            CallResource.Create(toNumber, fromNumber, url: url, method: "GET");
        }
    }
}
