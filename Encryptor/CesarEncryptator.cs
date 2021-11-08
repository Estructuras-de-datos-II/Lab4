using Encryptor;
using System;
using System.Text;

namespace Encryptor
{
    public class CesarEncrypt : Encryptator
    {
        private bool contains(byte value, byte[] data)
        {
            bool containValue = false;
            for(int i = 0; i < data.Length; i++)
            {
                if (data[i] == value) containValue = true;
            }
            return containValue;
        }

        private byte[] getDictionary(byte[] key)
        {
            byte[] auxiliarDictionary = new byte[256 + key.Length];
            int byteValue = 0;
            for(int i = 0; i < auxiliarDictionary.Length; i++)
            {
                if (i < key.Length) auxiliarDictionary[i] = key[i];
                else
                {
                    auxiliarDictionary[i] = (byte)byteValue;
                    byteValue++;
                }
            }

            byte[] dictionary = new byte[256];
            int pos = 0;
            for (int i = 0; i < auxiliarDictionary.Length; i++)
            {
                if(!contains(auxiliarDictionary[i], dictionary) || auxiliarDictionary[i] == 0)
                {
                    dictionary[pos] = auxiliarDictionary[i];
                    pos++;
                }
            }
           

            return dictionary;
        }
        public byte[] encrypt(byte[] dataToEncrypt, byte[] key)
        {
            byte[] encryptDictionary = new byte[256];
            encryptDictionary = getDictionary(key);

            byte[] encryptedData = new byte[dataToEncrypt.Length];
            for (int i = 0; i < dataToEncrypt.Length; i++)
            {
                encryptedData[i] = encryptDictionary[dataToEncrypt[i]];
            }

            return encryptedData;
        }

        int positionInArray(byte value, byte[] array)
        {
            int pos = 0;
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == value) pos = i;
            }

            return pos;
        }

        public byte[] decrypt(byte[] dataToDencrypt, byte[] key)
        {
            byte[] encryptDictionary = new byte[256];
            encryptDictionary = getDictionary(key);

            byte[] decryptedData = new byte[dataToDencrypt.Length];
            for (int i = 0; i < dataToDencrypt.Length; i++)
            {
                decryptedData[i] = (byte)(positionInArray(dataToDencrypt[i],encryptDictionary));
            }

            return decryptedData;
        }

        public byte[] encrypt(byte[] dataToEncrypt, int key)
        {
            throw new NotImplementedException();
        }

        public byte[] decrypt(byte[] dataToDencrypt, int key)
        {
            throw new NotImplementedException();
        }
    }

}
