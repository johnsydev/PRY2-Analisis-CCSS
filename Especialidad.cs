using System.Data;

namespace PRY2_Analisis_CCSS
{
    public class Especialidad
    {
        public int id_especialidad;
        public string nombre;
        public bool disponible;

        public Especialidad(int id, string nombre)
        {
            this.id_especialidad = id;
            this.nombre = nombre;
        }

        public void AbrirEspecialidad()
        {
            this.disponible = true;
        }

        public void CerrarEspecialidad()
        { 
            this.disponible = false; 
        }
    }
}