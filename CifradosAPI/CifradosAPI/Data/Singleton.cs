using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cifrados.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        private Singleton()
        {
          
        }

        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}
