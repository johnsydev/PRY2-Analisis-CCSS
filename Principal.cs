using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace PRY2_Analisis_CCSS
{
    public class Principal
    {

        public static void RepartirConsultorios()
        {

        }

        public static void ActualizarEspera()
        {
            ArrayList tiquetes = Tiquete.getListaTiquetes();
            ArrayList listaEspera = new ArrayList();
            foreach (Tiquete tiquete in tiquetes)
            {
                if (!tiquete.estaEnCola)
                {
                    listaEspera.Add(tiquete.paciente);
                }
            }
            Form1.ActualizarEsperaVisual(listaEspera);
        }

        public static void ActualizarColas()
        {
            // función fitness
            ArrayList pacientes = Pacientes.getListaPacientes();


            ArrayList tiquetes = Tiquete.getListaTiquetes();
            ArrayList consultorios = Consultorios.getConsultorios();

            foreach (Consultorios consultorio in consultorios)
            {
                if (consultorio.panelCola != null)
                {
                    consultorio.panelCola.Controls.Clear(); //cola visual
                }
                consultorio.colaPacientes.Clear();
            }


            foreach (Tiquete tiquete in tiquetes)
            {
                foreach (Consultorios consultorio in consultorios)
                {
                    tiquete.estaEnCola = false;
                    if (!consultorio.Disponible)
                    {
                        continue;
                    }
                    Especialidad especialidad = consultorio.buscarEspecialidadPorNombre(tiquete.especialidad.nombre);
                    if (especialidad != null && especialidad.disponible)
                    {
                        consultorio.agregarPaciente(tiquete);
                        tiquete.estaEnCola = true;

                        if (consultorio.panelCola != null)
                        {
                            Form1.AgregarVisualPacienteACola(consultorio.panelCola, tiquete.paciente.getNombre(), tiquete.paciente.getImagen());
                        }

                        break;
                    }
                }
            }

            ActualizarEspera();
            
        }
    }
}

