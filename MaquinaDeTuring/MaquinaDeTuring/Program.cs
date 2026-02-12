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
            Console.WriteLine("Bienvenidx a la Maquina de Turing");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine("Este el alfabeto: ");
            Console.WriteLine("Alfabeto: {a,b,c,d}");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            string cadena=LeerEntradaAlfabeto("Por favor ingrese la cadena inicial para la cinta (utilice solo a, b, c, d o _):");
            int posicion = LeerEnteroValido($"Ingrese la posición inicial del cabezal: (0 a {cadena.Length -1}):",0, cadena.Length-1);

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
                    Console.WriteLine("0. Salir");
                    Console.WriteLine("1. Mover a la derecha");
                    Console.WriteLine("2. Mover a la izquierda");
                    Console.WriteLine("3. Escribir símbolo");
                    Console.WriteLine("4. Buscar a la derecha");
                    Console.WriteLine("5. Buscar a la izquierda");
                    Console.WriteLine("6. Buscar patrón");
                    Console.WriteLine("7. Borrar y reemplazar un símbolo");
                    Console.WriteLine("8. Reiniciar máquina");         

                int opcion = LeerEnteroValido("\nElija una opción (0-8):", 0, 8);

                switch (opcion)
                    {
                    case 0:
                        Console.WriteLine("¿Desea salir de la Maquina de Turing? (s/n)");
                        string res10 = (Console.ReadLine() ?? "").ToLower();
                        if (res10 == "s")
                        {
                            salir = true;
                            break;
                        }
                        break;
                
                    case 1:
                        Console.WriteLine("¿Desea mover a la derecha? (s/n)");
                        string res1 = (Console.ReadLine() ?? "").ToLower();
                        if (res1 == "s")
                        {
                            m.MoverDerecha();
                            break;
                        }
                        break;
                    case 2:
                        Console.WriteLine("¿Desea mover a la izquierda? (s/n)");
                        string res2 = (Console.ReadLine() ?? "").ToLower();
                        if (res2 == "s")
                        {
                            m.MoverIzquierda();
                            break;
                        } 
                        break;
                    case 3:
                        Console.WriteLine("¿Desea escribir un símbolo? Esto sobrescribirá el símbolo actual. (s/n)");
                        string res3 = (Console.ReadLine() ?? "").ToLower();
                        if (res3 == "s")
                        {
                            string s = LeerEntradaAlfabeto("Ingrese el símbolo (a,b,c,d,_)");
                            m.Escribir(s[0]);
                            break;
                        }
                        break;
                    case 4:
                        Console.WriteLine("¿Desea buscar un símbolo hacia la derecha? (s/n)");
                        string res4 = (Console.ReadLine() ?? "").ToLower();
                        if (res4 == "s")
                        {
                            string busquedaDer = LeerEntradaAlfabeto("¿Qué símbolo desea buscar hacia la derecha?");
                            bool encontradoDer = m.MoverDerechaHasta(busquedaDer[0]);

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
                        Console.WriteLine("¿Desea buscar un símbolo hacia la izquierda? (s/n)");
                        string res5 = (Console.ReadLine() ?? "").ToLower();
                        if (res5 == "s")
                        {
                            string busquedaIzq = LeerEntradaAlfabeto("¿Qué símbolo desea buscar hacia la izquierda?");
                            bool encontradoIzq = m.MoverIzquierdaHasta(busquedaIzq[0]); 

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
                        Console.WriteLine("¿Desea buscar un patrón? (s/n)");
                        string res6 = (Console.ReadLine() ?? "").ToLower();
                        if (res6 == "s")
                        {
                            string patronABuscar = LeerEntradaAlfabeto("Ingrese el patrón a buscar:");

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
                        } break;
                    case 7:
                        Console.WriteLine("¿Desea borrar y reemplazar un símbolo? (s/n)");
                        string res7 = (Console.ReadLine() ?? "").ToLower();
                        if (res7 == "s")
                        {
                            string letraVieja = LeerEntradaAlfabeto("¿Qué letra desea buscar en toda la cinta?");
                            string letraNueva = LeerEntradaAlfabeto($"¿Por qué letra desea cambiar todas las '{letraVieja}'?");

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

                        Console.WriteLine("¿Desea reiniciar la máquina? Se perderán los datos actuales. (s/n)");
                        string res = (Console.ReadLine() ?? "").ToLower();
                        if (res == "s")
                        {
                            Console.Clear();
                            string nuevaCadena = LeerEntradaAlfabeto("Ingrese la nueva cadena inicial (utilice solo a, b, c, d o _):");
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

                    }
                }
            }

        static string LeerEntradaAlfabeto(string mensaje)
        {
            string entrada = "";
            while (true)
            {
                Console.WriteLine(mensaje);
                entrada = (Console.ReadLine() ?? "").ToLower();

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
                Console.WriteLine($" -> ERROR: Ingrese un número entre {min} y {max}.");
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
        }
    }
}