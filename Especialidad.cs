using System.Collections;
using System.Data;

namespace PRY2_Analisis_CCSS
{
    public class Especialidad
    {
        public int id_especialidad;
        public string nombre;
        int tiempoAtendido;
        public bool disponible;

        public static ArrayList especialidades = new ArrayList();

        public Especialidad(int id, string nombre)
        {
            this.id_especialidad = id;
            this.nombre = nombre;
            this.disponible = true;
            especialidades.Add(this);
        }

        public void AbrirEspecialidad()
        {
            this.disponible = true;
        }

        public void CerrarEspecialidad()
        {
            this.disponible = false;
        }

        public void setTiempoAtencion(int tiempo)
        {
            this.tiempoAtendido = tiempo;
        }

        public int getTiempoAtencion()
        {
            return this.tiempoAtendido;
        }

        public static ArrayList getEspecialidades()
        {
            return especialidades;
        }

        public static Especialidad buscarEspecialidad(int id)
        {
            foreach(Especialidad especialidad in especialidades)
            {
                if (especialidad.id_especialidad == id)
                {
                    return especialidad;
                }
            }
            return null;
        }
    }
}