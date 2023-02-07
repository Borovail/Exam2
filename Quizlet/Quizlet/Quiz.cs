using Quizlet.Entrance;
using Quizlet.Quizlet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static Quizlet.Entrance.UserAuthorization;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace Quizlet
{
    public class Quiz
    {
        readonly string _biologyPath = $"{Environment.CurrentDirectory}\\Biology.json";
        readonly string _mathPath = $"{Environment.CurrentDirectory}\\Math.json";
        readonly string _englisjhPath = $"{Environment.CurrentDirectory}\\English.json";

        readonly string _statsPath = $"{Environment.CurrentDirectory}\\Stats.json";
        readonly string _otherPath = $"{Environment.CurrentDirectory}\\";

        string topic;

        BindingList<Questions> _biologyQuestions;
        BindingList<Questions> _mathQuestions;
        BindingList<Questions> _englishQuestions;

        BindingList<Questions> _otherQuestions;


        BindingList<Stats> _stats;

        IOServices ioServicesBiology;
        IOServices ioServicesMath;
        IOServices ioServicesEnglish;
        IOServices ioServicesStats;

        IOServices ioServicesOther;




        public Quiz()
        {
            ioServicesBiology = new IOServices(_biologyPath);
            ioServicesMath = new IOServices(_mathPath);
            ioServicesEnglish = new IOServices(_englisjhPath);

            ioServicesStats = new IOServices(_statsPath);

            _biologyQuestions = ioServicesBiology.LoadDate<Questions>();
            _mathQuestions = ioServicesMath.LoadDate<Questions>();
            _englishQuestions = ioServicesEnglish.LoadDate<Questions>();

            _stats = ioServicesStats.LoadDate<Stats>();
        }



        public void SelectTopic(int ChooseTopic)
        {
            switch (ChooseTopic)
            {
                case 1:
                    topic = "Biology";
                    StartQuizlet(_biologyQuestions);
                    break;

                case 2:
                    topic = "Math";
                    StartQuizlet(_mathQuestions);
                    break;

                case 3:
                    topic = "English";
                    StartQuizlet(_englishQuestions);
                    break;

                case 4:
                    topic = "Mixed";
                    StartQuizlet(MixQuizlets());
                    break;

                case 5:
                    Console.WriteLine("Enter name of quizlet");
                    string name = Console.ReadLine();
                    if (CheckExistingFile(_otherPath + name + ".json"))
                    {
                    ioServicesOther = new IOServices(_otherPath+ name+".json");
                    _otherQuestions = ioServicesOther.LoadDate<Questions>();
                        topic = "name";
                        StartQuizlet(_otherQuestions);
                        break;
                    }
                    Console.WriteLine("Uncorrect name");
                    break;
            }
        }


        void StartQuizlet(BindingList<Questions> questions)
        {
            int counter = 1;
            int positiveAnswers = 0;
            Random random = new Random();
            do
            {
                int ChosenQuestion = random.Next(questions.Count);
                Console.WriteLine(counter + ": " + questions[ChosenQuestion].question);
                Console.WriteLine();
                Console.Write("You answer: ");
                Console.WriteLine();
                if (Console.ReadLine() == questions[ChosenQuestion].answer)
                {
                    positiveAnswers += 1;
                    Console.WriteLine("Correct answer");
                }
                else
                {
                    Console.WriteLine("Wrong answer");
                    Console.WriteLine();
                }

                counter++;

            } while (counter != 21);

            Console.WriteLine("\t\t\tYour result:");
            Console.WriteLine();
            Console.WriteLine("Correct answers: " + positiveAnswers);

            _stats.Add(new Stats(CurrentLogin, topic, positiveAnswers));
            ioServicesStats.SaveDate(_stats);

            Console.WriteLine("Top bests users");

            var bar = _stats.OrderBy(i => i.Score);
              

            foreach (var item in bar)
            {
                Console.WriteLine("User name: " + item.Login);
                Console.WriteLine("Topic: " + item.TopicName);
                Console.WriteLine("Score " + item.Score);
              
            }
            Console.WriteLine();
            Console.WriteLine("Quiz ended");
        }

        BindingList<Questions> MixQuizlets()
        {
            int counter = 1;
            Random random = new Random();
            var MixedQuestions = new BindingList<Questions>();
            do
            {
                int ChosenQuestionBiol = random.Next(_biologyQuestions.Count);
                int ChosenQuestionMath = random.Next(_mathQuestions.Count);
                int ChosenQuestionEngl = random.Next(_englishQuestions.Count);


                MixedQuestions.Add(_biologyQuestions[ChosenQuestionBiol]);
                MixedQuestions.Add(_mathQuestions[ChosenQuestionMath]);
                MixedQuestions.Add(_englishQuestions[ChosenQuestionEngl]);


                counter++;

            } while (counter != 30);
            return MixedQuestions;
        }


       public bool CheckExistingFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Can not find this quizlet");
                return false;
            }
            return true;

        }
    }

}
