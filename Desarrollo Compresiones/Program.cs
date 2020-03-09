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
            ArbolHuffman.Instance.Compresion_Huffman(root);
            ArbolHuffman.Instance.Descompresio_Huffman("C:\\Users\\roche\\Desktop\\Compressed_Tea.huff");
           
        }
    }
}
