using System;
using System.Collections.Generic;
using System.Media;
using System.IO;
using System.Threading;

namespace Cyberbot
{
        internal class Program
        {
            static readonly ConsoleColor MainColor = ConsoleColor.Cyan;
            static readonly ConsoleColor AccentColor = ConsoleColor.Yellow;
            static readonly ConsoleColor HighlightColor = ConsoleColor.White;
            static readonly ConsoleColor ErrorColor = ConsoleColor.Red;
            static readonly ConsoleColor SuccessColor = ConsoleColor.Green;
            static readonly ConsoleColor InfoColor = ConsoleColor.Magenta;

            static string userName = "";
            static bool showTypingEffect = true;
            static string audioFilePath = "greeting.wav";
            static Dictionary<string, int> topicVisits = new Dictionary<string, int>();
            static Random random = new Random();

            static void Main(string[] args)
            {
                try
                {
                    Console.Title = "Cybersecurity Learning Bot";

                    DisplayLogo();
                    PlayVoiceGreeting();
                    ShowWelcomeMessage();
                    GetUserName();
                    GetUserExperience();

                    bool exitRequested = false;
                    while (!exitRequested)
                    {
                        ShowMenu();
                        exitRequested = HandleMenuSelection();
                    }

                    ShowExitMessage();
                    Console.WriteLine("\nPress any key to exit...");
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ErrorColor;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }

            static void GetUserExperience()
            {
                Console.ForegroundColor = MainColor;
                TypeText($"\n{userName}, how would you rate your current cybersecurity knowledge? (1-5)");
                TypeText("1: Complete beginner | 3: Some knowledge | 5: Advanced user");
                Console.ForegroundColor = HighlightColor;

                string input = Console.ReadLine()?.Trim();
                if (int.TryParse(input, out int level) && level >= 1 && level <= 5)
                {
                    Console.ForegroundColor = AccentColor;
                    switch (level)
                    {
                        case 1:
                            TypeText($"Perfect, {userName}! We'll start with the basics and build your knowledge from the ground up.");
                            break;
                        case 2:
                            TypeText($"Great, {userName}! You have some familiarity, and we'll enhance your knowledge with practical tips.");
                            break;
                        case 3:
                            TypeText($"Excellent, {userName}! You have a good foundation - let's expand on that knowledge.");
                            break;
                        case 4:
                            TypeText($"Impressive, {userName}! With your solid knowledge, we'll focus on more advanced concepts.");
                            break;
                        case 5:
                            TypeText($"Wow, {userName}! With your advanced knowledge, we'll cover some expert-level concepts.");
                            break;
                    }
                }
                else
                {
                    Console.ForegroundColor = AccentColor;
                    TypeText("I'll assume you're just starting your cybersecurity journey. We'll begin with the fundamentals!");
                }

                // Initialize topic visit counters
                string[] topics = { "Password Security", "Phishing Awareness", "Public Wi-Fi Safety",
                               "Device Protection", "Social Media Privacy", "Ransomware Protection",
                               "Two-Factor Authentication" };
                foreach (string topic in topics)
                {
                    topicVisits[topic] = 0;
                }
            }

            static void DisplayLogo()
            {
                Console.Clear();
                Console.ForegroundColor = MainColor;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                      CYBERSECURITY GUARDIAN BOT - MAIN INTERFACE               ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");

                string[] asciiArt = new string[]
                {
                 @"╔═══════════════════════════════════════════════╗",
                 @"║                                               ║",
                 @"║       ╔═══╗ ╔═╗  ╔══╗  ╔═╗  ╦═╗               ║",
                 @"║       ║     ║ ║ ╔╝  ║  ║╣   ╠╦╝               ║",
                 @"║       ╚═══╝ ╚═╝ ╚═══╝  ╚═╝  ╩╚═               ║",
                 @"║                                               ║",
                 @"║    ╔══╗  ╔═╗  ╔═╗  ╦ ╦  ╦═╗  ╦  ╔═╗  ╦ ╦      ║",
                 @"║    ╚═╗║  ║╣   ║    ║ ║  ╠╦╝  ║  ╔╩╦╝  ╚╦╝     ║",
                 @"║    ╚═╝╚  ╚═╝  ╚═╝  ╚═╝  ╩╚═  ╩  ╩ ╚═   ╩      ║",
                 @"║                                               ║",
                 @"╚═══════════════════════════════════════════════╝",
                 @"                  ♥ │ ♥                          ",
                 @"                 ♥  │  ♥                         ",
                 @"                ♥   │   ♥                        ",
                 @"               ♥    │    ♥                       ",
                 @"        ╔═════♥═════♥═════♥═════╗                ",
                 @"        ║    SECURITY SHIELD    ║                ",
                 @"        ╚═══════════════════════╝                ",
                 @"        ╱♥╲    ╔═══════╗    ╱♥╲                  ",
                 @"       ╱  ♥╲   ║ ╭───╮ ║   ╱  ♥╲                 ",
                 @"      ╱   ♥ ╲  ║ │ ♥ │ ║  ╱   ♥ ╲                ",
                 @"     ╱    ♥  ╲ ║ ╰───╯ ║ ╱    ♥  ╲               ",
                 @"    ╱     ♥   ╲║       ║╱     ♥   ╲              ",
                 @"   ╱      ♥    ╚═══════╝      ♥    ╲             ",
                 @"  ╱       ♥   ╭───────────╮   ♥     ╲            ",
                 @" ╱        ♥   │  PROTECT  │   ♥      ╲           ",
                 @"╱         ♥   │  YOURSELF │   ♥       ╲          ",
                 @"          ♥   ╰───────────╯   ♥                  ",
                 @"          ♥  ╔═══╗     ╔═══╗  ♥                  ",
                 @"          ♥  ║ ♥ ║     ║ ♥ ║  ♥                  ",
                 @"          ♥  ╚═══╝     ╚═══╝  ♥                  ",
                 @"          ╚═══════════════════╝                  ",
                 @"                                                 ",
                 @"        GUARDING YOUR DIGITAL LIFESTYLE          "
                };

                foreach (string line in asciiArt)
                {
                    Console.ForegroundColor = MainColor;
                    Console.WriteLine(line);
                    Thread.Sleep(20); // Speed up the animation a bit
                }

                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            }

            static void DisplaySmallLogo()
            {
                Console.ForegroundColor = MainColor;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                             CYBERSECURITY GUARDIAN                             ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
            }

            static void PlayVoiceGreeting()
            {
                try
                {
                    string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                    {
                        if (File.Exists(audioFilePath))
                        {
                            SoundPlayer player = new SoundPlayer(audioFilePath);
                            player.Play();
                        }
                        else
                        {
                            Console.ForegroundColor = AccentColor;
                            TypeText("Audio greeting file not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ErrorColor;
                    TypeText($"Unable to play audio greeting: {ex.Message}");
                }
            }

            static void ShowWelcomeMessage()
            {
                Console.ForegroundColor = MainColor;
                TypeText("\nWelcome to your Personal Cybersecurity Guardian!");

                Console.ForegroundColor = AccentColor;
                TypeText("Your journey to becoming more cyber-aware starts right here.");
                TypeText("Learn practical security tips and discover how to protect your digital life.");
                Console.WriteLine();
            }

            static void GetUserName()
            {
                Console.ForegroundColor = MainColor;
                TypeText("Before we begin, may I have your name? ");
                Console.ForegroundColor = HighlightColor;

                userName = Console.ReadLine()?.Trim();
                while (string.IsNullOrWhiteSpace(userName))
                {
                    Console.ForegroundColor = ErrorColor;
                    TypeText("Oops! Please enter a valid name: ");
                    Console.ForegroundColor = HighlightColor;
                    userName = Console.ReadLine()?.Trim();
                }

                Console.ForegroundColor = AccentColor;
                TypeText($"\nPleasure to meet you, {userName}. Let's strengthen your cyber defense together!");

                Console.ForegroundColor = MainColor;
                TypeText("Would you like to disable the typing animation? (Y/N): ");
                Console.ForegroundColor = HighlightColor;

                string response = Console.ReadLine()?.Trim().ToUpper();
                showTypingEffect = !(response == "Y" || response == "YES");
            }

            static void ShowMenu()
            {
                Console.Clear();
                DisplaySmallLogo();

                string[] menuItems = new string[]
                {
                "1. Password Security",
                "2. Phishing Awareness",
                "3. Public Wi-Fi Safety",
                "4. Device Protection",
                "5. Social Media Privacy",
                "6. Ransomware Protection",
                "7. Two-Factor Authentication",
                "8. Exit"
                };

                Console.ForegroundColor = MainColor;
                Console.WriteLine("║ Choose a cybersecurity topic below:                                            ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");

                foreach (string item in menuItems)
                {
                    Console.WriteLine($"║   {item.PadRight(72)}║");
                }

                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            }

            static bool HandleMenuSelection()
            {
                Console.ForegroundColor = AccentColor;
                TypeText("\nEnter your choice (1-8): ");
                Console.ForegroundColor = HighlightColor;

                string input = Console.ReadLine()?.Trim();
                if (!int.TryParse(input, out int choice) || choice < 1 || choice > 8)
                {
                    Console.ForegroundColor = ErrorColor;
                    TypeText("Please select a valid option between 1 and 8.");
                    Thread.Sleep(1000);
                    return false;
                }

                switch (choice)
                {
                    case 1:
                        ShowTopic("Password Security", new[]
                        {
                        "• Use complex, unique passwords for every account.",
                        "• Aim for at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
                        "• Avoid personal info, dictionary words, and common substitutions.",
                        "• Use a password manager to generate and store strong passwords.",
                        "• Change critical passwords every 3-6 months.",
                        "• Consider using passphrases (multiple words together) for better memorability and security."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Try creating a password with at least 12 characters using a mix of letters, numbers, and symbols." },
                        { "Intermediate", "Consider using a password manager like LastPass, Bitwarden, or 1Password to generate and store unique passwords." },
                        { "Advanced", "Set up a master password strategy with different tiers of password strength based on the account importance." }
                        });
                        break;

                    case 2:
                        ShowTopic("Phishing Awareness", new[]
                        {
                        "• Be skeptical of urgent messages asking for personal information.",
                        "• Check email sender addresses carefully for slight misspellings.",
                        "• Hover over links to verify URLs before clicking.",
                        "• Look for poor grammar, spelling errors, and generic greetings.",
                        "• Never provide sensitive information in response to an email request.",
                        "• Contact companies through official channels if you're unsure about a message."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "When you receive an email asking for personal information, always call the company directly using their official phone number." },
                        { "Intermediate", "Install browser extensions that highlight suspicious URLs and email security tools that analyze links." },
                        { "Advanced", "Set up email filtering rules and consider creating disposable email addresses for less important accounts." }
                        });
                        break;

                    case 3:
                        ShowTopic("Public Wi-Fi Safety", new[]
                        {
                        "• Avoid logging into sensitive accounts (banking, email) on public Wi-Fi.",
                        "• Use a reputable VPN service when connecting to public networks.",
                        "• Verify network names before connecting - attackers create similar-sounding networks.",
                        "• Disable auto-connect features on your devices.",
                        "• Enable HTTPS Everywhere in your browser.",
                        "• Consider using your mobile data for sensitive transactions instead."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Make sure you're using 'https://' websites when on public Wi-Fi - look for the padlock icon." },
                        { "Intermediate", "Try a VPN service like NordVPN, ExpressVPN, or Proton VPN to encrypt your connections." },
                        { "Advanced", "Create a separate device profile or user account for when you're using untrusted networks." }
                        });
                        break;

                    case 4:
                        ShowTopic("Device Protection", new[]
                        {
                        "• Keep your operating system and applications updated with the latest patches.",
                        "• Use reputable antivirus/anti-malware software and keep it updated.",
                        "• Enable device encryption where available.",
                        "• Set up automatic screen locks with strong passwords/biometrics.",
                        "• Back up your data regularly to secure locations.",
                        "• Be cautious when installing new applications and review permissions."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Set up automatic updates for your operating system and applications." },
                        { "Intermediate", "Implement a 3-2-1 backup strategy: 3 copies of data, on 2 different media types, with 1 copy stored offsite." },
                        { "Advanced", "Consider disk encryption, application sandboxing, and restrictive permissions models for your devices." }
                        });
                        break;

                    case 5:
                        ShowTopic("Social Media Privacy", new[]
                        {
                        "• Limit what personal information you share publicly on profiles.",
                        "• Review and adjust privacy settings on all platforms regularly.",
                        "• Be cautious about connecting with unknown accounts.",
                        "• Avoid posting about your current location or upcoming travel plans.",
                        "• Use different profile photos across platforms to reduce cross-platform tracking.",
                        "• Regularly audit apps and third-party services connected to your accounts."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Google yourself regularly to see what information is publicly available about you." },
                        { "Intermediate", "Review and restrict the permissions on apps connected to your social media accounts." },
                        { "Advanced", "Create separate professional and personal social media presences with different privacy levels." }
                        });
                        break;

                    case 6:
                        ShowTopic("Ransomware Protection", new[]
                        {
                        "• Back up your data regularly to offline storage or cloud services.",
                        "• Keep a separate backup disconnected from your network.",
                        "• Be vigilant about email attachments and links.",
                        "• Keep software, operating systems, and security tools updated.",
                        "• Use reputable security software with ransomware protection features.",
                        "• Create a security plan for what to do if infected."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Set up automatic cloud backups for your most important files and documents." },
                        { "Intermediate", "Test your backup restoration process to ensure you can recover files if needed." },
                        { "Advanced", "Create an incident response plan that includes steps to isolate infected systems quickly." }
                        });
                        break;

                    case 7:
                        ShowTopic("Two-Factor Authentication", new[]
                        {
                        "• Enable 2FA/MFA on all accounts where available, especially email and financial services.",
                        "• Use authenticator apps instead of SMS when possible for better security.",
                        "• Store backup/recovery codes securely in case you lose access to your authentication device.",
                        "• Don't share verification codes with anyone, even people claiming to be from tech support.",
                        "• Consider using a hardware security key for highest-value accounts.",
                        "• Review your connected devices and sessions regularly."
                    },
                        new Dictionary<string, string>
                        {
                        { "Beginner", "Start by enabling 2FA on your email account - it's often the recovery method for all your other accounts." },
                        { "Intermediate", "Use an authenticator app like Google Authenticator, Authy, or Microsoft Authenticator instead of SMS codes." },
                        { "Advanced", "Consider investing in hardware security keys like YubiKey or Google Titan for your most critical accounts." }
                        });
                        break;

                    case 8:
                        return true;
                }

                return false;
            }

            static void ShowTopic(string title, string[] tips, Dictionary<string, string> personalizedTips)
            {
                // Track which topics the user has visited
                if (topicVisits.ContainsKey(title))
                {
                    topicVisits[title]++;
                }

                Console.Clear();
                DisplaySmallLogo();

                Console.ForegroundColor = HighlightColor;
                Console.WriteLine($"║ {title.ToUpper().PadRight(76)}║");
                Console.ForegroundColor = MainColor;
                Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");

                Console.ForegroundColor = AccentColor;
                Console.WriteLine($"║ {userName}, here are some tips to help you stay secure:                     ║");
                Console.WriteLine("║                                                                              ║");

                Console.ForegroundColor = HighlightColor;
                foreach (string tip in tips)
                {
                    Console.WriteLine($"║   {tip.PadRight(72)}║");
                    Thread.Sleep(50);
                }

                Console.ForegroundColor = MainColor;
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");

                // Show a "Did You Know" fact
                ShowDidYouKnow(title);

                // Show personalized advice
                ShowPersonalizedAdvice(title, personalizedTips);

                // Show real-world scenario
                ShowRealWorldScenario(title);

                AskToLearnMore();
            }

            static void ShowDidYouKnow(string topic)
            {
                Dictionary<string, string[]> facts = new Dictionary<string, string[]>
            {
                { "Password Security", new[] {
                    "The most common password is still '123456', followed by 'password'.",
                    "It would take a computer about 10 years to crack a 12-character password with mixed case, numbers and symbols.",
                    "A password manager can create unique 30+ character passwords you never need to remember."
                }},
                { "Phishing Awareness", new[] {
                    "Over 90% of cyber attacks begin with a phishing email.",
                    "Phishing attacks increased by 350% during the COVID-19 pandemic.",
                    "Spear phishing targets specific individuals with personalized messages."
                }},
                { "Public Wi-Fi Safety", new[] {
                    "Hackers can create fake Wi-Fi hotspots called 'evil twins' that mimic legitimate networks.",
                    "Without a VPN, most data sent over public Wi-Fi can be intercepted.",
                    "Some hackers deploy portable devices that can fit in a pocket to capture Wi-Fi traffic."
                }},
                { "Device Protection", new[] {
                    "The average time before an unpatched computer gets infected online is under 10 minutes.",
                    "Most malware is designed to steal personal data or use your device for crypto mining.",
                    "Some advanced malware can infect your device's firmware, surviving even complete OS reinstalls."
                }},
                { "Social Media Privacy", new[] {
                    "The information from your social media can be used to answer many security questions.",
                    "Photos you post online often contain location data (geotags) that reveal exactly where you were.",
                    "Even after deletion, your data may persist in backups and archives for years."
                }},
                { "Ransomware Protection", new[] {
                    "The average ransomware payment in 2023 exceeded $100,000.",
                    "Even after paying ransom, only about 65% of victims recover all their data.",
                    "Ransomware attacks occur approximately every 11 seconds worldwide."
                }},
                { "Two-Factor Authentication", new[] {
                    "2FA can block over 99.9% of automated attacks on your accounts.",
                    "SMS-based 2FA can be defeated through SIM swapping attacks.",
                    "Using a hardware security key is currently the most secure form of two-factor authentication."
                }}
            };

                if (facts.ContainsKey(topic) && facts[topic].Length > 0)
                {
                    string fact = facts[topic][random.Next(facts[topic].Length)];
                    Console.ForegroundColor = InfoColor;
                    Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║ DID YOU KNOW?                                                                  ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine($"║ {fact.PadRight(76)}║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                }
            }

            static void ShowPersonalizedAdvice(string topic, Dictionary<string, string> personalizedTips)
            {
                int visits = topicVisits[topic];
                string experienceLevel;

                if (visits == 0)
                    experienceLevel = "Beginner";
                else if (visits == 1)
                    experienceLevel = "Intermediate";
                else
                    experienceLevel = "Advanced";

                if (personalizedTips.ContainsKey(experienceLevel))
                {
                    Console.ForegroundColor = SuccessColor;
                    Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║ PERSONALIZED RECOMMENDATION                                                    ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine($"║ {personalizedTips[experienceLevel].PadRight(76)}║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                }
            }

            static void ShowRealWorldScenario(string topic)
            {
                Dictionary<string, string> scenarios = new Dictionary<string, string>
            {
                { "Password Security", "A company employee used the same password for their work and personal accounts. When a shopping website was breached, hackers used those credentials to access the company's network." },
                { "Phishing Awareness", "An executive received an email appearing to be from the CEO asking for an urgent wire transfer. The sender address was off by one letter, but they didn't notice and sent $24,000 to scammers." },
                { "Public Wi-Fi Safety", "While using airport Wi-Fi, a traveler checked their bank account without a VPN. A week later, they discovered unauthorized purchases made with their credit card information." },
                { "Device Protection", "A student postponed updating their laptop for weeks. During that time, malware exploited a known vulnerability that had already been patched, encrypting their thesis research." },
                { "Social Media Privacy", "After posting vacation photos in real-time, a family returned home to find their house had been burglarized. The thieves knew exactly when they would be away." },
                { "Ransomware Protection", "A medical clinic didn't have proper backups when hit by ransomware. They paid $15,000 to recover patient data but still lost 30% of their records permanently." },
                { "Two-Factor Authentication", "A social media influencer without 2FA had their account compromised. The attacker posted inappropriate content and scammed followers before the account could be recovered." }
            };

                if (scenarios.ContainsKey(topic))
                {
                    Console.ForegroundColor = AccentColor;
                    Console.WriteLine("\n╔════════════════════════════════════════════════════════════════════════════════╗");
                    Console.WriteLine("║ REAL-WORLD SCENARIO                                                            ║");
                    Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
                    Console.WriteLine($"║ {scenarios[topic].PadRight(76)}║");
                    Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
                }
            }

            static void AskToLearnMore()
            {
                Console.ForegroundColor = MainColor;
                Console.WriteLine("\nPress any key to return to the main menu...");
                Console.ReadKey();
            }

            static void TypeText(string text)
            {
                if (!showTypingEffect)
                {
                    Console.WriteLine(text);
                    return;
                }

                foreach (char c in text)
                {
                    Console.Write(c);
                    Thread.Sleep(10); // Speed up the typing effect a bit
                }
                Console.WriteLine();
            }

            static void ShowExitMessage()
            {
                Console.Clear();
                DisplaySmallLogo();

                Console.ForegroundColor = AccentColor;
                Console.WriteLine($"║ Thank you for using the Cybersecurity Guardian, {userName}!                   ║");
                Console.WriteLine("╠════════════════════════════════════════════════════════════════════════════════╣");
                Console.ForegroundColor = MainColor;
                Console.WriteLine("║ Remember to practice good security habits every day:                           ║");
                Console.ForegroundColor = SuccessColor;
                Console.WriteLine("║  • Use strong, unique passwords                                                ║");
                Console.WriteLine("║  • Enable two-factor authentication                                            ║");
                Console.WriteLine("║  • Keep your software updated                                                  ║");
                Console.WriteLine("║  • Be cautious of suspicious messages                                          ║");
                Console.WriteLine("║  • Back up your important data                                                 ║");
                Console.ForegroundColor = MainColor;
                Console.WriteLine("║                                                                                ║");
                Console.WriteLine("║ Stay safe in your digital journey!                                             ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════╝");
            }
        }
    }
