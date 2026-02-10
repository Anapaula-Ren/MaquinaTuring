using System;
using System.Collections.Generic;
using System.Text;

namespace MaquinaDeTuring
{
    public class Maquina
    {
        public List<char> Cinta { get; set; }
        public int Cabezal { get; set; }
        public char SimboloGuardado { get; set; }
        public string EstadoActual { get; set; }

        public Maquina(string cadenaInicial, int posicionInicial)
        {
            Cinta=cadenaInicial.ToList();
            Cabezal=posicionInicial;
            SimboloGuardado=' ';
            EstadoActual = "Inicio";
        }

        public void MoverDerecha()
        {
           Console.WriteLine("Moviendo el cabezal a la derecha...");
        }
        public void MoverIzquierda()
        {
            Console.WriteLine("Moviendo el cabezal a la izquierda...");
        }
        public void Escribir(char simbolo)
        {
            Console.WriteLine($"Escribiendo el símbolo '{simbolo}' en la cinta...");
        }
    } 
}
