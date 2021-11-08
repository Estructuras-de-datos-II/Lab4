using System;

namespace Encryptor
{
    public interface Encryptator
    {
        byte[] encrypt(byte[] dataToEncrypt, int key);

        byte[] decrypt(byte[] dataToDencrypt, int key);

        byte[] encrypt(byte[] dataToEncrypt, byte[] key);

        byte[] decrypt(byte[] dataToDencrypt, byte[] key);
    }

}

