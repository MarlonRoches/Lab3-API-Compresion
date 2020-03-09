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
            int Iteracion;
            string salida = "";  //cambiar por escritura del archivo
            string W = "", K = "";
            var DiccionarioGeneral = ObetnerDiccionarioInicial();//poner como parametro el path global de data
            var DiccionarioWK = DiccionarioGeneral;
            var residuoEscritura = string.Empty;
            var bufferescritura = string.Empty;
            
            CompresionLZW();

            void CompresionLZW()
            {
                var file = new FileStream(_Path, FileMode.Open); // cambiar a dinamico
                var lectura = new BinaryReader(file);
                var Buffer = new byte[8];//buffer
                Iteracion--;
                while (lectura.BaseStream.Position != lectura.BaseStream.Length)
                {
                    Buffer = lectura.ReadBytes(8);
                    foreach (var Caracter in Buffer)
                    {
                        var WK = "";
                        if (W == "")//caso inicial
                        {
                            WK = ((char)Caracter).ToString();
                            Validacion_Diccionario(WK);
                        }
                        else//demas casos
                        {
                            K = ((char)Caracter).ToString();
                            WK = W + K;
                            Validacion_Diccionario(WK);
                        }
                    } //llenar el diccionario con la cantidad total de letras
                }
                Bloq(DiccionarioGeneral[W]);

                //salida = salida + DiccionarioGeneral[W];//ULTIMA INSTRUCCION QUE ES ESCRIBIR LA ULTIMA LINEA
                //file.Close();
            }

            void Validacion_Diccionario(string WK)
            {
                if (DiccionarioGeneral.ContainsKey(WK))
                {
                    W = WK;
                }
                else
                {
                    Iteracion++;
                    DiccionarioGeneral.Add(WK, Iteracion); //generamos codigo
                    Bloq(DiccionarioGeneral[W]);
                    W = K;//bajamos

                }
            }
            //escritura por bloques
            void Bloq(int id)
            {
                var actual = residuoEscritura + Convert.ToString(id, 2).PadLeft(8,'0');
                bufferescritura +=(char)String_A_Byte( actual.Substring(0,8));
                residuoEscritura = actual.Remove(0, 8);
                if (bufferescritura.Length ==100)
                {
                    //escribir
                    bufferescritura = bufferescritura.Remove(0, 100);
                }
            }
            var diccionarioescrito = true;
            void LZWFILE(string caracter)
            {
                var pathFile = new FileStream("C:\\Users\\roche\\Desktop\\Lab1Compresion_\\Compresion\\Compreso.lzw", FileMode.Append);
                var writer = new StreamWriter(pathFile);
                if (diccionarioescrito)
                {
                    foreach (var item in DiccionarioWK)
                    {
                        writer.Write(item.Key + item.Value);
                    }
                    diccionarioescrito = false;
                }

            }


            //methods
            Dictionary<string, int> ObetnerDiccionarioInicial()
            {
                int bufferLength = 10000;
                //patth = path;
                //rutaAGuardar = ruta;
                var File = new FileStream(_Path, FileMode.Open); // cambiar a dinamico
                var Lector = new BinaryReader(File);
                var byteBuffer = new byte[bufferLength];//buffer
                var Diccionario = new Dictionary<string, int>();
                Iteracion = 0;
                while (Lector.BaseStream.Position != Lector.BaseStream.Length)
                {
                    byteBuffer = Lector.ReadBytes(bufferLength);
                    foreach (var Caracter in byteBuffer) //Crear diccionario de letras
                    {
                        if (!Diccionario.ContainsKey(Convert.ToString((char)Caracter))) { Diccionario.Add(Convert.ToString((char)Caracter), Iteracion); Iteracion++; }
                    }
                }

                File.Close();
                return Diccionario;

            }
        }
        byte String_A_Byte(string bufer) //String binario a byte
        {

            int num, binVal, decVal = 0, baseVal = 1, rem;
            num = int.Parse(bufer);
            binVal = num;

            while (num > 0)
            {
                rem = num % 10;
                decVal = decVal + rem * baseVal;
                num = num / 10;

                baseVal = baseVal * 2;
            }
            return Convert.ToByte(decVal);
        }
    }
}
