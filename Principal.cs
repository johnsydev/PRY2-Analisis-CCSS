using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

namespace PRY2_Analisis_CCSS
{
    public class Principal
    {

        public void Menu() 
        {
            Console.WriteLine();
            Console.WriteLine("Opciones:");
            Console.WriteLine("1. Crear Consultorios");
            Console.WriteLine("2. Crear Especialidades");
            Console.WriteLine("3. Ver consultorios");
            Console.WriteLine("4. Asignar especialidad a consultorio");
            Console.WriteLine("5. Cerrar consultorio");
            Console.WriteLine("0. Salir");

            Console.Write("Seleccione una opción: ");
        }

        public void Main()
        {
            Console.WriteLine("Bienvenido");
            Menu();
            int opcion = int.Parse(Console.ReadLine());

            int numConsultorio = 1;

            while (opcion != 0)
            {
                switch (opcion)
                {
                    case 1:
                        Consultorios consultorio = new Consultorios(numConsultorio);
                        numConsultorio++;
                        Console.WriteLine("Consultorio creado con ID: " + consultorio.id_consultorio);
                        break;

                    case 2:
                        Console.Write("Ingrese el nombre de la especialidad: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Ingrese el numero de la especialidad: ");
                        int numero = int.Parse(Console.ReadLine());

                        Especialidad especialidad = new Especialidad(numero, nombre);

                        Console.Write("Ingrese el tiempo de atencion de la especialidad: ");
                        int tiempoAtencion = int.Parse(Console.ReadLine());

                        especialidad.setTiempoAtencion(tiempoAtencion);

                        Console.WriteLine("Especialidad creada: " + especialidad.nombre +
                                          " con tiempo de atención: " + especialidad.getTiempoAtencion() + " minutos.");
                        break;

                    case 3:
                        ArrayList listaConsultorios = Consultorios.getConsultorios();
                        if (listaConsultorios.Count > 0)
                        {
                            foreach (Consultorios cons in listaConsultorios)
                            {
                                Console.Write("Consultorio: " + cons.id_consultorio);
                                string estado = "(Abierto)";
                                if (cons.Disponible != true)
                                {
                                    estado = "(Cerrado)";
                                }
                                Console.WriteLine(" " + estado);
                                if (cons.getEspecialidades().Count > 0)
                                {
                                    foreach(Especialidad especialidad1 in cons.getEspecialidades())
                                    {
                                        Console.WriteLine("   - " + especialidad1.nombre);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay consultorios");
                        }
                        break;

                    case 4:
                        Console.Write("Ingrese el numero del consultorio: ");
                        int numeroC = int.Parse(Console.ReadLine());
                        Consultorios c = Consultorios.buscarConsultorio(numeroC);

                        Console.Write("Ingrese el numero de la especialidad: ");
                        int numeroE = int.Parse(Console.ReadLine());
                        Especialidad e = Especialidad.buscarEspecialidad(numeroE);

                        c.AgregarEspecialidad(e);
                        Console.WriteLine("Especialidad agregada correctamente");
                        break;

                    case 5:
                        Console.Write("Ingrese el numero del consultorio: ");
                        int numeroCerrar = int.Parse(Console.ReadLine());
                        Consultorios cCerrar = Consultorios.buscarConsultorio(numeroCerrar);
                        cCerrar.CerrarConsultorio();
                        Console.WriteLine("Consultorio cerrado.");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
                Menu();
                Console.Write("Seleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}

