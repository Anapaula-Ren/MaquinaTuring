// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/
using MaquinaDeTuring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CajeroAutomatico
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bienvenidx a la Maquina de Turing Pro");
            Console.WriteLine("presione cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Te proporcionaremos el alfabeto... porfa no lo olvidesw :)");
            Console.WriteLine("Alfabeto: {a,b,c,d}");
            Console.WriteLine("presione cualquier tecla para continuar");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("porfavor ingresa la cadena");
            string cadena = Console.ReadLine();

            Console.WriteLine("Ingrese la posicion inicial del cabezal");
            int posicion = int.Parse(Console.ReadLine());



            Maquina miMaquina = new Maquina(cadena, posicion);
            EjecutarMenu(miMaquina);
        }

            static void EjecutarMenu(Maquina m)
            {
                bool salir = false;
                while (!salir)
                {
                    Console.Clear();
                    DibujarInterfaz(m);

                    Console.WriteLine("Seleccione una acción:");
                    Console.WriteLine("1. Mover Derecha");
                    Console.WriteLine("2. Mover Izquierda");
                    Console.WriteLine("3. Escribir Símbolo");
                    Console.WriteLine("4. Salir");

                    Console.Write("\n Opcion: ");

                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            m.MoverDerecha();
                            break;
                        case "2":
                            m.MoverIzquierda();
                            break;
                        case "3":
                            Console.Write("Ingrese el símbolo a escribir {a,b,c,d}: ");
                            char simbolo = Console.ReadKey().KeyChar;
                            m.Escribir(simbolo);
                            break;
                        case "4":
                            salir = true;
                            break;
                    }
                }
            }

        static void DibujarInterfaz(Maquina m)
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine($" ESTADO ACTUAL: {m.EstadoActual}");
            Console.WriteLine("========================================");

            // Dibujar celdas de la cinta
            Console.Write(" CINTA: ");
            foreach (char c in m.Cinta)
            {
                Console.Write($"[{c}] ");
            }
            Console.WriteLine();

            // Dibujar el cabezal (la flechita)
            Console.Write("        ");
            for (int i = 0; i < m.Cabezal; i++)
            {
                Console.Write("    "); // Espacios para alinear con las celdas
            }
            Console.WriteLine("  ^");
            Console.WriteLine($"\n Memoria (Guardado): [{m.SimboloGuardado}]");
            Console.WriteLine("========================================");
        }

    }
}