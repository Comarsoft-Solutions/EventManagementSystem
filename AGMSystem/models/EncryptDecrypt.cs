using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AGMSystem.models
{
    public class EncryptDecrypt
    {
        //---- any text string is good here
        private static string m_strPassPhrase = "MyPriv@Password!$$";
        //--- we are doing MD5 encryption - can be "SHA1"
        private static string m_strHashAlgorithm = "MD5";
        //--- can be any number
        private static int m_strPasswordIterations = 2;
        //--- must be 16 bytes
        private static string m_strInitVector = "@1B2c3D4e5F6g7H8";
        //--- can be 192 or 128
        private static int m_intKeySize = 256;



        //Encrypt Function: 

        static internal string EncryptPasswordMD5(string plainText, string p_strSaltValue)
        {

            string strReturn = string.Empty;

            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.

            try
            {
                byte[] initVectorBytes = null;
                initVectorBytes = System.Text.Encoding.ASCII.GetBytes(m_strInitVector);

                byte[] saltValueBytes = null;
                saltValueBytes = System.Text.Encoding.ASCII.GetBytes(p_strSaltValue);

                // Convert our plaintext into a byte array.
                // Let us assume that plaintext contains UTF8-encoded characters.
                byte[] plainTextBytes = null;
                plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);

                // First, we must create a password, from which the key will be derived.
                // This password will be generated from the specified passphrase and 
                // salt value. The password will be created using the specified hash 
                // algorithm. Password creation can be done in several iterations.

                Rfc2898DeriveBytes password = default(Rfc2898DeriveBytes);

                password = new Rfc2898DeriveBytes(m_strPassPhrase, saltValueBytes, m_strPasswordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = null;
                int intKeySize = 0;

                intKeySize = Convert.ToInt32((m_intKeySize / 8));

                keyBytes = password.GetBytes(intKeySize);

                // Create uninitialized Rijndael encryption object.
                System.Security.Cryptography.RijndaelManaged symmetricKey = null;
                symmetricKey = new System.Security.Cryptography.RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = System.Security.Cryptography.CipherMode.CBC;

                //symmetricKey.Padding = PaddingMode.Zeros


                // Generate encryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                System.Security.Cryptography.ICryptoTransform encryptor = null;
                encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                System.IO.MemoryStream memoryStream = null;
                memoryStream = new System.IO.MemoryStream();

                // Define cryptographic stream (always use Write mode for encryption).
                System.Security.Cryptography.CryptoStream cryptoStream = null;
                cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, encryptor, System.Security.Cryptography.CryptoStreamMode.Write);
                // Start encrypting.
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memory stream into a byte array.
                byte[] cipherTextBytes = null;
                cipherTextBytes = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert encrypted data into a base64-encoded string.
                string cipherText = null;
                cipherText = Convert.ToBase64String(cipherTextBytes);

                // Return encrypted string.
                strReturn = cipherText;

            }
            catch (Exception ex)
            {
                strReturn = null;
            }

            return strReturn;

        }




        //Decrypt Function: 

        static internal string DecryptPasswordMD5(string cipherText, string p_strSaltValue)
        {

            string strReturn = string.Empty;

            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.


            try
            {
                byte[] initVectorBytes = null;
                initVectorBytes = System.Text.Encoding.ASCII.GetBytes(m_strInitVector);

                byte[] saltValueBytes = null;
                saltValueBytes = System.Text.Encoding.ASCII.GetBytes(p_strSaltValue);

                // Convert our ciphertext into a byte array.
                byte[] cipherTextBytes = null;
                cipherTextBytes = Convert.FromBase64String(cipherText);

                // First, we must create a password, from which the key will be 
                // derived. This password will be generated from the specified 
                // passphrase and salt value. The password will be created using
                // the specified hash algorithm. Password creation can be done in
                // several iterations.

                Rfc2898DeriveBytes password = default(Rfc2898DeriveBytes);

                password = new Rfc2898DeriveBytes(m_strPassPhrase, saltValueBytes, m_strPasswordIterations);

                // Use the password to generate pseudo-random bytes for the encryption
                // key. Specify the size of the key in bytes (instead of bits).
                byte[] keyBytes = null;
                int intKeySize = 0;

                intKeySize = Convert.ToInt32((m_intKeySize / 8));

                keyBytes = password.GetBytes(intKeySize);

                // Create uninitialized Rijndael encryption object.
                System.Security.Cryptography.RijndaelManaged symmetricKey = null;
                symmetricKey = new System.Security.Cryptography.RijndaelManaged();

                // It is reasonable to set encryption mode to Cipher Block Chaining
                // (CBC). Use default options for other symmetric key parameters.
                symmetricKey.Mode = System.Security.Cryptography.CipherMode.CBC;

                //symmetricKey.Padding = PaddingMode.Zeros

                // Generate decryptor from the existing key bytes and initialization 
                // vector. Key size will be defined based on the number of the key 
                // bytes.
                System.Security.Cryptography.ICryptoTransform decryptor = null;
                decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);

                // Define memory stream which will be used to hold encrypted data.
                System.IO.MemoryStream memoryStream = null;
                memoryStream = new System.IO.MemoryStream(cipherTextBytes);

                // Define memory stream which will be used to hold encrypted data.
                System.Security.Cryptography.CryptoStream cryptoStream = null;
                cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, decryptor, System.Security.Cryptography.CryptoStreamMode.Read);

                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold ciphertext;
                // plaintext is never longer than ciphertext.
                byte[] plainTextBytes = null;
                plainTextBytes = new byte[cipherTextBytes.Length + 1];

                // Start decrypting.
                int decryptedByteCount = 0;
                decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Convert decrypted data into a string. 
                // Let us assume that the original plaintext string was UTF8-encoded.
                string plainText = null;
                plainText = System.Text.Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

                // Return decrypted string.
                strReturn = plainText;

            }
            catch (Exception ex)
            {
                strReturn = null;
            }

            return strReturn;


        }


        public string EncryptPassword(string Password)
        {

            //Encrypt the Password
            string sEncryptedPassword = "";
            string sEncryptKey = "P@SSW@RD@09";
            //Should be minimum 8 characters

            try
            {
                sEncryptedPassword = EncryptDecrypt.EncryptPasswordMD5(Password, sEncryptKey);

            }
            catch (Exception ex)
            {
                return sEncryptedPassword;
            }

            return sEncryptedPassword;
        }


        public string DecryptPassword(string Password)
        {
            //Encrypt the Password
            string sDecryptedPassword = "";
            string sEncryptKey = "P@SSW@RD@09";
            //Should be minimum 8 characters

            try
            {
                sDecryptedPassword = EncryptDecrypt.DecryptPasswordMD5(Password, sEncryptKey);

            }
            catch (Exception ex)
            {
                return sDecryptedPassword;
            }

            return sDecryptedPassword;
        }
    }
}