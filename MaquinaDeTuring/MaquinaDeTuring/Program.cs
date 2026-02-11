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
                    Console.WriteLine("4. Mover Derecha hasta...");
                    Console.WriteLine("5. Mover Izquierda hasta...");
                    Console.WriteLine("6. Buscar Patrón");
                    Console.WriteLine("7. Borrar");
                    Console.WriteLine("8. Reiniciar Máquina");         

                    Console.WriteLine("10. Salir");

                    //Console.Write("\n Opcion: ");

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
                            string s = LeerEntradaAlfabeto("Ingrese el símbolo (a,b,c,d,_)");
                            m.Escribir(s[0]);
                            break;
                        /*Console.Write("Ingrese el símbolo a escribir {a,b,c,d}: ");
                        char simbolo = Console.ReadKey().KeyChar;
                        m.Escribir(simbolo);*/
                        
                        case 4:
                        string busquedaDer = LeerEntradaAlfabeto("¿Qué símbolo quieres buscar hacia la derecha?");
                        bool encontradoDer = m.MoverDerechaHasta(busquedaDer[0]); // Le pasamos el char

                        if (!encontradoDer)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" -> No se encontró el símbolo hasta el final de la cinta.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;
                    /*m.MoverDerechaHasta();
                    break;*/
                        case 5:
                        string busquedaIzq = LeerEntradaAlfabeto("¿Qué símbolo quieres buscar hacia la izquierda?");
                        bool encontradoIzq = m.MoverIzquierdaHasta(busquedaIzq[0]); // Le pasamos el char

                        if (!encontradoIzq)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" -> No se encontró el símbolo en la cinta.");
                            Console.ResetColor();
                            Console.ReadKey();
                        }
                        break;
                    case 6:
                        string patronABuscar = LeerEntradaAlfabeto("Ingresa el patrón a buscar:");

                        if (m.BuscarPatron(patronABuscar))
                        {
                            // Calculamos el rango basado en donde quedó el cabezal
                            m.ResaltadoFin = m.Cabezal;
                            m.ResaltadoInicio = m.Cabezal - (patronABuscar.Length - 1);

                            Console.Clear();
                            DibujarInterfaz(m); // Dibujamos con el color activado

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n -> ¡PATRÓN ENCONTRADO Y RESALTADO!");
                            Console.ResetColor();
                            Console.WriteLine("Presione cualquier tecla para continuar...");
                            Console.ReadKey();

                            // QUITAMOS EL COLOR: Reseteamos para que la siguiente vez salga normal
                            m.ResaltadoInicio = -1;
                            m.ResaltadoFin = -1;
                        }
                        else
                        {
                            Console.WriteLine("No se encontró el patrón.");
                            Console.ReadKey();
                        }/*string miPatron = LeerEntradaAlfabeto("Ingrese el patrón (texto) que desea buscar en la cinta:");

                        // 2. Pasamos el patrón como argumento al método
                        bool encontradoPatron = m.BuscarPatron(miPatron);

                        // 3. Feedback visual para el usuario
                        if (encontradoPatron)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($" -> ¡Patrón '{miPatron}' encontrado! El cabezal se movió al inicio del patrón.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" -> El patrón '{miPatron}' no existe en la cinta.");
                        }
                        Console.ResetColor();
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();*/
                        break;
                    case 7:
                        m.Borrar();
                        break;
                    case 8:
                            Console.WriteLine("Work in progress :)");
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

            // --- AQUÍ ESTÁ EL CAMBIO ---
            Console.Write(" CINTA: ");
            for (int i = 0; i < m.Cinta.Count; i++) // Cambiamos foreach por for
            {
                // Si el índice 'i' está dentro del rango del patrón encontrado...
                if (i >= m.ResaltadoInicio && i <= m.ResaltadoFin)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Pintamos de verde
                }

                Console.Write($"[{m.Cinta[i]}] "); // Imprimimos la celda
                Console.ResetColor(); // Volvemos al color normal para la siguiente
            }
            Console.WriteLine();
            // ---------------------------

            // Dibujar el cabezal (la flechita)
            Console.Write("        ");
            for (int i = 0; i < m.Cabezal; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine("  ^");
            Console.WriteLine($"\n Memoria (Guardado): [{m.SimboloGuardado}]");
            Console.WriteLine("========================================");
            /*Console.WriteLine("\n========================================");
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
            Console.WriteLine("========================================");*/
        }

    }
}