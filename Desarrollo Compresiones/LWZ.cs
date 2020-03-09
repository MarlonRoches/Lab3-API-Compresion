using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desarrollo_Compresiones
{
    class LWZ
    {
        private static LWZ _instance = null;
        public static LWZ Instance
        {
            get
            {
                if (_instance == null) _instance = new LWZ();
                return _instance;
            }
        }

        public void CompresionLZW(string _Path)
        {
            int n;
            string salida = "";  //cambiar por escritura del archivo
            string W = "", K = "";
            var DiccionarioOriginal = ObetnerDiccionarioInicial();//poner como parametro el path global de data

            var moni = 0;
            Dictionary<string, int> ObetnerDiccionarioInicial()
            {
                int bufferLength = 10000;
                //patth = path;
                //rutaAGuardar = ruta;
                var File = new FileStream(_Path, FileMode.Open); // cambiar a dinamico
                var Lector = new BinaryReader(File);
                var byteBuffer = new byte[bufferLength];//buffer
                var Diccionario = new Dictionary<string, int>();
                n = 0;
                while (Lector.BaseStream.Position != Lector.BaseStream.Length)
                {
                    byteBuffer = Lector.ReadBytes(bufferLength);
                    foreach (var Caracter in byteBuffer) //Crear diccionario de letras
                    {
                        if (!Diccionario.ContainsKey(Convert.ToString((char)Caracter))) { Diccionario.Add(Convert.ToString((char)Caracter), n); n++; }
                    }
                }

                File.Close();
                return Diccionario;

            }
        }
    }
}
