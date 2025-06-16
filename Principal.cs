using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace PRY2_Analisis_CCSS
{
    // This class represents the core of the system's scheduling logic and acts as the "fitness function"
    // in a genetic algorithm context. Its main responsibility is to evaluate and optimize the distribution
    // of patients across consultorios (medical offices) by balancing workload and priority. It handles:
    // assigning patients based on priority, updating queues, managing attention times, and simulating
    // patient flow in a way that aims to maximize overall system efficiency and resource utilization.
    public class Principal
    {
        // This list stores the scheduled stop times for patient attention
        public static ArrayList horasParada = new ArrayList();

        // Adds a stop time (end of patient attention) in "HH:mm" format
        public static void AgregarHoraParada(DateTime hora)
        {
            horasParada.Add(hora.ToString("HH:mm"));
        }

        // Placeholder for logic to assign consultorios automatically (currently empty)
        public static void RepartirConsultorios()
        {
        }

        // Updates the visual waiting list with patients who are not currently in a queue
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

        // Updates all consultorio queues with the most optimal distribution based on priority
        public static void ActualizarColas()
        {
            ArrayList consultorios = Consultorios.getConsultorios();

            // Clear existing visual and logical queues
            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.panelCola != null)
                {
                    consultorio.panelCola.Controls.Clear();
                }
                consultorio.colaPacientes.Clear();
            }

            // Create a new priority queue based on current tickets
            ColaDePrioridad colaPrioridad = new ColaDePrioridad();

            foreach (Tiquete tiquete in Tiquete.getListaTiquetes())
            {
                tiquete.estaEnCola = false;
                colaPrioridad.Agregar(tiquete, tiquete.getPrioridad());
            }

            // Assign patients to the most optimal consultorio based on specialty and availability
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
                        Form1.AgregarVisualPacienteACola(
                            consultorio.panelCola,
                            tiquete.paciente.getNombre(),
                            tiquete.paciente.getImagen(),
                            tiquete
                        );
                    }
                }

                colaPrioridad.Remover();
            }

            // Increase priority for unattended patients
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

        // Finds the most optimal consultorio for a given specialty (lowest total attention time)
        public static Consultorios ConsultorioOptimo(Especialidad especialidad)
        {
            ArrayList consultorios = Consultorios.getConsultorios();
            int min = int.MaxValue;
            Consultorios optimo = null;

            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.buscarEspecialidadPorNombre(especialidad.nombre) != null && consultorio.Disponible)
                {
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

        // Ensures that a patient is not attended in more than one consultorio at the same time
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

        // Checks each consultorio and processes patients if their attention time has ended
        public static void Atender(DateTime horaActual1)
        {
            Debug.Print("Atendiendo pacientes a las: " + horaActual1.ToString("HH:mm"));
            Form1.AgregarLog("Atendiendo pacientes a las: " + horaActual1.ToString("HH:mm"));
            string horaActual = horaActual1.ToString("HH:mm");

            ArrayList consultorios = Consultorios.getConsultorios();

            foreach (Consultorios consultorio in consultorios)
            {
                // If a patient is inside and the queue is not empty
                if ((consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro != null)
                    || (consultorio.pacienteAdentro != null && consultorio.colaPacientes != null))
                {
                    Tiquete pacienteActual = consultorio.pacienteAdentro;
                    Debug.Print("Paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                    Form1.AgregarLog("Paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);

                    // If the patient's attention time has finished
                    if (pacienteActual.horaSalida == horaActual)
                    {
                        Debug.Print("Paciente atendido: " + pacienteActual.paciente.getNombre() + " en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                        Form1.AgregarLog("Paciente atendido: " + pacienteActual.paciente.getNombre() + " en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);

                        pacienteActual.estaAtendido = true;
                        pacienteActual.estaEnCola = false;
                        pacienteActual.EliminarTiquete();

                        // Attend the next patient if available
                        if (consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro != null)
                        {
                            consultorio.AtenderPaciente(horaActual1);
                        }
                        else
                        {
                            consultorio.pacienteAdentro = null;
                        }

                        if (consultorio.labelTiempo != null)
                            consultorio.labelTiempo.Text = consultorio.getTiempoFinalizacionAtencion();

                        ActualizarColas();
                    }
                }
                // If the consultorio is free and there are patients in the queue
                else if (consultorio.colaPacientes != null && consultorio.colaPacientes.Count > 0 && consultorio.pacienteAdentro == null)
                {
                    Debug.Print("Atendiendo paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                    Form1.AgregarLog("Atendiendo paciente en consultorio: " + consultorio.id_consultorio + " a las: " + horaActual);
                    consultorio.AtenderPaciente(horaActual1);

                    if (consultorio.labelTiempo != null)
                        consultorio.labelTiempo.Text = consultorio.getTiempoFinalizacionAtencion();
                }
            }

            // Remove the current hour from stop hours if reached
            if (horasParada.Contains(horaActual))
            {
                horasParada.Remove(horaActual);
            }
        }
    }
}







