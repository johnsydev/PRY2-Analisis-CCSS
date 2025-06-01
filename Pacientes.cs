using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PRY2_Analisis_CCSS
{
    public class Pacientes
    {
        public int prioridad;
        public int id_paciente;
        public string nombre;
        public ArrayList especialidades = new ArrayList();

        public static ArrayList listaPacientes = new ArrayList();
        public static int cantidadPacientes = 0;

        public Pacientes(int prioridad, string nombre)
        {
            cantidadPacientes++;
            this.id_paciente = cantidadPacientes;
            this.prioridad = prioridad;
            this.nombre = nombre;
            listaPacientes.Add(this);
        }

        public void setEspecialidad(Especialidad especialidad)
        {
            this.especialidades.Add(especialidad);
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

        public ArrayList getEspecialidades() {
            return this.especialidades;
        }

        public static ArrayList getListaPacientes()
        { 
            return listaPacientes; 
        }

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