﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    public class Consultorios
    {
        public int id_consultorio;
        public ArrayList especialidades = new ArrayList();
        public bool Disponible;
        public ArrayList colaPacientes = new ArrayList(); 

        public static ArrayList consultorios = new ArrayList();
        public static int cantidadConsultorios = 0;

        public Consultorios()
        {
            cantidadConsultorios++;
            this.id_consultorio = cantidadConsultorios;
            this.Disponible = true;
            consultorios.Add(this);
        }

        public void AgregarEspecialidad(Especialidad especialidad) { // Esto necesita de la clase Especialidades
            Especialidad espCopia = new Especialidad(especialidad.id_especialidad, especialidad.nombre);
            this.especialidades.Add(espCopia);
        }

        public void EliminarEspecialidad(Especialidad especialidad) { 
            this.especialidades.Remove(especialidad);
        }

        public ArrayList getEspecialidades() { 
            return this.especialidades;
        }

        public void CerrarConsultorio() { 
            this.Disponible = false;
        }

        public void AbrirConsultorio(){
            this.Disponible = true;
        }

        public static void EliminarConsultorio(int id) {
            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.id_consultorio == id)
                {
                    consultorios.Remove(consultorio);
                }
            }
        }

        public static ArrayList getConsultorios()
        {
            return consultorios;
        }

        public void agregarPaciente(Pacientes paciente) {
            if (Disponible == false)
            {
                MessageBox.Show("El consultorio esta cerrado");
            }
            this.colaPacientes.Add(paciente);
        }

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
    }
}
