using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Compresion.Data
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
                
                    Buffer = lectura.ReadToEnd();
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

                Agregar_A_Salida(DiccionarioGeneral[W], false);
                EscribirCompress();
                file.Close();
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

                    Agregar_A_Salida(DiccionarioGeneral[W], true);

                    W = K;

                }
            }

            void Agregar_A_Salida(int id, bool caso)
            {
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
                File.Close();
            }
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
       
        
        public void DescompresionLZW(string path)
        {
            var DiccionarioDescompresion = new Dictionary<int, string>();
            var Iteracion = 0;
            var compreso = new FileStream(path, FileMode.Open);
            var lector = new StreamReader(compreso);
            var linea = string.Empty; 
            linea +=(char)lector.Read();
            int CodigoViejo = 0, CodigoNuevo = 0;
            string Cadena = string.Empty, Caracter = string.Empty;
            var Texto_Descompreso = string.Empty;
           
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
                   DiccionarioDescompresion.Add(int.Parse(temp[2]), "|");
                }
                else
                {
                   DiccionarioDescompresion.Add(int.Parse(temp[1]), temp[0]);

                }

            }
            Iteracion = DiccionarioDescompresion.Count();
            CodigoViejo = lector.Read();
            Caracter = DiccionarioDescompresion[CodigoViejo];
            Texto_Descompreso += Caracter;

            //MIENTRAS (!EOF)
            while (true)
            {
                //...LEER cód_nuevo
                CodigoNuevo = lector.Read();
                if (CodigoNuevo==-1)
                {
                    break;
                }
                if (!DiccionarioDescompresion.ContainsKey(CodigoNuevo))
                {
                    Cadena = DiccionarioDescompresion[CodigoViejo]+ DiccionarioDescompresion[CodigoViejo][0];
                    DiccionarioDescompresion.Add(Iteracion, Cadena);
                    Iteracion++;
                    Texto_Descompreso += CodigoNuevo;
                    CodigoViejo = CodigoNuevo;
                }
                //...SINO
                else
                {
                    Cadena = DiccionarioDescompresion[CodigoNuevo];
                    Texto_Descompreso += Cadena;
                    Caracter = Cadena[0].ToString();
                    DiccionarioDescompresion.Add(Iteracion, $"{DiccionarioDescompresion[CodigoViejo]}{Caracter}");
                    Iteracion++;
                    CodigoViejo = CodigoNuevo;
                }
            }


            //Escritura en archivo
            var directory = Path.GetDirectoryName(path);
            var name = Path.GetFileNameWithoutExtension(path);
            var Decompress = new FileStream($"{directory}\\Dec_{name}.txt",FileMode.Create);
            var writer = new StreamWriter(Decompress);
            foreach (var item in Texto_Descompreso)
            {
                writer.Write(item.ToString());
            }
        }
    }
}
