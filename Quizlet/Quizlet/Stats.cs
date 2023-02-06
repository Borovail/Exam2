using Quizlet.Entrance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizlet.Quizlet
{
    internal class Stats: Logins
    {
        string _topicName;
        int _score;
        public string TopicName
        {
            get { return _topicName; }
            set { _topicName = value; }
        }

        public int Score
        {
            get { return _score; }
            set { _score = value; }
        }


        public Stats(string login,string topicname,int score):base(login) 
        {
            _topicName=topicname;
            Score = score;
        }
    }
}
