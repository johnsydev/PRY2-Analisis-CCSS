using System;

namespace PRY2_Analisis_CCSS
{
    public class Pacientes
    {
        public int prioridad;
        public string nombre;

        public Pacientes(int prioridad, string nombre)
        {
            this.prioridad = prioridad;
            this.nombre = nombre;
        }

        public void setPrioridad(int prioridad)
        {
            this.prioridad = prioridad;
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public int getPrioridad()
        {
            return this.prioridad;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public string ToString()
        {
            return "Prioridad: " + this.prioridad + " Nombre: " + this.nombre;
        }

    }
}