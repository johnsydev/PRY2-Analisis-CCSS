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
                    if (consultorio.buscarEspecialidadPorNombre(tiquete.especialidad.nombre) != null)
                    {
                        consultorio.agregarPaciente(tiquete);

                        if (consultorio.panelCola != null)
                        {
                            Form1.AgregarVisualPacienteACola(consultorio.panelCola, tiquete.paciente.getNombre(), tiquete.paciente.getImagen());
                        }

                        break;
                    }
                }
            }


            
        }
    }
}

