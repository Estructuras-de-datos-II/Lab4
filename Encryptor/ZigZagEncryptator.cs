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
            int rest = dataToDencrypt.Length % 2;
            byte[] decryptedData = new byte[dataToDencrypt.Length];
            for(int i = 0; i < (dataToDencrypt.Length / 2 + rest); i++)
            {
                decryptedData[i * 2] = dataToDencrypt[i];
            }
            int j = 0;
            for (int i = (dataToDencrypt.Length) / 2 + rest; i < dataToDencrypt.Length; i++)
            {
                decryptedData[j * 2 + 1] = dataToDencrypt[i];
                j++;
            }
            return decryptedData;
        }

        public byte[] decrypt(byte[] dataToDencrypt, byte[] key)
        {
            throw new NotImplementedException();
        }

        public byte[] encrypt(byte[] dataToEncrypt, int key)
        {
            byte[] leftData;
            if (dataToEncrypt.Length % 2 == 1)
            {
                leftData = new byte[(dataToEncrypt.Length / 2 + 1)];
            }
            else
            {
                leftData = new byte[(dataToEncrypt.Length / 2)];
            }
            byte[] rightData = new byte[(dataToEncrypt.Length / 2)];

            int i = 0;
            foreach (byte b in dataToEncrypt)
            {

                    if(i % 2 == 0)
                    {
                        leftData[i / 2] = b;
                    }
                    else
                    {
                        rightData[i / 2] = b;
                    }
                    i++;
            }
            byte[] encryptedData = new byte[leftData.Length + rightData.Length];
            leftData.CopyTo(encryptedData, 0);
            //Console.WriteLine(encryptedData.Length);
            //Console.WriteLine(leftData.Length + rightData.Length);
            rightData.CopyTo(encryptedData, leftData.Length);

            var sb = new StringBuilder("new byte[] { ");
            foreach (var b in rightData)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            //Console.WriteLine(sb.ToString());

            return encryptedData;
        }

        public byte[] encrypt(byte[] dataToEncrypt, byte[] key)
        {
            throw new NotImplementedException();
        }
    }
}
