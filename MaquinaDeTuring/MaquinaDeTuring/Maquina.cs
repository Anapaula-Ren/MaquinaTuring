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
            EstadoActual = "MoverDerecha";
            if (Cabezal == Cinta.Count - 1)
            {
                Cinta.Add(' ');
            }
            Cabezal++;
        }
        public void MoverIzquierda()
        {
            EstadoActual = "MoverIzquierda";
            if (Cabezal == 0)
            {
                Cinta.Insert(0, ' ');
                Cabezal = 0;
                return;
            }
            Cabezal--;
        }
        public void Escribir(char simbolo)
        {
            EstadoActual = "Escribir";
            SimboloGuardado = simbolo;
            Cinta[Cabezal] = simbolo;
        }

    }
}
