using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Data;
using System.IO;
using Desarrollo_Compresiones;
namespace Desarrollo_Compresiones
{
    class Program
    {
        static void Main(string[] args)
        {
            // var root = Console.ReadLine();
            //C:\\Users\\roche\\Desktop\\Tea.txt
            //C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.txt
            //C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.lzw
            var file = new FileStream("C:\\Users\\roche\\Desktop\\Tea.txt", FileMode.Open);
            var bin = new StreamReader(file);
            var lol = Convert.ToInt32(bin.BaseStream.Length);
            var bytess = bin.ReadToEnd();
            var lista = new List<string>();
            foreach (var item in bytess)
            {
                lista.Add(item.ToString());
            }
            LWZ.Instance.CompresionLZW(lista,"Tea.txt");

            //descompresion
            var file1 = new FileStream("C:\\Users\\roche\\Desktop\\ED2\\Lab3-API-Compresion\\Desarrollo Compresiones\\bin\\Debug\\Tea.lzw", FileMode.Open);
            var bin1 = new StreamReader(file1);
            var lol1 = Convert.ToInt32(bin1.BaseStream.Length);
            var bytess1 = bin1.ReadToEnd();
            var lista1 = new List<string>();
            foreach (var item in bytess1)
            {
                lista1.Add(item.ToString());
            }
            LWZ.Instance.DescompresionLZW(lista1, "Tea.lzw");
            
            //LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\BackUpLZW.txt");
            //LWZ.Instance.LZWDecompress("C:\\Users\\roche\\Desktop\\BackUpLZW.lzw");

           // LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.txt");
            //LWZ.Instance.LZWDecompress("C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.lzw");
            //ArbolHuffman.Instance.Compresion_Huffman("");
            //ArbolHuffman.Instance.Descompresio_Huffman("C:\\Users\\roche\\Desktop\\Compressed_Tea.huff");
           
        }
    }
}
