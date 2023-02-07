using Quizlet;
using Quizlet.Entrance;
using System.ComponentModel;
using static System.Formats.Asn1.AsnWriter;
using static Quizlet.Quiz;


UserAuthorization Authorization = new UserAuthorization();
BindingList<Questions> questions = new BindingList<Questions>();
IOServices ioServices;
BindingList<Questions> QuestionsForChech;
BindingList<Questions> EditQuizlet;
Quiz quiz = new Quiz();
string Login, Password;
int input = 0 ;
bool b = false;
string path = "D:\\visual\\Projects\\Exam2\\Quizlet\\bin\\Debug\\net7.0\\";

do
{
    Console.WriteLine("Enter Login");
    Login = Console.ReadLine();

    Console.WriteLine("Enter pasword");
    Password = Console.ReadLine();
	Console.WriteLine();
    if (Authorization.CheckAuthorizedUsers(Login, Password) != UserAuthorization.AutorizeStatus.Admin)
	{
		Console.WriteLine("Uncorrect login or password");
	
	}

	else
	{
	Console.WriteLine("Acces is allowed");
	}

} while (Authorization.CheckAuthorizedUsers(Login, Password) != UserAuthorization.AutorizeStatus.Admin);


do
{
	try
	{



		Console.WriteLine();
		Console.WriteLine("1 - Edit Quizlet");
		Console.WriteLine("2 - Create Quizlet");
		Console.WriteLine("3 - See Quizlet");

		Console.WriteLine("0 - Exit");
		Console.WriteLine();
		input = Convert.ToInt32(Console.ReadLine());
		Console.WriteLine();
		switch (input)
		{
			case 1:
				Console.WriteLine("Enter quizlet's name");
				string namequiz = Console.ReadLine();
				string temppath = path + namequiz + ".json";

				if (quiz.CheckExistingFile(temppath))
				{

					Console.WriteLine("Enter question's id you want to change");
					int id = Convert.ToInt32(Console.ReadLine());

					Console.WriteLine("Enter new question");
					string newquestion = Console.ReadLine();

					Console.WriteLine("Enter new answer");
					string newanswer = Console.ReadLine();

					ioServices = new IOServices(temppath);
					EditQuizlet = ioServices.LoadDate<Questions>();
					Console.WriteLine();
					for (int i = 0; i < EditQuizlet.Count; i++)
					{
						if (EditQuizlet[i].Id == id)
						{
							EditQuizlet[i].question = newquestion;
							EditQuizlet[i].answer = newanswer;
							Console.WriteLine("Quizlet Succesfully changed");
							ioServices.SaveDate(EditQuizlet);
							b=true;
							break;
						}
					}
					if (b = false)
					{
				    Console.WriteLine("Can not find this id");
					Console.WriteLine();
					break;
					}
					break;
				}

                break;

			case 2:
				Console.WriteLine("Min count question = 20");
				Console.WriteLine("Enter your quizlet's name");

				string nameq = Console.ReadLine();
				Console.WriteLine();

				ioServices = new IOServices(path + nameq+".json");

				for (int i = 1; i < 21; i++)
				{
					Console.WriteLine();
					Console.WriteLine("Enter question");
					string Question = Console.ReadLine();

					Console.WriteLine("Enter answer");
					string Answer = Console.ReadLine();

					Console.WriteLine();
					if (i == 10) Console.WriteLine("Half done");
					Console.WriteLine();

					questions.Add(new Questions { answer = Answer, question = Question, Id = i });
				}

				ioServices.SaveDate(questions);
				Console.WriteLine();
				Console.WriteLine("New quizlet succesfulle created");

				break;

			case 3:
				Console.WriteLine("Enter quizlet's name");
				string name = Console.ReadLine();
				string temp = path + name + ".json";

				if (quiz.CheckExistingFile(temp))
				{


					ioServices = new IOServices(temp);
					QuestionsForChech = ioServices.LoadDate<Questions>();

					foreach (var item in QuestionsForChech)
					{
						Console.WriteLine("Id: " + item.Id);
						Console.WriteLine("Question: " + item.question);
						Console.WriteLine("Answer: " + item.answer);
						
					}
					Console.WriteLine();
					break;
				}
				break;

			case 0:
				break;

			default:
				Console.WriteLine("Wrong number");
				Console.WriteLine();
				break;
		}
	}
	catch
	{
		Console.WriteLine("Something went wrong");
	}
} while (input != 0);