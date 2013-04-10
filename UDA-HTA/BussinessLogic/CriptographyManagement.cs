using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterfaceBussinessLogic;
using System.Security.Cryptography;

namespace BussinessLogic
{
    class CriptographyManagement : ICriptographyManagement
    {
        public string sha256Encryipt(string clearPswd)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(clearPswd));
            return byteArrayToString(hashedDataBytes);
        }


        private string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }

            return output.ToString();
        }


    }
}
