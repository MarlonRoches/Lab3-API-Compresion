using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.Data;
namespace Desarrollo_Compresiones
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = Console.ReadLine();
            ArbolHuffman.Instance.MainCompresionHuffman(root);
            ArbolHuffman.Instance.HuffDescompresion("C:\\Users\\roche\\Desktop\\Compressed_BIBLIA COMPLETA.huff");
           
        }
    }
}
