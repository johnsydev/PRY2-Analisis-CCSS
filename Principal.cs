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
            Console.WriteLine("3. Nuevo Paciente");
            Console.WriteLine("4. Ver consultorios");
            Console.WriteLine("5. Asignar especialidad a consultorio");
            Console.WriteLine("6. Cerrar consultorio");
            Console.WriteLine("7. Asignar especialidad a paciente");
            Console.WriteLine("8. Ver pacientes");
            Console.WriteLine("0. Salir");

            Console.Write("Seleccione una opción: ");
        }

        public void Main()
        {
            Console.WriteLine("Bienvenido");
            Menu();
            int opcion = int.Parse(Console.ReadLine());

            ArrayList colaDeEspera = new ArrayList();

            int prioridadPaciente = 1;

            while (opcion != 0)
            {
                switch (opcion)
                {
                    case 1:
                        Consultorios consultorio = new Consultorios();
                        Console.WriteLine("Consultorio creado con ID: " + consultorio.id_consultorio);
                        break;

                    case 2:
                        Console.Write("Ingrese el nombre de la especialidad: ");
                        string nombre = Console.ReadLine();

                        Especialidad especialidad = new Especialidad(nombre);

                        Console.Write("Ingrese el tiempo de atencion de la especialidad (minutos): ");
                        int tiempoAtencion = int.Parse(Console.ReadLine());

                        especialidad.setTiempoAtencion(tiempoAtencion);

                        Console.WriteLine("Especialidad creada: " + especialidad.nombre +
                                          " con tiempo de atención: " + especialidad.getTiempoAtencion() + " minutos.");
                        break;

                    case 3:
                        Console.Write("Ingrese el nombre del paciente: ");
                        string nombrePaciente = Console.ReadLine();
                        Pacientes paciente = new Pacientes(nombrePaciente, "");
                        prioridadPaciente++;
                        break;

                    case 4:
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

                    case 5:
                        Console.Write("Ingrese el numero del consultorio: ");
                        int numeroC = int.Parse(Console.ReadLine());
                        Consultorios c = Consultorios.buscarConsultorio(numeroC);

                        Console.Write("Ingrese el numero de la especialidad: ");
                        int numeroE = int.Parse(Console.ReadLine());
                        Especialidad e = Especialidad.buscarEspecialidad(numeroE);

                        c.AgregarEspecialidad(e);
                        Console.WriteLine("Especialidad agregada correctamente");
                        break;

                    case 6:
                        Console.Write("Ingrese el numero del consultorio: ");
                        int numeroCerrar = int.Parse(Console.ReadLine());
                        Consultorios cCerrar = Consultorios.buscarConsultorio(numeroCerrar);
                        cCerrar.CerrarConsultorio();
                        Console.WriteLine("Consultorio cerrado.");
                        break;

                    case 7:
                        Console.Write("Ingrese el numero del paciente: ");
                        int numeroP = int.Parse(Console.ReadLine());
                        Pacientes pac = Pacientes.buscarPaciente(numeroP);
                        Console.Write("Ingrese el numero de la especialidad: ");
                        int numeroEsp = int.Parse(Console.ReadLine());
                        Especialidad esp = Especialidad.buscarEspecialidad(numeroEsp);
                        pac.setEspecialidad(esp);
                        Console.WriteLine("Especialidad asignada.");
                        break;
                    case 8:
                        ArrayList listaPacientes = Pacientes.getListaPacientes();
                        if (listaPacientes.Count > 0)
                        {
                            foreach (Pacientes paci in listaPacientes)
                            {
                                Console.WriteLine("Paciente: " + paci.id_paciente + " - " + paci.nombre);
                                
                                if (paci.getEspecialidades().Count > 0)
                                {
                                    foreach (Especialidad especialidadPaci in paci.getEspecialidades())
                                    {
                                        Console.WriteLine("   - " + especialidadPaci.nombre);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay pacientes.");
                        }
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

