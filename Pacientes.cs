using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PRY2_Analisis_CCSS
{
    // This class represents a patient in the system
    public class Pacientes
    {
        public int id_paciente; // Unique ID assigned to each patient
        public string nombre; // Patient's name
        public string genero; // Patient's gender
        public string imagen; // Path or reference to the patient's image
        public string horaLlegada; // The time the patient arrived
        public ArrayList especialidades = new ArrayList(); // List of specialties the patient is assigned to
        public ArrayList tiquetes = new ArrayList(); // List of tickets associated with the patient

        public static ArrayList listaPacientes = new ArrayList(); // Static list of all patients
        public static int cantidadPacientes = 0; // Counter to keep track of how many patients have been created

        // Empty constructor (used for XML serialization)
        public Pacientes() { }

        // Constructor that assigns a name and gender to the patient and adds them to the global list
        public Pacientes(string nombre, string genero)
        {
            cantidadPacientes++;
            this.id_paciente = cantidadPacientes;
            this.genero = genero;
            this.nombre = nombre;
            listaPacientes.Add(this);
        }

        // Adds a specialty to the patient
        public void setEspecialidad(Especialidad especialidad)
        {
            this.especialidades.Add(especialidad);
        }

        // Sets the patient's name
        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        // Returns the patient's name
        public string getNombre()
        {
            return this.nombre;
        }

        // Sets the time the patient arrived
        public void setHoraLlegada(string horaLlegada)
        {
            this.horaLlegada = horaLlegada;
        }

        // Returns the patient's gender
        public string getGenero()
        {
            return this.genero;
        }

        // Sets the image associated with the patient
        public void setImagen(string imagen)
        {
            this.imagen = imagen;
        }

        // Returns the patient's image
        public string getImagen()
        {
            return this.imagen;
        }

        // Adds a ticket to the patient’s list of tickets
        public void asignarTiquete(Tiquete tiquete)
        {
            this.tiquetes.Add(tiquete);
        }

        // Returns the list of specialties the patient is assigned to
        public ArrayList getEspecialidades()
        {
            return this.especialidades;
        }

        // Returns the static list of all patients
        public static ArrayList getListaPacientes()
        {
            return listaPacientes;
        }

        // Searches for a patient by ID in the static list
        public static Pacientes buscarPaciente(int id)
        {
            foreach (Pacientes paci in listaPacientes)
            {
                if (paci.id_paciente == id)
                {
                    return paci;
                }
            }
            return null;
        }
    }
}
