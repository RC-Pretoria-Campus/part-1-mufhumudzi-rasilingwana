using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DisplayWelcomeMessage();

            Console.Write("\nPlease enter your name: ");
            string userName = Console.ReadLine()?.Trim();

            while (string.IsNullOrWhiteSpace(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid input. Please enter a valid name: ");
                Console.ResetColor();
                userName = Console.ReadLine()?.Trim();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nHello, {userName}! I'm your cybersecurity assistant. You can ask me about password safety, phishing, and safe browsing.");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\nYou: ");
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot: Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (userInput == "exit" || userInput == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nBot: Stay safe online, {0}! Goodbye!", userName);
                    Console.ResetColor();
                    break;
                }

                string response = GetResponse(userInput);
                DisplayTypingEffect(response);
            }
        }

        static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
      .----------------.
     | .--------------. |
     | |  CYBER BOT  | |
     | '--------------' |
     '------.  .------'  
       .----|  |----.  
      ||   |  |   ||  
      ||   |  |   ||  
      ||   '=='   ||  
      '------------'  
      [##########]  
     (  ◉      ◉  )  
      \   ----   /  
       '--------'  
");

            Console.WriteLine("Welcome to the Cybersecurity Awareness Chatbot!");
            Console.WriteLine("Type 'exit' or 'quit' to end the chat.");
            Console.ResetColor();
        }

        static string GetResponse(string input)
        {
            switch (input)
            {
                case "how are you?":
                case "how are you":
                    return "I'm just a bot, but I'm here to help you stay safe online!";
                case "what's your purpose?":
                case "what is your purpose":
                    return "My purpose is to educate you about online security and how to stay safe in the digital world.";
                case "what can i ask you about?":
                case "what can i ask":
                    return "You can ask me about password safety, phishing, and safe browsing.";
                case "tell me about password safety":
                case "password safety":
                    return "Use strong passwords with at least 12 characters, a mix of letters, numbers, and symbols. Never reuse passwords!";
                case "what is phishing?":
                case "phishing":
                    return "Phishing is a type of cyber attack where attackers trick you into providing sensitive information through fake emails or websites.";
                case "how to browse safely?":
                case "safe browsing":
                    return "Always use HTTPS websites, avoid clicking on suspicious links, and keep your browser up to date.";
                default:
                    return "I didn't quite understand that. Could you rephrase?";
            }
        }

        static void DisplayTypingEffect(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Bot: ");
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(30);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }
}
