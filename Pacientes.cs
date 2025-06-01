using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace PRY2_Analisis_CCSS
{
    public class Pacientes
    {
        public int id_paciente;
        public string nombre;
        public ArrayList especialidades = new ArrayList();
        public ArrayList tiquetes = new ArrayList();

        public static ArrayList listaPacientes = new ArrayList();
        public static int cantidadPacientes = 0;

        public Pacientes(string nombre)
        {
            cantidadPacientes++;
            this.id_paciente = cantidadPacientes;
            this.nombre = nombre;
            listaPacientes.Add(this);
        }

        public void setEspecialidad(Especialidad especialidad)
        {
            this.especialidades.Add(especialidad);
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public void asignarTiquete(Especialidad especialidad)
        {
            Tiquete tiquete = new Tiquete(especialidad);
            this.tiquetes.Add(tiquete);
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