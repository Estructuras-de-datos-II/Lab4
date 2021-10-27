using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encryptor 
{
    public class RutaEncryptator : Encryptator
    {
        private const int V = -1;

        public byte[] decrypt(byte[] dataToDencrypt, int key)
        {
            int rows = 0;
            int residualData;
            if (dataToDencrypt.Length % key == 0)
            {
                rows = dataToDencrypt.Length / key;
                residualData = 0;

            }
            else
            {
                rows = dataToDencrypt.Length / key + 1;
                residualData = rows * key - dataToDencrypt.Length ;
            }


            byte[,] data = new byte[rows, key];
            int row = 0, column = 0;
            int x = 0;
            int pos = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (pos < dataToDencrypt.Length) data[i, j] = dataToDencrypt[pos]; pos++;
                }
            }
            byte[] decryptedData = new byte[dataToDencrypt.Length];
            pos = 0;
            for (int r = rows - 1; r >= 0; r--)
            {
                for (int c = 0; c < key; c++)
                {
                    if (!(r == rows - 1 && c >= (key - residualData - 1)))
                    {
                        decryptedData[pos] = data[r, c];
                        pos++;
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
            int rows = 0;
            int residualData = 0;
            if (dataToEncrypt.Length % key == 0)
            {
                rows = dataToEncrypt.Length / key;
            }
            else
            {
                rows = dataToEncrypt.Length / key + 1;
                residualData = rows * key - dataToEncrypt.Length ;
            }

            byte[,] data = new byte[rows, key];

            int pos = 0, i = 0, j = 0;
            for(i = 0; i < rows; i++)
            {
                for(j = 0; j < key; j++)
                {
                    if (pos < dataToEncrypt.Length) data[i, j] = dataToEncrypt[pos];pos++;
                }
            }

            byte[] encryptedData = new byte[dataToEncrypt.Length];
            pos = 0;
            for(int r = rows - 1; r >= 0; r--)
            {
                for(int c = 0; c < key; c++)
                {
                    if(!(r == rows - 1 && c >= (key - residualData - 1)))
                    {
                        encryptedData[pos] = data[r, c];
                        pos++;
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
