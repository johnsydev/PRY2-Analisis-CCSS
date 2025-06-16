using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    // This class represents a medical consulting room that can serve patients
    public class Consultorios
    {
        public int id_consultorio; // Unique ID of the consultorio
        public ArrayList especialidades = new ArrayList(); // Specialties assigned to this consultorio
        public bool Disponible; // Indicates whether the consultorio is open or closed
        public ArrayList colaPacientes = new ArrayList(); // Queue of patients (Tiquetes)
        public Panel panelCola; // Visual panel for showing the patient queue
        public Label labelTiempo; // Label to show estimated end time
        public Tiquete pacienteAdentro; // Current patient being attended

        public static ArrayList consultorios = new ArrayList(); // Static list of all consultorios
        public static int cantidadConsultorios = 0; // Total number of consultorios created

        // Constructor: assigns ID and marks consultorio as available
        public Consultorios(int id)
        {
            cantidadConsultorios++;
            this.id_consultorio = id;
            this.Disponible = true;
            consultorios.Add(this);
        }

        // Adds a specialty to the consultorio (by copying it)
        public void AgregarEspecialidad(Especialidad especialidad)
        {
            Especialidad espCopia = new Especialidad(especialidad.id_especialidad, especialidad.nombre);
            this.especialidades.Add(espCopia);
        }

        // Removes a specialty from the consultorio
        public void EliminarEspecialidad(Especialidad especialidad)
        {
            this.especialidades.Remove(especialidad);
        }

        // Returns all specialties in this consultorio
        public ArrayList getEspecialidades()
        {
            return this.especialidades;
        }

        // Closes the consultorio
        public void CerrarConsultorio()
        {
            this.Disponible = false;
        }

        // Opens the consultorio
        public void AbrirConsultorio()
        {
            this.Disponible = true;
        }

        // Removes a consultorio from the global list based on its ID
        public static void EliminarConsultorio(int id)
        {
            for (int i = consultorios.Count - 1; i >= 0; i--)
            {
                Consultorios cons = (Consultorios)consultorios[i];
                if (cons.id_consultorio == id)
                {
                    consultorios.RemoveAt(i);
                    break;
                }
            }
        }

        // Returns the global list of consultorios
        public static ArrayList getConsultorios()
        {
            return consultorios;
        }

        // Adds a patient ticket to the queue
        public void agregarPaciente(Tiquete paciente)
        {
            if (Disponible == false)
            {
                MessageBox.Show("El consultorio esta cerrado");
            }
            this.colaPacientes.Add(paciente);
        }

        // Finds a consultorio by ID
        public static Consultorios buscarConsultorio(int id)
        {
            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.id_consultorio == id)
                {
                    return consultorio;
                }
            }
            return null;
        }

        // Searches for a specialty by name inside this consultorio
        public Especialidad buscarEspecialidadPorNombre(string nombre)
        {
            foreach (Especialidad especialidad in especialidades)
            {
                if (especialidad.nombre == nombre)
                {
                    return especialidad;
                }
            }
            return null;
        }

        // Calculates the total estimated attention time based on the queue
        public int getTotalTiempoAtencion()
        {
            int tiempoTotal = 0;
            foreach (Tiquete tiquete in colaPacientes)
            {
                Especialidad especialidad = tiquete.getEspecialidad();
                if (especialidad != null)
                {
                    tiempoTotal += especialidad.getTiempoAtencion();
                }
            }
            return tiempoTotal;
        }

        // Returns how many patients are currently waiting
        public int CantidadPacientes()
        {
            return colaPacientes.Count;
        }

        // Gets the first available consultorio ID not currently in use
        public static int ObtenerPrimerIDDisponible()
        {
            int id = 1;
            List<int> usados = new List<int>();

            foreach (object obj in consultorios)
            {
                Consultorios cons = obj as Consultorios;
                if (cons != null)
                {
                    usados.Add(cons.id_consultorio);
                }
            }

            while (usados.Contains(id))
            {
                id++;
            }

            return id;
        }

        // Processes the next patient in the queue and sets them as currently being attended
        public void AtenderPaciente(DateTime horaActual)
        {
            if (colaPacientes.Count > 0)
            {
                Tiquete proximo = (Tiquete)colaPacientes[0];
                if (Principal.ControlDeColas(proximo))
                {
                    pacienteAdentro = proximo;
                    colaPacientes.RemoveAt(0);
                    pacienteAdentro.estaEnCola = false;
                    pacienteAdentro.estaAtendido = true;
                    pacienteAdentro.setHoraAtencion(horaActual);
                    Form1.AgregarLog($"El paciente {pacienteAdentro.paciente.getNombre()} ha sido ingresado en el consultorio {this.id_consultorio} a las {horaActual.ToString("HH:mm")}");
                    Principal.AgregarHoraParada(horaActual.AddMinutes(pacienteAdentro.especialidad.getTiempoAtencion()));
                    Principal.ActualizarColas();
                }
                else
                {
                    Form1.AgregarLog($"El paciente {proximo.paciente.getNombre()} ya esta siendo atendido en otro consultorio, tendrá que esperar para entrar en el consultorio {this.id_consultorio}");
                }
            }
            else
            {
                pacienteAdentro = null;
            }
        }

        // Calculates the estimated time at which all patients (including the one being attended) will be finished
        public string getTiempoFinalizacionAtencion()
        {
            if (pacienteAdentro == null && colaPacientes.Count == 0)
            {
                return "00:00";
            }

            DateTime horaActual = Form1.GetHoraVirtualActual();
            int totalMinutos = 0;

            if (pacienteAdentro != null)
            {
                totalMinutos += pacienteAdentro.especialidad.getTiempoAtencion();
            }

            foreach (Tiquete tiquete in colaPacientes)
            {
                if (pacienteAdentro != tiquete && !tiquete.estaAtendido && tiquete != pacienteAdentro)
                    totalMinutos += tiquete.especialidad.getTiempoAtencion();
            }

            Debug.Print("Total tiempo atención acumulado: " + totalMinutos);
            horaActual = horaActual.AddMinutes(totalMinutos);
            return horaActual.ToString("HH:mm");
        }
    }
}
