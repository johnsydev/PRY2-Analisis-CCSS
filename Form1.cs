using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    public partial class Form1 : Form
    {
        ArrayList cuadros = new ArrayList();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new CustomMenuRenderer();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cuadro_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                Button btn = sender as Button;
                Consultorios cons = Consultorios.buscarConsultorio(int.Parse(btn.Text));

                menu.Items.Add("Asignar especialidad", null, (s, ev) =>
                {
                    string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad", "Asignar especialidad");
                    Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
                    cons.AgregarEspecialidad(esp);

                });
                menu.Items.Add("Cerrar especialidad", null, (s, ev) =>
                {
                    string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad que desea cerrar", "Cerrar especialidad");
                    Especialidad esp = cons.buscarEspecialidadPorNombre(nombreEspecialidad);
                    esp.CerrarEspecialidad();
                    MessageBox.Show("Especialidad cerrada: " + esp.nombre, "Éxito");

                });
                menu.Items.Add("Ver especialidades", null, (s, ev) =>
                {
                    string mensaje = "";
                    foreach (Especialidad esp in cons.getEspecialidades())
                    {
                        string estado = " (Abierto)";
                        if (esp.disponible == false)
                        {
                            estado = " (Cerrado)";
                        }

                        mensaje += "• " + esp.nombre + estado + "\n";
                    }
                    if (mensaje == "")
                    {
                        mensaje = "No tiene especialidades asignadas";
                    }

                    MessageBox.Show(mensaje, "Lista de especialidades:");
                });

                if (!cons.Disponible)
                {
                    menu.Items.Add("Abrir consultorio", null, (s, ev) =>
                    {
                        cons.AbrirConsultorio();
                        btn.BackColor = Color.Green;
                        MessageBox.Show("Consultorio abierto: " + cons.id_consultorio, "Éxito");

                    });
                }
                else
                {
                    menu.Items.Add("Cerrar consultorio", null, (s, ev) =>
                    {
                        cons.CerrarConsultorio();
                        btn.BackColor = Color.Red;
                        MessageBox.Show("Consultorio cerrado: " + cons.id_consultorio, "Éxito");

                    });
                }

                menu.Show(Cursor.Position);
            }
        }

        private void AgregarConsultorio_Click(object sender, EventArgs e)
        {
            if (Especialidad.cantidadEspecialidades == 0)
            {
                MessageBox.Show("Debe crear al menos una especialidad", "Error");
                return;
            }


            Consultorios consultorio = new Consultorios();

            string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad", "Asignar especialidad");
            Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
            consultorio.AgregarEspecialidad(esp);

            MessageBox.Show("Consultorio creado con ID: " + consultorio.id_consultorio, "Éxito");
            int contador = Consultorios.cantidadConsultorios - 1;

            Button cuadro = new Button();
            cuadro.Size = new Size(100, 50);
            cuadro.Text = "" + consultorio.id_consultorio;
            cuadro.BackColor = Color.Green;
            cuadro.ForeColor = Color.White;
            cuadro.Location = new Point(20 + (contador * 120), 40);
            this.Controls.Add(cuadro);
            this.cuadros.Add(cuadro);

            cuadro.MouseDown += new MouseEventHandler(this.Cuadro_MouseClick);
        }

        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string nombre = Prompt.ShowDialog("Ingrese el nombre", "Agregar especialidad");

            Especialidad especialidad = new Especialidad(nombre);

            int tiempoAtencion = int.Parse(Prompt.ShowDialog("Ingrese el tiempo de atencion de la especialidad (minutos)", "Agregar especialidad"));

            especialidad.setTiempoAtencion(tiempoAtencion);

            MessageBox.Show("Especialidad creada: " + especialidad.nombre +
                              " con tiempo de atención: " + especialidad.getTiempoAtencion() + " minutos.", "Éxito");
        }

        private void especialidadesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void AgregarPaciente_Click(object sender, EventArgs e)
        {
            string nombre = Prompt.ShowDialog("Ingrese el nombre", "Agregar paciente");

            string genero = Prompt.ShowElegirGenero("Seleccione su genero", "Seleccionar genero");

            Pacientes paciente = new Pacientes(nombre, genero);
            MessageBox.Show("Paciente creado: " + paciente.nombre, "Éxito");
        }

        private void VerPacientes_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            foreach (Pacientes paci in Pacientes.getListaPacientes())
            {
                mensaje += "• " + paci.nombre + "\n";
            }
            MessageBox.Show(mensaje, "Lista de pacientes");
        }

        private void EliminarConsultorio_Click(object sender, EventArgs e)
        {
            int idConsultorio = Prompt.ShowEliminarConsultorio("Seleccione el consultorio que desea eliminar", "Eliminar consultorio");
            Consultorios.EliminarConsultorio(idConsultorio);

            Button botonAEliminar = null;
            Control[] controles = new Control[this.Controls.Count];
            this.Controls.CopyTo(controles, 0);

            foreach (Control ctrl in controles)
            {
                if (ctrl is Button btn && btn.Text == idConsultorio.ToString())
                {
                    botonAEliminar = btn;
                    break;
                }
            }

            if (botonAEliminar != null)
            {
                botonAEliminar.Visible = false;  // Ocultar sin modificar la colección mientras iteras
            }
        }
    }
}