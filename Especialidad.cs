using System.Collections;
using System.Data;

namespace PRY2_Analisis_CCSS
{
    // This class represents a medical specialty used in the ticket and consultory system.
    public class Especialidad
    {
        public int id_especialidad; // Unique identifier for the specialty
        public string nombre; // Name of the specialty
        public int tiempoAtendido; // Time in minutes needed to attend a patient in this specialty
        public bool disponible; // Indicates if this specialty is currently available

        public static ArrayList especialidades = new ArrayList(); // Static list containing all specialties
        public static int cantidadEspecialidades = 0; // Static counter to track how many specialties have been created

        // Default constructor (used for XML serialization)
        public Especialidad() { }

        // Constructor that creates a new specialty with a name and assigns a unique ID
        public Especialidad(string nombre)
        {
            cantidadEspecialidades++;
            this.id_especialidad = cantidadEspecialidades;
            this.nombre = nombre;
            this.disponible = true;
            especialidades.Add(this);
        }

        // Constructor used when loading from saved data (allows setting a specific ID)
        public Especialidad(int id, string nombre)
        {
            this.id_especialidad = id;
            this.nombre = nombre;
            this.disponible = true;
        }

        // Marks the specialty as available for assignment
        public void AbrirEspecialidad()
        {
            this.disponible = true;
        }

        // Marks the specialty as unavailable
        public void CerrarEspecialidad()
        {
            this.disponible = false;
        }

        // Sets the time in minutes that a consultation for this specialty takes
        public void setTiempoAtencion(int tiempo)
        {
            this.tiempoAtendido = tiempo;
        }

        // Returns the time in minutes that a consultation for this specialty takes
        public int getTiempoAtencion()
        {
            return this.tiempoAtendido;
        }

        // Returns the static list of all created specialties
        public static ArrayList getEspecialidades()
        {
            return especialidades;
        }

        // Finds and returns a specialty by its ID
        public static Especialidad buscarEspecialidad(int id)
        {
            foreach (Especialidad especialidad in especialidades)
            {
                if (especialidad.id_especialidad == id)
                {
                    return especialidad;
                }
            }
            return null;
        }

        // Finds and returns a specialty by its name
        public static Especialidad buscarEspecialidadPorNombre(string nombre)
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
    }
}
