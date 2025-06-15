using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace PRY2_Analisis_CCSS
{

    public class Principal
    {
        public static ArrayList horasParada = new ArrayList();

        public static void AgregarHoraParada(DateTime hora)
        {
            horasParada.Add(hora.ToString("HH:mm"));
        }

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
                if (true) //(!tiquete.estaAtendido && !tiquete.estaEnCola)
                {
                    tiquete.estaEnCola = false; // Resetear el estado de la cola
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
                        Form1.AgregarVisualPacienteACola(consultorio.panelCola, tiquete.paciente.getNombre(), tiquete.paciente.getImagen(), tiquete);
                    }
                }
                colaPrioridad.Remover();
            }

            // Actualizar prioridad
            foreach (Tiquete tiquete in Tiquete.getListaTiquetes())
            {
                if (!tiquete.estaEnCola)
                {
                    tiquete.agregarPrioridad(1);
                }
                Debug.Print("Cliente: " + tiquete.paciente.getNombre() + ", Prioridad: " + tiquete.getPrioridad());
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
                if (consultorio.buscarEspecialidadPorNombre(especialidad.nombre) != null && consultorio.Disponible)
                {
                    //int cantidad = consultorio.CantidadPacientes();
                    int cantidad = consultorio.getTotalTiempoAtencion();
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

        public static void Atender(DateTime horaActual1)
        {
            Debug.Print("Atendiendo pacientes a las: " + horaActual1.ToString("HH:mm"));
            string horaActual = horaActual1.ToString("HH:mm");
            ArrayList consultorios = Consultorios.getConsultorios();
            foreach (Consultorios consultorio in consultorios)
            {
                if ((consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro != null) || (consultorio.pacienteAdentro != null && consultorio.colaPacientes != null))
                {
                    Tiquete pacienteActual = consultorio.pacienteAdentro;
                    Debug.Print("Paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                    if (pacienteActual.horaSalida == horaActual)
                    {
                        Debug.Print("Paciente atendido: " + pacienteActual.paciente.getNombre() + " en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                        pacienteActual.estaAtendido = true;
                        pacienteActual.estaEnCola = false;
                        pacienteActual.EliminarTiquete(); //atendido

                        if (consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro != null)
                        {
                            consultorio.AtenderPaciente(horaActual1);
                            consultorio.labelTiempo.Text = consultorio.getTiempoFinalizacionAtencion();
                        }

                        ActualizarColas();
                    }
                }
                else if (consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro == null)
                {
                    Debug.Print("Atendiendo paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                    consultorio.AtenderPaciente(horaActual1);
                    //consultorio.labelTiempo.Text = consultorio.getTiempoFinalizacionAtencion();
                }
            }
            if (horasParada.Contains(horaActual))
            {
                horasParada.Remove(horaActual);
            }
        }
    }
}







