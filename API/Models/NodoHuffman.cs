using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class NodoHuffman
    {
        NodoHuffman Padre    { get; set; }
        NodoHuffman Derecho  {get;set;}
        NodoHuffman Izquierdo{get;set;}
        char Caracter        {get;set;}
        double Frecuencia    {get;set;}
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
