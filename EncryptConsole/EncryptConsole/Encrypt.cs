using System;
using System.Text;
using Encryptor;

namespace EncryptConsole
{
    class Encrypt
    {
        static void Main(string[] args)
        {
            bool validate = false;
            int typeOfEncryption = 0;
            while (!validate)
            {
                Console.WriteLine("What method of encryption do you want to use? ");
                Console.WriteLine("1. Cesar");
                Console.WriteLine("2. ZigZac");
                Console.WriteLine("3. Ruta");
                typeOfEncryption = Convert.ToInt16(Console.ReadLine());
                if (typeOfEncryption >= 1 && typeOfEncryption <= 3) validate = true; 
            }


            byte[] content = null;
            string pathFile = "";
            try
            {
                Console.WriteLine("Write the path of the file to encrypt: ");
                pathFile = Console.ReadLine();
                content = System.IO.File.ReadAllBytes(pathFile);
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Incorrect path");
                Console.ReadKey();
                return;
            }
            Encryptator x = new CesarEncrypt();
            Object key = null;
            switch (typeOfEncryption)
            {
                case  1:
                    Console.WriteLine("Key (Palabra): ");
                    key = Console.ReadLine();
                    x = new CesarEncrypt();
                    break;
                case 2:
                    Console.WriteLine("Key (Numero positivo): ");
                    key = Convert.ToInt16(Console.ReadLine());
                    x = new ZigZagEncryptator();
                    break;
                case 3:
                    Console.WriteLine("Key (Numero positivo): ");
                    key = Convert.ToInt16(Console.ReadLine());
                    x = new RutaEncryptator();
                    break;
            }
            byte[] dd;
            int n = 0;
            foreach(byte b in content)
            {
                if (b == 0) n++;
            }
            dd = new byte[content.Length - n];
            int i = 0;
            foreach (byte b in content)
            {
                if(b != 0)
                {
                    dd[i] = b;
                    i++;
                }
            }

            byte[] encypted = null;
            switch (typeOfEncryption)
            {
                case 1:
                    encypted = x.encrypt(dd, Encoding.ASCII.GetBytes(key.ToString()));
                    break;
                case 2:
                    encypted = x.encrypt(dd, Convert.ToInt16(key));
                    break;
                case 3:
                    encypted = x.encrypt(dd, Convert.ToInt16(key));
                    break;
            }

            createFileE(encypted, pathFile);
            Console.WriteLine("Encrypt complete, press a key to continue...");
            Console.ReadKey();
            byte[] decrypted = null;
            switch (typeOfEncryption)
            {
                case 1:
                    decrypted = x.decrypt(encypted, Encoding.ASCII.GetBytes(key.ToString()));
                    break;
                case 2:
                    decrypted = x.decrypt(encypted, Convert.ToInt16(key));
                    break;
                case 3:
                    decrypted = x.decrypt(encypted, Convert.ToInt16(key));
                    break;
            }
            createFileD(decrypted, pathFile);
            Console.WriteLine("Decrypt complete, press a key to continue...");
            Console.ReadKey();
        }

        static void createFileE(byte[] data, string pathName)
        {
            System.IO.FileStream oFileStream = null;
            oFileStream = new System.IO.FileStream(pathName + "-encrypted.txt", System.IO.FileMode.Create);
            oFileStream.Write(data, 0, data.Length);
            oFileStream.Close();
        }

        static void createFileD(byte[] data, string pathName)
        {
            System.IO.FileStream oFileStream = null;
            oFileStream = new System.IO.FileStream(pathName + "-decrypted.txt", System.IO.FileMode.Create);
            oFileStream.Write(data, 0, data.Length);
            oFileStream.Close();
        }
    }
}
