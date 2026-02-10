// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/
using MaquinaDeTuring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquinaDeTuring
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

            string cadena=LeerEntradaAlfabeto("Por favor ingresa la cadena inicial para la cinta (usa solo a, b, c, d o _):");
            int posicion = LeerEnteroValido($"Ingrese la posición inicial del cabezal (0 a {cadena.Length -1}):",0, cadena.Length-1);
            /*Console.WriteLine("porfavor ingresa la cadena");
            string cadena = Console.ReadLine();*/

            /*Console.WriteLine("Ingrese la posicion inicial del cabezal");
            int posicion = int.Parse(Console.ReadLine());*/



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
                    Console.WriteLine("4. Buscar Símbolo");
                    Console.WriteLine("5. Borrar Simbolo Actual");
                    Console.WriteLine("10. Salir");

                    Console.Write("\n Opcion: ");

                /*string opcion = Console.ReadLine();*/
                int opcion = LeerEnteroValido("\nElija una opción (1-10):", 1, 10);

                switch (opcion)
                    {
                        case 1:
                            m.MoverDerecha();
                            break;
                        case 2:
                            m.MoverIzquierda();
                            break;
                        case 3:
                            Console.Write("Ingrese el símbolo a escribir {a,b,c,d}: ");
                            char simbolo = Console.ReadKey().KeyChar;
                            m.Escribir(simbolo);
                            break;
                        case 4:
                            m.Buscar();
                             break;
                        case 5:
                            m.Borrar();
                            break;  
                        case 10:
                            salir = true;
                            break;
                    }
                }
            }

        static string LeerEntradaAlfabeto(string mensaje)
        {
            string entrada = "";
            while (true)
            {
                Console.WriteLine(mensaje);
                entrada = Console.ReadLine().ToLower();

                if (!string.IsNullOrEmpty(entrada) && entrada.All(c => "abcd_".Contains(c)))
                    return entrada;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" -> ERROR: Solo caracteres {a, b, c, d} o '_'");
                Console.ResetColor();
            }
        }

        static int LeerEnteroValido(string mensaje, int min, int max)
        {
            int num;
            while (true)
            {
                Console.WriteLine(mensaje);
                if (int.TryParse(Console.ReadLine(), out num) && num >= min && num <= max)
                    return num;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" -> ERROR: Ingresa un número entre {min} y {max}.");
                Console.ResetColor();
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