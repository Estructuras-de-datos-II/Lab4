using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor
{
    public class ZigZagEncryptator : Encryptator
    {

        public byte[] decrypt(byte[] dataToDencrypt, int key)
        {
            byte[] decryptedData = new byte[dataToDencrypt.Length];
            double colD = dataToDencrypt.Length / key;
            int col = (int)Math.Ceiling(colD);
            int pos = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < col * 2;)
                {

                    int relativePos = j * (key - 1) + i;
                    if (relativePos < dataToDencrypt.Length)
                    {
                        decryptedData[relativePos] = dataToDencrypt[pos];
                        pos++;
                    }

                    j += 2;
                    if (!(i == 0 || i == key - 1))
                    {
                        relativePos = (key - 1) * j - i;
                        if (relativePos < dataToDencrypt.Length)
                        {
                            decryptedData[relativePos] = dataToDencrypt[pos];
                            pos++;
                        }
                    }
                }
            }

            return decryptedData;
        }

        public byte[] decrypt(byte[] dataToDencrypt, byte[] key)
        {
            throw new NotImplementedException();
        }

        public byte[] encrypt(byte[] dataToEncrypt, int key)
        {
            double colD = dataToEncrypt.Length / key;
            int col = (int)Math.Ceiling(colD);
            byte[] encryptedData = new byte[dataToEncrypt.Length];
            int pos = 0;
            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < col * 2;)
                {

                    int relativePos = j * (key - 1) + i;
                    if (relativePos < dataToEncrypt.Length)
                    {
                        encryptedData[pos] = dataToEncrypt[relativePos];
                        pos++;
                    }

                    j += 2;
                    if (!(i == 0 || i == key - 1))
                    {
                        relativePos = (key - 1) * j - i;
                        if (relativePos < dataToEncrypt.Length)
                        {
                            encryptedData[pos] = dataToEncrypt[relativePos];
                            pos++;
                        }
                    }
                }
            }

            return encryptedData;
        }

        public byte[] encrypt(byte[] dataToEncrypt, byte[] key)
        {
            throw new NotImplementedException();
        }
    }
}
