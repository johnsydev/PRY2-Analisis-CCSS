using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PRY2_Analisis_CCSS
{
    // This class represents a service ticket assigned to a patient for a specific medical specialty.
    public class Tiquete
    {
        public int prioridad = 0; // Priority of the ticket (used in sorting or assigning order)
        public Especialidad especialidad; // Medical specialty linked to this ticket
        public int id_tiquete; // Unique ID for this ticket
        public static ArrayList listaTiquetes = new ArrayList(); // Static list holding all tickets
        public static int cantidadTiquetes = 0; // Static counter for the total number of tickets
        public Pacientes paciente; // Patient linked to the ticket
        public bool estaAtendido = false; // True if the patient has been attended
        public bool estaEnCola = false; // True if the ticket is currently in a queue
        public string horaAtencion = ""; // Time when the patient starts being attended
        public string horaSalida = ""; // Time when the patient will finish being attended
        public DateTime horaInicioDatetime; // Actual DateTime for internal calculations

        // Default constructor (used for XML deserialization)
        public Tiquete() { }

        // Constructor that sets the specialty and patient, assigns an ID, and adds to the global list
        public Tiquete(Especialidad especialidad, Pacientes paciente)
        {
            cantidadTiquetes++;
            this.prioridad = 1;
            this.especialidad = especialidad;
            this.id_tiquete = cantidadTiquetes;
            this.paciente = paciente;
            listaTiquetes.Add(this);
        }

        // Sets a specific priority value for the ticket
        public void setPrioridad(int prioridad)
        {
            this.prioridad = prioridad;
        }

        // Adds to the current priority value
        public void agregarPrioridad(int prioridad)
        {
            this.prioridad += prioridad;
        }

        // Sets the start and end time for the attention period
        public void setHoraAtencion(DateTime horaEntrada)
        {
            this.horaAtencion = horaEntrada.ToString("HH:mm");
            this.horaSalida = horaEntrada.AddMinutes(especialidad.getTiempoAtencion()).ToString("HH:mm");
            this.horaInicioDatetime = horaEntrada;
        }

        // Removes this ticket from the global list
        public void EliminarTiquete()
        {
            listaTiquetes.Remove(this);
        }

        // Returns the medical specialty associated with this ticket
        public Especialidad getEspecialidad()
        {
            return this.especialidad;
        }

        // Returns the priority of this ticket
        public int getPrioridad()
        {
            return this.prioridad;
        }

        // Returns the static list of all tickets
        public static ArrayList getListaTiquetes()
        {
            return listaTiquetes;
        }
    }
}
