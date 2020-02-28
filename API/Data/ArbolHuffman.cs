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
        Dictionary<char, double> Diccionario = new Dictionary<char, double>();
        Dictionary<string, NodoHuffman> Frecuencias = new Dictionary<string, NodoHuffman>();
        string Root = String.Empty;
        public void Comprimir(string _root)   
        {
            Root = _root;
            ObtenerFrecuencias();
            ArmarArbol();

        }

        public void ArmarArbol()
        {
            Frecuencias = Diccionario;
            var temp = Diccionario.ToArray();
            
            var contador = 1    ;
            while (Diccionario.Count != 1)
            {
                //juntar
                var nuevo = new NodoHuffman($"C{contador}")
                {

                }

                //eliminar
                //Ordenar

            }
        }
        public void ObtenerFrecuencias()
        {
            var reader = new StreamReader(Root);
            var texto = reader.ReadToEnd();
            foreach (var item in texto)
            {
                if (Diccionario.ContainsKey(item.ToString()))
                {
                    // lo contiene
                    Diccionario[item.ToString()].Frecuencia++;
                }
                else
                {
                    //nuevo
                    Diccionario.Add(item.ToString(), new NodoHuffman(item.ToString()));
                }
            }
            foreach (var item in Diccionario)
            {
                item.Value.Frecuencia = item.Value.Frecuencia / texto.Length;
            }
            var Sorting = Diccionario.ToList();
            Sorting.Sort((x, y) => x.Value.Frecuencia.CompareTo(y.Value.Frecuencia));
            Diccionario = new Dictionary<string, NodoHuffman>();
            foreach (var item in Sorting)
            {
                Diccionario.Add(item.Key, item.Value);
            }

            //C:\Users\roche\Desktop\Tony\Lab1Compresion_\Compresion\BIBLIA COMPLETA.txt
            reader.Close();
        }
    }
}
