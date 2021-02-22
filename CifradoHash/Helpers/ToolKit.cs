using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace CifradoHash.Helpers
{
    public static class ToolKit
    {
        public static bool ArraysComparative(byte[] a, byte[] b)
        {
            bool equals = true;
            if (a.Length != b.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].Equals(b[i]) == false)
                    {
                        equals = false;
                        break;
                    }
                }
            }
            return equals;
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            BinaryFormatter binForm = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                Object obj = (Object)binForm.Deserialize(memStream);
                return obj;
            }
        }
    }
}
