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

        public int ResaltadoInicio { get; set; }= -1;
        public int ResaltadoFin { get; set; }= -1;
        public Maquina(string cadenaInicial, int posicionInicial)
        {
            Cinta=cadenaInicial.ToList();
            Cabezal=posicionInicial;
            SimboloGuardado='_';
            EstadoActual = "Inicio";
        }
        // Operaciones primitivas
        public void MoverDerecha()
        {
            EstadoActual = "MoverDerecha";
            if (Cabezal == Cinta.Count - 1)
            {
                Cinta.Add('_');
            }
            Cabezal++;
        }
        public void MoverIzquierda()
        {
            EstadoActual = "MoverIzquierda";

            if (Cabezal == 0)
            {
                EstadoActual = "Error: límite izquierdo alcanzado";
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
        // Operaciones compuestas
    
        public bool MoverDerechaHasta(char simbolo)
        {
            MoverDerecha();
            EstadoActual = $"MoverDerechaHasta {simbolo}";
            while (Cinta[Cabezal] != ' ')
            {
                if (Cinta[Cabezal] == simbolo)
                    return true;
                MoverDerecha();
            }
            return false; 
        }
        public bool MoverIzquierdaHasta(char simbolo)
        {
            MoverIzquierda();
            EstadoActual = $"MoverIzquierdaHasta {simbolo}";
            while (Cinta[Cabezal] != ' ')
            {
                if (Cinta[Cabezal] == simbolo)
                    return true;
                MoverIzquierda();
            }
            return false;
        }
        public bool BuscarPatron(string patron)
        {
            EstadoActual = $"BuscarPatron {patron}";
            int pos = Cabezal;
            while (pos + patron.Length <= Cinta.Count)
            {
                bool coincide = true;
                for (int i = 0; i < patron.Length; i++)
                {
                    if (Cinta[pos + i] != patron[i])
                    {
                        coincide = false;
                        break;
                    }
                }
                if (coincide)
                {
                    Cabezal = pos + patron.Length -1;  
                    return true;
                }
                pos++;
            }
            return false;
        }
        public void Borrar()
        {
            Escribir('_');
        }


    }
}
