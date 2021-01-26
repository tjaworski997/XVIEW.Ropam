using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XVIEW.Ropam.BLL
{
    public class VMPC
    {
        public static string ByteArrayToString(byte[] data)
        {
            string result = System.Text.Encoding.UTF8.GetString(data);
            return result;
        }
        public static string DecodeString(string str, string key, string vec)
        {
            if (str != null && !string.IsNullOrEmpty(str))
            {
                byte[] array = Convert.FromBase64String(str);
                char[] array2 = key.ToCharArray();
                byte[] array3 = new byte[array2.Length];
                for (int i = 0; i < array2.Length; i++)
                {
                    array3[i] = (byte)array2[i];
                }
                char[] array4 = vec.ToCharArray();
                byte[] array5 = new byte[array4.Length];
                for (int j = 0; j < array4.Length; j++)
                {
                    array5[j] = (byte)array4[j];
                }
                VMPC.Encode(array, 0, array.Length, array3, array5, array5.Length);

                return Encoding.UTF8.GetString(array);
            }
            return str;
        }
        public static void Encode(byte[] bytes, int startIdx, int endIdx, byte[] key, byte[] vector, int vectorLen = 16)
        {
            byte[] array = new byte[256];
            if (endIdx - startIdx > 0)
            {
                byte b = 0;
                for (int i = 0; i < 256; i++)
                {
                    array[i] = (byte)i;
                }
                byte b2;
                for (int i = 0; i <= 767; i++)
                {
                    b2 = (byte)(i % 256);
                    b = array[(int)(b + array[(int)b2] + key[i % key.Length]) % 256];
                    byte b3 = array[(int)b2];
                    array[(int)b2] = array[(int)b];
                    array[(int)b] = b3;
                }
                for (int i = 0; i <= 767; i++)
                {
                    b2 = (byte)(i % 256);
                    b = array[(int)(b + array[(int)b2] + vector[i % vectorLen]) % 256];
                    byte b3 = array[(int)b2];
                    array[(int)b2] = array[(int)b];
                    array[(int)b] = b3;
                }
                b2 = 0;
                for (int i = startIdx; i < endIdx; i++)
                {
                    b = array[(int)(b + array[(int)b2]) % 256];
                    bytes[i] ^= array[(int)(array[(int)array[(int)b]] + 1) % 256];
                    byte b3 = array[(int)b2];
                    array[(int)b2] = array[(int)b];
                    array[(int)b] = b3;
                    b2 += 1;
                }
            }
        }
        public static string EncodeString(string str, string key, string vec)
        {
            if (str != null && !string.IsNullOrEmpty(str))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                char[] array = key.ToCharArray();
                byte[] array2 = new byte[array.Length];
                for (int i = 0; i < array.Length; i++)
                {
                    array2[i] = (byte)array[i];
                }
                char[] array3 = vec.ToCharArray();
                byte[] array4 = new byte[array3.Length];
                for (int j = 0; j < array3.Length; j++)
                {
                    array4[j] = (byte)array3[j];
                }
                VMPC.Encode(bytes, 0, bytes.Length, array2, array4, array4.Length);
                return Convert.ToBase64String(bytes);
            }
            return str;
        }
        public static byte[] StringToByteArray(string text)
        {
            char[] array = text.ToCharArray();
            byte[] array2 = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = (byte)array[i];
            }
            return array2;
        }
    }
}