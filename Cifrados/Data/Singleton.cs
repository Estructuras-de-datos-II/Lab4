using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cifrados.Modelo;

namespace Cifrados.Data
{
    public sealed class Singleton
    {
        private readonly static Singleton _instance = new Singleton();


        //public string textoparacomprimir = "";
        //public string mensajecomp;
        //public string mensajedescomp;
        //public Cifrados.Cesar comprimircesar = new Cifrados.Cesar();

      

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
