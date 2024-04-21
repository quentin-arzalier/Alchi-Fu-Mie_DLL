using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace AFM_DLL.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        ///     Mélange une liste d'objets
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Retire et renvoie le premier élément de la liste
        /// </summary>
        public static T PopFirst<T>(this IList<T> list)
        {
            var toPop = list[0];
            list.RemoveAt(0);
            return toPop;
        }
    }
}
