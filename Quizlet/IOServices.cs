using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Quizlet
{
    internal class IOServices
    {
        readonly string _path;
        public IOServices(string path) 
        {
            _path = path;
        }

       public void SaveDate<T>(T data)
        {

            using(StreamWriter writer = File.CreateText(_path))
            {
                string output=JsonConvert.SerializeObject(data);
                writer.WriteLine(output);
            }

        }


        public BindingList<T> LoadDate<T>()
        {
            
            if(!File.Exists(_path))
            {
                File.CreateText(_path).Dispose();
                return new BindingList<T>();
            }

            using(var reader = File.OpenText(_path)) 
            {
                var data = reader.ReadToEnd();
                if (data == "")
                {
                    return new BindingList<T>();
                }
                return JsonConvert.DeserializeObject<BindingList<T>>(data);
            }
        }


    }
}
