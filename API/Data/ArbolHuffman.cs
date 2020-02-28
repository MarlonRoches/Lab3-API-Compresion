using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using API.Models;
using System.Web;

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
        #region Variables

        List<NodoHuffman> Arbol = new List<NodoHuffman>();
        private int bufferLength = 8;
        public Dictionary<char, decimal> Letras = new Dictionary<char, decimal>();
        public List<NodoHuffman> DiccionarioPrefijos = new List<NodoHuffman>();
        int cantidad_de_letras = 0;
        Dictionary<char, string> IndexID = new Dictionary<char, string>();
        string patth = null;
        string rutaAGuardar;
        public bool escrito;
        #endregion
        public void Comprimir(string _root)   
        {
            patth = _root;
            
        }
        
    }
}
