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
        public int id_consultorio { get; private set; }
        public ArrayList especialidades { get; set; }
        public int tiempo_atencion { get; set; }
        public bool Disponible { get; set; }

        public Consultorios(int id_consultorio, ArrayList especialidades, int tiempo_atencion)
        {
            this.id_consultorio = id_consultorio;
            this.Disponible = true;
        }

        public void AgregarEspecialidad(Especialidad especialidad) { // Esto necesita de la clase Especialidades
            this.especialidades.Add(especialidad);
        }

        public void setTiempoAtencion(int tiempo_atencion) {
            this.tiempo_atencion = tiempo_atencion;
        }

        public void CerrarConsultorio() { 
            this.Disponible = false;
        }

        public void AbrirConsultorio(){
            this.Disponible = true;
        }

    }
}
