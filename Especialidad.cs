using System.Data;

namespace PRY2_Analisis_CCSS
{
    public class Especialidad
    {
        public int id_especialidad;
        public string nombre;
        int tiempoAtendido;
        public bool disponible;

        public Especialidad(int id, string nombre)
        {
            this.id_especialidad = id;
            this.nombre = nombre;
            this.disponible = true;
        }

        public void AbrirEspecialidad()
        {
            this.disponible = true;
        }

        public void CerrarEspecialidad()
        { 
            this.disponible = false; 
        }

        public void setTiempoAtencion(int tiempo) {
            this.tiempoAtendido = tiempo;
        }

        public int getTiempoAtencion() {
            return this.tiempoAtendido;
        }
}