using System;

namespace PRY2_Analisis_CCSS
{
    public class Principal
    {
        public void Main()
        {
            Console.WriteLine("Bienvenido");
            Console.WriteLine("1. Crear Consultorios");
            Console.WriteLine("2. Crear Especialidades");

            Console.Write("Seleccione una opción: ");
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Consultorios consultorio = new Consultorios(1);
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
                                      " con tiempo de atención: " + especialidad.tiempo_atencion + " minutos.");
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}

