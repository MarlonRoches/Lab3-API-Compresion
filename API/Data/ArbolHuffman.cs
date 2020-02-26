using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using API.Models;
namespace API.Data
{
    public class ArbolHuffman
    {
        private static ArbolHuffman _instance = null;
        public static ArbolHuffman Instance
        {
            get
            {
                if (_instance == null) _instance = new ArbolHuffman();
                return _instance;
            }
        }
        Dictionary<string, NodoHuffman> Diccionario = new Dictionary<string, NodoHuffman>();
        public void Comprimir(string root)
        {
            var reader = new StreamReader(root);
            var texto = reader.ReadToEnd();
            foreach (var item in texto)
            {
                if (Diccionario.ContainsKey(item.ToString()))
                {
                    // lo contiene
                }
                else
                {
                    //nuevo
                    Diccionario.Add(item.ToString(),new NodoHuffman());
                }
            }
        }

    }
}
