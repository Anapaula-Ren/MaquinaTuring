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
                    Console.WriteLine("4. Buscar a la Derecha");
                    Console.WriteLine("5. Buscar a la Izquierda");
                    Console.WriteLine("6. Buscar Patrón");
                    Console.WriteLine("7. Borrar");
                    Console.WriteLine("8. Reiniciar Máquina");         

                    Console.WriteLine("10. Salir");

                int opcion = LeerEnteroValido("\nElija una opción (1-10):", 1, 10);

                switch (opcion)
                    {
                        case 1:
                        Console.WriteLine("estas seguro de querer moverte a la derecha? (s/n)");
                        string res1 = Console.ReadLine().ToLower();
                        if (res1 == "s")
                        {
                            m.MoverDerecha();
                            break;
                        }
                        break;
                        case 2:
                        Console.WriteLine("estas seguro de querer moverte a la izquierda? (s/n)");
                        string res2 = Console.ReadLine().ToLower();
                        if (res2 == "s")
                        {
                            m.MoverIzquierda();
                            break;
                        } break;
                        case 3:
                        Console.WriteLine("¿Estás seguro de que quieres escribir un símbolo? Esto sobrescribirá el símbolo actual. (s/n)");
                        string res3 = Console.ReadLine().ToLower();
                        if (res3 == "s")
                        {
                            string s = LeerEntradaAlfabeto("Ingrese el símbolo (a,b,c,d,_)");
                            m.Escribir(s[0]);
                            break;
                        }
                            break;
                        case 4:
                        Console.WriteLine("¿Estás seguro de que quieres buscar un símbolo hacia la derecha? (s/n)");
                        string res4 = Console.ReadLine().ToLower();
                        if (res4 == "s")
                        {
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
                        }
                        break;

                        case 5:
                        Console.WriteLine("¿Estás seguro de que quieres buscar un símbolo hacia la izquierda? (s/n)");
                        string res5 = Console.ReadLine().ToLower();
                        if (res5 == "s")
                        {
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
                        }
                        break;
                    case 6:
                        Console.WriteLine("¿Estás seguro de que quieres buscar un patrón? (s/n)");
                        string res6 = Console.ReadLine().ToLower();
                        if (res6 == "s")
                        {
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
                            }
                            break;
                        }break;
                    case 7:
                        Console.WriteLine("¿Estás seguro de que quieres borrar el símbolo actual? (s/n)");
                        string res7 = Console.ReadLine().ToLower();
                        if (res7 == "s")
                        {
                            string letraVieja = LeerEntradaAlfabeto("¿Qué letra quieres buscar en toda la cinta?");
                            string letraNueva = LeerEntradaAlfabeto($"¿Por qué letra quieres cambiar todas las '{letraVieja}'?");

                            // 2. Ejecutamos el cambio en la lógica (la parte de tu compañera)
                            // Usamos [0] porque tus métodos devuelven string, pero necesitamos un char
                            m.ReemplazarTodo(letraVieja[0], letraNueva[0]);

                            // 3. EFECTO VISUAL (Tu especialidad como Front-end)
                            Console.Clear();
                            DibujarInterfaz(m); // Aquí ya se verá el brillo verde en las celdas cambiadas

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"\n -> ¡ÉXITO! Se reemplazaron {m.IndicesResaltados.Count} símbolos.");
                            Console.ResetColor();
                            Console.WriteLine("Presione cualquier tecla para confirmar y quitar el resaltado...");
                            Console.ReadKey();

                            // 4. LIMPIEZA: Quitamos las marcas de color para que la cinta vuelva a la normalidad
                            m.IndicesResaltados.Clear();
                            break;
                        }
                        break;
                    case 8:

                            Console.WriteLine("¿Estás seguro de que quieres reiniciar la máquina? Se perderán los datos actuales. (s/n)");
                            string res = Console.ReadLine().ToLower();
                        if  (res == "s")
                        {
                            Console.Clear();
                            string nuevaCadena= LeerEntradaAlfabeto("Ingrese la nueva cadena inicial (usa solo a, b, c, d o _):");
                            int nuevaPosicion = LeerEnteroValido($"Ingrese la posición inicial del cabezal (0 a {nuevaCadena.Length - 1}):", 0, nuevaCadena.Length - 1);
                            m = new Maquina(nuevaCadena, nuevaPosicion);
                            salir = true;
                            EjecutarMenu(m);
                            return;
                        }
                        else
                        {
                            break;
                        }

                        break;

                    case 10:
                        Console.WriteLine("estas seguro que deseas salir de ma Maquina de Turing?");
                        string res10 = Console.ReadLine().ToLower();
                        if (res10 == "s")
                        {


                            salir = true;
                            break;
                        }
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

            Console.Write(" CINTA: ");
            // Cambiamos el foreach por un ciclo for para usar el índice 'i'
            for (int i = 0; i < m.Cinta.Count; i++)
            {
                // REGLA DE COLOR: 
                // Si el índice i está en el rango de búsqueda O en la lista de reemplazo...
                bool esPatron = (i >= m.ResaltadoInicio && i <= m.ResaltadoFin);
                bool esCambio = m.IndicesResaltados.Contains(i);

                if (esPatron || esCambio)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // ¡Brillo!
                }

                Console.Write($"[{m.Cinta[i]}] ");
                Console.ResetColor(); // Siempre reseteamos para no pintar toda la línea
            }
            Console.WriteLine();

            // Dibujar el cabezal (flechita)
            Console.Write("        ");
            for (int i = 0; i < m.Cabezal; i++) Console.Write("    ");
            Console.WriteLine("  ^");

            Console.WriteLine($"\n Memoria (Guardado): [{m.SimboloGuardado}]");
            Console.WriteLine("========================================");

            /*Console.WriteLine("\n========================================");
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
            Console.WriteLine("========================================");*/

        }
    }
}