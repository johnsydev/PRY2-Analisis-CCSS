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
        public Panel panelCola;
        public Label labelTiempo;
        public Tiquete pacienteAdentro;

        public static ArrayList consultorios = new ArrayList();
        public static int cantidadConsultorios = 0;

        public Consultorios(int id)
        {
            cantidadConsultorios++;
            this.id_consultorio = id;
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
            for (int i = consultorios.Count - 1; i >= 0; i--)
            {
                Consultorios cons = (Consultorios)consultorios[i];
                if (cons.id_consultorio == id)
                {
                    consultorios.RemoveAt(i);
                    break;
                }
            }
        }

        public static ArrayList getConsultorios()
        {
            return consultorios;
        }

        public void agregarPaciente(Tiquete paciente) {
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

        public int getTotalTiempoAtencion()
        {
            int tiempoTotal = 0;
            foreach (Tiquete tiquete in colaPacientes)
            {
                Especialidad especialidad = tiquete.getEspecialidad();
                if (especialidad != null)
                {
                    tiempoTotal += especialidad.getTiempoAtencion();
                }
            }
            return tiempoTotal;
        }

        public int CantidadPacientes()
        {
            return colaPacientes.Count;
        }

        public static int ObtenerPrimerIDDisponible()
        {
            int id = 1;
            List<int> usados = new List<int>();

            foreach (object obj in consultorios)
            {
                Consultorios cons = obj as Consultorios;
                if (cons != null)
                {
                    usados.Add(cons.id_consultorio);
                }
            }

            while (usados.Contains(id))
            {
                id++;
            }

            return id;
        }


        public void AtenderPaciente(DateTime horaActual)
        {
            if (colaPacientes.Count > 0)
            {
                Tiquete proximo = (Tiquete)colaPacientes[0];
                if (Principal.ControlDeColas(proximo)) {
                    pacienteAdentro = proximo;
                    colaPacientes.RemoveAt(0);
                    pacienteAdentro.estaEnCola = false;
                    pacienteAdentro.estaAtendido = true;
                    pacienteAdentro.setHoraAtencion(horaActual);
                    Form1.AgregarLog($"El paciente {pacienteAdentro.paciente.getNombre()} ha sido ingresado en el consultorio {this.id_consultorio} a las {horaActual.ToString("HH:mm")}");
                    Principal.AgregarHoraParada(horaActual.AddMinutes(pacienteAdentro.especialidad.getTiempoAtencion()));
                    Principal.ActualizarColas();
                }
                else
                {
                    Form1.AgregarLog($"El paciente {proximo.paciente.getNombre()} ya esta siendo atendido en otro consultorio, tendrá que esperar para entrar en el consultorio {this.id_consultorio}");
                }
            }
            else
            {
                pacienteAdentro = null;
            }
        }

        public string getTiempoFinalizacionAtencion()
        {
            if (pacienteAdentro == null && colaPacientes.Count == 0)
            {
                return "00:00"; 
            }

            DateTime horaActual = Form1.GetHoraVirtualActual();

            if (pacienteAdentro != null)
            {
                horaActual = horaActual.AddMinutes(pacienteAdentro.especialidad.getTiempoAtencion());
            }

            foreach (Tiquete tiquete in colaPacientes)
            {
                horaActual = horaActual.AddMinutes(tiquete.especialidad.getTiempoAtencion());
            }

            return horaActual.ToString("HH:mm"); 
        }


    }
}
