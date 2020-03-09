using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Data;
using Desarrollo_Compresiones;
namespace Desarrollo_Compresiones
{
    class Program
    {
        static void Main(string[] args)
        {
           // var root = Console.ReadLine();
            LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.txt");
            ArbolHuffman.Instance.Compresion_Huffman("");
            ArbolHuffman.Instance.Descompresio_Huffman("C:\\Users\\roche\\Desktop\\Compressed_Tea.huff");
           
        }
    }
}
