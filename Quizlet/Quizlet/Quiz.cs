using Quizlet.Entrance;
using Quizlet.Quizlet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using static Quizlet.Entrance.Autorization;
using System.Threading.Tasks;
namespace Quizlet
{
    internal class Quiz
    {
        readonly string _biologyPath = $"{Environment.CurrentDirectory}\\Biology.json";
        readonly string _mathPath = $"{Environment.CurrentDirectory}\\Math.json";
        readonly string _englisjhPath = $"{Environment.CurrentDirectory}\\English.json";
        readonly string _statsPath = $"{Environment.CurrentDirectory}\\Stats.json";
        string topic;

        BindingList<Questions> _biologyQuestions ;
        BindingList<Questions> _mathQuestions;
        BindingList<Questions> _englishQuestions ;

        BindingList<Stats> _stats;

        IOServices ioServicesBiology;
        IOServices ioServicesMath;
        IOServices ioServicesEnglish;
        IOServices ioServicesStats;


        
        public Quiz()
        {
            ioServicesBiology = new IOServices(_biologyPath);
            ioServicesMath= new IOServices(_mathPath);
            ioServicesEnglish= new IOServices(_englisjhPath);

            ioServicesStats= new IOServices(_statsPath);

            _biologyQuestions = ioServicesBiology.LoadDate<Questions>();
            _mathQuestions = ioServicesMath.LoadDate< Questions>();
            _englishQuestions = ioServicesEnglish.LoadDate< Questions>();

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
                Console.WriteLine(counter+": "+ questions[ChosenQuestion].question);
                Console.WriteLine();
                Console.Write("You answer: ");
                Console.WriteLine();
                if(Console.ReadLine()== questions[ChosenQuestion].answer)
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
            Console.WriteLine("Correct answers: "+ positiveAnswers);

            _stats.Add(new Stats(CurrentLogin,topic, positiveAnswers));
            ioServicesStats.SaveDate(_stats);

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

    }
}
