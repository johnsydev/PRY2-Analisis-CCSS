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

        public Consultorios(int id_consultorio)
        {
            this.id_consultorio = id_consultorio;
            this.Disponible = true;
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

    }
}
