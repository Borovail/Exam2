using Quizlet;
using Quizlet.Entrance;
using Quizlet.Quizlet;
using System.ComponentModel;

Quiz quiz = new Quiz();
UserAuthorization autorization = new UserAuthorization();
ScoreStats scoreStats = new ScoreStats();

string Login;
string Password;
string Bitrhdate;

int input=0;
bool status= false;
Console.WriteLine("Have acount? Enter - 0 \t\t\t\t\tCreate account: Enter - 1 ");

do
{

    try
    {
        input = Convert.ToInt32(Console.ReadLine());
        do
        {


            if (input == 0)
            {
                Console.WriteLine("Enter login");
                Login = Console.ReadLine();

                Console.WriteLine("Enter password");
                Password = Console.ReadLine();

                if (autorization.CheckAuthorizedUsers(Login, Password) == UserAuthorization.AutorizeStatus.Unknown)
                {
                    Console.WriteLine();
                    Console.WriteLine("Uncorrect password or login. May be this account do not exist");
                    Console.WriteLine("Try again - 0 \t Create new account - 1");

                    string temp=Console.ReadLine();

                    if (temp == "1") {   input = 1;   status= true;    }
                    if (temp == "0") { input = 0; }
                    if(temp!="0" && temp != "1") Console.WriteLine("Uncorrect answer");
                    
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Access is allowed");
                    status = true;
                }
            }
        } while (status==false);

        
        



        if (input == 1)
        {
            Console.WriteLine("Enter login");
            Login = Console.ReadLine();

            Console.WriteLine("Enter password");
            Password = Console.ReadLine();

            Console.WriteLine("Enter birthdate");
            Bitrhdate = Console.ReadLine();

            autorization.AutorizeUser(Login, Password, Bitrhdate);

            Console.WriteLine("Welcome");
            
        }
        else if(input != 0)
        { 
            Console.WriteLine("Wrong number");
            status = false;
        }


       

    }
    catch
    {
        Console.WriteLine("Something went wrong");
    }

} while (status == false);


do
{
    Console.WriteLine();
    Console.WriteLine("1 - Start Quizlet");
    Console.WriteLine("2 - Check my stats");
    Console.WriteLine("3 - Check top 20 in certain quizlet");
    Console.WriteLine("4 - Settings");
    Console.WriteLine("5 - Exit");
    Console.WriteLine();
    try
    {


        input = Convert.ToInt32(Console.ReadLine());

        switch (input)
        {

            case 1:
                Console.WriteLine("Chose topic\n1 - Biology\t 2 - Math\t1 - English");
                quiz.SelectTopic(Convert.ToInt32(Console.ReadLine()));
                break;

            case 2:
                scoreStats.MyStats();
                break;

            case 3:
                Console.WriteLine("Select in which topic you want to see top 20 \n1 - Biology\t 2 - Math\t1 - English");
                scoreStats.TopStats(Console.ReadLine());
                break;

            case 4:

                Console.WriteLine("Change password - 1 \t\t\t Change birthdate - 2 \t\t\t Exit - 0");
                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        Console.WriteLine("Enter login");
                        Login = Console.ReadLine();
                        Console.WriteLine("Enter new password");
                        Password = Console.ReadLine();

                        autorization.ChangePassword(Login, Password);
                        break;

                    case 2:
                        Console.WriteLine("Enter login");
                        Login = Console.ReadLine();
                        Console.WriteLine("Enter new birthdate");
                        string birthdate = Console.ReadLine();

                        autorization.ChangeBirthDate(Login, birthdate);
                        break;

                    case 0:
                        break;

                    default:
                        Console.WriteLine("Wrong number");
                        break;
                }
                
                break;

                default: 
                Console.WriteLine("Uncorect number");
                break;
        }
    }
    catch
    {
        Console.WriteLine("Something went wrong");
    }

} while (true);

//scoreStats.MyStats();


//string topic = Console.ReadLine();
//scoreStats.TopStats(topic);

//quiz.SelectTopic(1);

//scoreStats.TopStats(topic);

//scoreStats.MyStats();




