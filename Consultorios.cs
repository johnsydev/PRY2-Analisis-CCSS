using System;
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
            this.especialidades.Add(especialidad);
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
    }
}
