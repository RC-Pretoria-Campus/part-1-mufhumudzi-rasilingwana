using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NAudio.Wave;

namespace CyberBotEnhanced
{
    internal class Program
    {
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use strong passwords with a mix of letters, numbers, and symbols.",
                "Avoid using the same password across multiple sites.",
                "Don't include personal details like your name or birthdate in passwords."
            }},
            { "scam", new List<string> {
                "Scammers often pose as trusted companies. Always verify email addresses.",
                "Avoid clicking on links from unknown senders.",
                "Report suspicious emails to your email provider or company."
            }},
            { "privacy", new List<string> {
                "Limit what personal information you share online.",
                "Use privacy settings on social media to restrict visibility.",
                "Be cautious when granting app permissions."
            }},
            { "phishing", new List<string> {
                "Be cautious of emails asking for personal information.",
                "Don’t click on suspicious links or download attachments from unknown sources.",
                "Verify the sender’s email address and look for typos."
            }},
        };

        static Dictionary<string, string> memory = new Dictionary<string, string>();

        static void Main()
        {
            string soundFile = @"C:\Users\mufhu\source\repos\poe\poe\bin\Debug\Recording.wav";

            try
            {
                using (var audioFile = new AudioFileReader(soundFile))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    Thread.Sleep(3000);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error playing sound: " + ex.Message);
                Console.ResetColor();
            }

            Console.OutputEncoding = Encoding.UTF8;
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

            memory["name"] = userName;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nHello, {userName}! I'm your cybersecurity assistant. Ask me anything about password safety, phishing, scams, and privacy.");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\nYou: ");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot: Please enter a valid question.");
                    Console.ResetColor();
                    continue;
                }

                if (input == "exit" || input == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nBot: Stay safe online, {memory["name"]}! Goodbye!");
                    Console.ResetColor();
                    break;
                }

                // Sentiment detection
                string sentimentResponse = DetectSentiment(input);
                if (!string.IsNullOrEmpty(sentimentResponse))
                {
                    DisplayTypingEffect(sentimentResponse);
                    continue;
                }

                // Memory & interest
                if (input.Contains("interested in"))
                {
                    string topic = input.Split("interested in").Last().Trim();
                    memory["interest"] = topic;
                    DisplayTypingEffect($"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.");
                    continue;
                }

                if (memory.ContainsKey("interest") && input.Contains("tip"))
                {
                    string interest = memory["interest"];
                    if (keywordResponses.ContainsKey(interest))
                    {
                        string randomTip = GetRandomResponse(keywordResponses[interest]);
                        DisplayTypingEffect($"As someone interested in {interest}, here's a tip: {randomTip}");
                        continue;
                    }
                }

                // Keyword recognition
                string matchedKeyword = keywordResponses.Keys.FirstOrDefault(k => input.Contains(k));
                if (!string.IsNullOrEmpty(matchedKeyword))
                {
                    string response = GetRandomResponse(keywordResponses[matchedKeyword]);
                    DisplayTypingEffect(response);
                    continue;
                }

                // Fallback default response
                DisplayTypingEffect("I'm not sure I understand. Can you try rephrasing?");
            }
        }

        static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
      .----------------.  
     | .--------------. |
     | |  CYBER BOT   | |
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

        static string GetRandomResponse(List<string> responses)
        {
            Random rand = new Random();
            return responses[rand.Next(responses.Count)];
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

        static string DetectSentiment(string input)
        {
            if (input.Contains("worried"))
            {
                return "It's completely normal to feel worried. Cyber threats are real, but you’re doing the right thing by learning.";
            }
            if (input.Contains("frustrated"))
            {
                return "Don't worry, cybersecurity can be complex. Let’s work through it together.";
            }
            if (input.Contains("curious"))
            {
                return "Curiosity is great! Ask away and I’ll provide the best cybersecurity insights I can.";
            }
            return null;
        }
    }
}

