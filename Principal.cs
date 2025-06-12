using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace PRY2_Analisis_CCSS
{
    public class Principal
    {

        public static void RepartirConsultorios()
        {

        }

        public static void ActualizarEspera()
        {
            ArrayList tiquetes = Tiquete.getListaTiquetes();
            ArrayList listaEspera = new ArrayList();
            foreach (Tiquete tiquete in tiquetes)
            {
                if (!tiquete.estaEnCola)
                {
                    listaEspera.Add(tiquete.paciente);
                }
            }
            Form1.ActualizarEsperaVisual(listaEspera);
        }


        //Deben de atenderse los pacientes en el tiempo mas optimo siempre viendo su prioridad
        public static void ActualizarColas()
        {
            // función fitness
            ArrayList consultorios = Consultorios.getConsultorios();

            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.panelCola != null)
                {
                    consultorio.panelCola.Controls.Clear(); //cola visual
                }
                consultorio.colaPacientes.Clear();
            }

            ColaDePrioridad colaPrioridad = new ColaDePrioridad();
            foreach (Tiquete tiquete in Tiquete.getListaTiquetes())
            {
                if (!tiquete.estaAtendido && !tiquete.estaEnCola)
                {
                    colaPrioridad.Agregar(tiquete, tiquete.getPrioridad());
                }
            }

            while (colaPrioridad.Cantidad() > 0)
            {
                Tiquete tiquete = colaPrioridad.PrimerElemento();
                if (tiquete == null)
                    break;

                String nombreEspecialidad = tiquete.getEspecialidad().nombre;
                Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
                Consultorios consultorio = ConsultorioOptimo(esp);
                if (consultorio != null && esp != null && esp.disponible)
                {
                    consultorio.agregarPaciente(tiquete);
                    tiquete.estaEnCola = true;
                    if (consultorio.panelCola != null)
                    {
                        Form1.AgregarVisualPacienteACola(consultorio.panelCola, tiquete.paciente.getNombre(), tiquete.paciente.getImagen());
                    }
                }
                colaPrioridad.Remover();
            }

            ActualizarEspera();
        }

        //Esto es para buscar el consultorio mas optimo para una especialidad (el que tiene menos pacientes)
        public static Consultorios ConsultorioOptimo(Especialidad especialidad)
        {
            ArrayList consultorios = Consultorios.getConsultorios();
            int min = int.MaxValue;
            Consultorios optimo = null;
            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.getEspecialidades().Contains(especialidad) && consultorio.Disponible)
                {
                    int cantidad = consultorio.CantidadPacientes();
                    if (cantidad < min)
                    {
                        min = cantidad;
                        optimo = consultorio;
                    }
                }
            }
            return optimo;
        }

        //Esto es para validar que el mismo paciente no este en dos consultorios al mismo tiempo
        public static bool ControlDeColas(Tiquete tiquete)
        {
            foreach (Tiquete tiq in Tiquete.getListaTiquetes())
            {
                if (tiq.estaAtendido && tiq.paciente == tiquete.paciente)
                {
                    return false;
                }
            }
            return true;
        }
    }
}







