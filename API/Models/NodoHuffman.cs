using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class NodoHuffman
    {
       public NodoHuffman Padre    { get; set; }
       public NodoHuffman Derecho  {get;set;}
       public NodoHuffman Izquierdo{get;set;}
       public char Caracter        {get;set;}
       public double Frecuencia    {get;set;}
        public NodoHuffman(char _caracter)
        {
            Frecuencia = 1;
            Caracter = _caracter;
            Padre = null;
            Izquierdo = null;
            Derecho = null;


        }
    }
}
