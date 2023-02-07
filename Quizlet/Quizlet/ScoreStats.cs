using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quizlet.Entrance.UserAuthorization;
using static System.Formats.Asn1.AsnWriter;

namespace Quizlet.Quizlet
{
    internal class ScoreStats
    {
        readonly string _statsPath = $"{Environment.CurrentDirectory}\\Stats.json";
        BindingList<Stats> Scores;
        IOServices ioServices;

      
        public void MyStats()
        {
            ioServices = new IOServices(_statsPath);
            Scores = ioServices.LoadDate<Stats>();

            foreach (var score in Scores)
            {
                if (score.Login == CurrentLogin)
                {
                    Console.WriteLine("Name: "+ score.Login);
                    Console.WriteLine("Topic: "+ score.TopicName);
                    Console.WriteLine("Score: "+score.Score);
                }
                Console.WriteLine();
            }
        }

        public void TopStats(string topic)
        {
            ioServices = new IOServices(_statsPath);
            Scores = ioServices.LoadDate<Stats>();

            string topicname;
            switch (topic)
            {
                case "1":
                    topicname = "Biology";
                    break;

                case "2":
                    topicname = "Math";

                    break;

                case "3":
                    topicname = "English";

                    break;

                default:
                    Console.WriteLine("Unkown topic");
                    return;
            }

            int counter = 0;

            foreach (var score in Scores)
            {
                if (counter > 20) return;
                if (score.TopicName == topicname)
                {
                    Console.WriteLine(score.Login);
                    Console.WriteLine("Topic: " + score.TopicName);
                    Console.WriteLine("Score: " + score.Score);
                    counter++;
                }
                Console.WriteLine();
            }
        }

    }
}
