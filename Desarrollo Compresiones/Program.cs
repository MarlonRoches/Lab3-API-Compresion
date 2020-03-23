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
            //C:\\Users\\roche\\Desktop\\Tea.txt
            //C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.txt
            //C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.lzw
            LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\Tea.txt");
            LWZ.Instance.LZWDecompress("C:\\Users\\roche\\Desktop\\Tea.lzw");
            
            //LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\BackUpLZW.txt");
            //LWZ.Instance.LZWDecompress("C:\\Users\\roche\\Desktop\\BackUpLZW.lzw");

           // LWZ.Instance.CompresionLZW("C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.txt");
            //LWZ.Instance.LZWDecompress("C:\\Users\\roche\\Desktop\\Tony\\Lab1Compresion_\\Compresion\\BIBLIA COMPLETA.lzw");
            //ArbolHuffman.Instance.Compresion_Huffman("");
            //ArbolHuffman.Instance.Descompresio_Huffman("C:\\Users\\roche\\Desktop\\Compressed_Tea.huff");
           
        }
    }
}
