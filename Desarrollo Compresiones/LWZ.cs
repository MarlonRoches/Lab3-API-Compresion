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
            var DiccionarioWK = ObetnerDiccionarioInicial();
            var residuoEscritura = string.Empty;
            var diccionarioescrito = true;
            CompresionLZW();

            void CompresionLZW()
            {
                var file = new FileStream(_Path, FileMode.Open); // cambiar a dinamico
                var lectura = new StreamReader(file);
                string Buffer = "";//buffer
                Iteracion--;
//w=NULL
                
                    Buffer = lectura.ReadToEnd();
//MIENTRAS (!EOF)
                    foreach (var Caracter in Buffer)
                    {
                        var WK = "";
                        if (W == "")
                        {
                            WK = (Caracter).ToString();
                            Validacion_Diccionario(WK);
                        }
                        else
                        {
                            K = ((char)Caracter).ToString();
                            WK = W + K;
                            Validacion_Diccionario(WK);
                        }
                    } 
//...LEER K
//...SI (wK está en el diccionario) ENTONCES
//......w=wK
//...SINO
//......Imprimir el código de w
//......Agregar wK al diccionario
//......w=K
//...FIN_SI
//FIN_MIENTRAS
//Imprimir el código de w

                //ULTIMA INSTRUCCION QUE ES ESCRIBIR LA ULTIMA LINEA
                Agregar_A_Salida(DiccionarioGeneral[W], false);

                //salida = salida + DiccionarioGeneral[W];
                //file.Close();
                EscribirCompress();
            }

            void Validacion_Diccionario(string WK)
            {
                if (diccionarioescrito)
                {
                    EscribirDiccionario();
                }
                if (DiccionarioGeneral.ContainsKey(WK))
                {
                    W = WK;
                }
                else
                {
                    Iteracion++;
                    DiccionarioGeneral.Add(WK, Iteracion); //generamos codigo
                    //Bloq(DiccionarioGeneral[W]);
                    Agregar_A_Salida(DiccionarioGeneral[W], true);
                    //salida += DiccionarioGeneral[W];
                    W = K;//bajamos

                }
            }
            //escritura por bloques
            void Agregar_A_Salida(int id, bool caso)
            {
                    //tranformar a binario 
                    var carsito = (char)id;
                    salida += carsito;
                   
            }

            void EscribirDiccionario()
            {
                var path = Path.GetDirectoryName(_Path);
                var name = Path.GetFileNameWithoutExtension(_Path);
                var File = new FileStream($"{path}\\{name}.lzw", FileMode.Append);
                var writer = new StreamWriter(File);
                if (diccionarioescrito)
                {
                    foreach (var item in DiccionarioWK)
                    {
                        writer.Write($"{item.Key}|{item.Value}♀");
                    }
                    writer.Write("END");
                    diccionarioescrito = false;
                }
                writer.Close();
            }
            void EscribirCompress()
            {
                var path = Path.GetDirectoryName(_Path);
                var name = Path.GetFileNameWithoutExtension(_Path);
                var File = new FileStream($"{path}\\{name}.lzw", FileMode.Append);
                var writer = new StreamWriter(File);
                foreach (var item in salida)
                {
                    writer.Write(item);
                }
                writer.Close();
            }
            //methods
            Dictionary<string, int> ObetnerDiccionarioInicial()
            {
                var File = new FileStream(_Path, FileMode.Open); // cambiar a dinamico
                var Lector = new StreamReader(File);
                var byteBuffer = string.Empty;//buffer
                var Diccionario = new Dictionary<string, int>();
                Iteracion = 0;
                while (Lector.BaseStream.Position != Lector.BaseStream.Length)
                {
                    byteBuffer = Lector.ReadToEnd();
                    foreach (var Caracter in byteBuffer) //Crear diccionario de letras
                    {
                        if (!Diccionario.ContainsKey(Convert.ToString(Caracter))) 
                        { 
                            Diccionario.Add(Convert.ToString(Caracter), Iteracion); 
                            Iteracion++; 
                        }
                    }
                }

                File.Close();
                return Diccionario;


            }
        }
        int Binario_A_Byte(string bufer) //String binario a byte
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
            return decVal;
        }
        string Decimal_A_Binario(string original)
        {
            int a = original.Length > 0 ? int.Parse(original) : 0;
            string binario = "";
            while (a > 0)
            {
                binario = a % 2 + binario;
                a = a / 2;
            }
            original = binario;
            return original;
        }
        Dictionary<string, int> ObetnerDiccionarioInicial(string path)
        {
            int bufferLength = 8;
            //patth = path;
            //rutaAGuardar = ruta;
            var pathFile = new FileStream(path, FileMode.Open); // cambiar a dinamico
            var Lector = new BinaryReader(pathFile);
            var byteBuffer = new byte[bufferLength];//buffer
            var Diccionario = new Dictionary<string, int>();
            var n = 0;
            while (Lector.BaseStream.Position != Lector.BaseStream.Length)
            {
                byteBuffer = Lector.ReadBytes(bufferLength);
                foreach (var Caracter in byteBuffer) //Crear diccionario de letras
                {
                    if (!Diccionario.ContainsKey(Convert.ToString((char)Caracter)))
                    {
                        var caracter = (char)Caracter;
                        Diccionario.Add(Convert.ToString(caracter), n); n++;
                    }
                }
            }

            pathFile.Close();
            return Diccionario;

        }
        public void LZWDecompress(string path)
        {
            var DiccionarioTemp = new Dictionary<int, string>();
            var n = 0;
            
            var compreso = new FileStream(path, FileMode.Open);
            var lector = new StreamReader(compreso);
            var linea = string.Empty; 
                linea +=(char)lector.Read();

            int Anterior = 0;
            int Actual = 0;
            var Actuals = string.Empty;
            var caracter = string.Empty;
            var Salida = string.Empty;
            while (!linea.Contains("END"))
            {

                linea += (char)lector.Read();
            }
                var caractrer = linea.Split('♀');
            foreach (var item in caractrer)
            {
                if (item=="END")
                {
                    break;
                }
                   var temp = item.Split('|');
                if (temp.Length==3)
                {
                    // tiene | incluido
                   DiccionarioTemp.Add(int.Parse(temp[2]), "|");
                }
                else
                {
                   DiccionarioTemp.Add(int.Parse(temp[1]), temp[0]);

                }

            }
            //LEER cód_viejo
            n = DiccionarioTemp.Count();
            Anterior = lector.Read();
            //carácter=Traducir(cód_viejo)
            caracter = DiccionarioTemp[Anterior];
            //Imprimir carácter
            Salida += caracter;

            //MIENTRAS (!EOF)
            while (true)
            {
                //...LEER cód_nuevo
                Actual = lector.Read();
                if (Actual==-1)
                {
                    break;
                }
                //...SI (cód_nuevo no está en el diccionario) ENTONCES
                if (!DiccionarioTemp.ContainsKey(Actual))
                {
                    Actuals = DiccionarioTemp[Anterior]+ DiccionarioTemp[Anterior][0];
                    //......cadena=Traducir(cód_viejo)
                    DiccionarioTemp.Add(n, Actuals);
                    n++;
                    Salida += Actual;
                    //......cadena=cadena+caracter
                    Anterior = Actual;
                }
                //...SINO
                else
                {
                    //......cadena=Traducir(cód_nuevo)
                    Actuals = DiccionarioTemp[Actual];
                    //...Imprimir cadena
                    Salida += Actuals;
                    //...carácter=Primer carácter de cadena
                    caracter = Actuals[0].ToString();
                    //...Agregar Traducir(cód_viejo)+carácter al diccionario
                    DiccionarioTemp.Add(n, $"{DiccionarioTemp[Anterior]}{caracter}");
                    n++;
                    //...cód_viejo=cód_nuevo
                    Anterior = Actual;
                }
                //...FIN_SI
                //FIN_MIENTRAS
            }

            var directory = Path.GetDirectoryName(path);
            var name = Path.GetFileNameWithoutExtension(path);
            var Decompress = new FileStream($"{directory}\\Dec_{name}.txt",FileMode.Create);
            var writer = new StreamWriter(Decompress);
            foreach (var item in Salida)
            {
                writer.Write(item.ToString());
            }
        }
    }
}
