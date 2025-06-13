using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    public partial class Form1 : Form
    {
        ArrayList cuadros = new ArrayList();
        private DateTime horaVirtualActual;
        public static Form instancia;

        public Form1()
        {
            InitializeComponent();
            instancia = this;
        }

        public static Form GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Form1();
            }
            return Form1.instancia;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Renderer = new CustomMenuRenderer();
            IniciarReloj();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }
        private async void IniciarReloj()
        {
            horaVirtualActual = DateTime.Today.AddHours(7); 

            while (horaVirtualActual < DateTime.Today.AddHours(17)) 
            {
                lblHora.Text = horaVirtualActual.ToString("HH:mm");
                await Task.Delay(300); 
                horaVirtualActual = horaVirtualActual.AddMinutes(1);
            }

            lblHora.Text = "Fin de la jornada laboral";
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
                    Principal.ActualizarColas();

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
                        string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
                        rutaBase = Path.GetFullPath(rutaBase);
                        string rutaImagen = Path.Combine(rutaBase, "consultorioA.png");
                        Image escalada = Image.FromFile(rutaImagen).GetThumbnailImage(btn.Width, btn.Height, null, IntPtr.Zero);
                        btn.Image = escalada;
                        MessageBox.Show("Consultorio abierto: " + cons.id_consultorio, "Éxito");

                    });
                }
                else
                {
                    menu.Items.Add("Cerrar consultorio", null, (s, ev) =>
                    {
                        cons.CerrarConsultorio();
                        string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
                        rutaBase = Path.GetFullPath(rutaBase);
                        string rutaImagen = Path.Combine(rutaBase, "consultorioC.png");
                        Image escalada = Image.FromFile(rutaImagen).GetThumbnailImage(btn.Width, btn.Height, null, IntPtr.Zero);
                        btn.Image = escalada;
                        MessageBox.Show("Consultorio cerrado: " + cons.id_consultorio, "Éxito");
                        Principal.ActualizarColas();

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

            string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
            rutaBase = Path.GetFullPath(rutaBase);
            string rutaImagen = Path.Combine(rutaBase, "consultorioA.png");

            Button cuadro = new Button();
            cuadro.Size = new Size(80, 120);
            cuadro.Text = "" + consultorio.id_consultorio;
            Image escalada = Image.FromFile(rutaImagen).GetThumbnailImage(cuadro.Width, cuadro.Height, null, IntPtr.Zero);
            cuadro.Image = escalada;
            cuadro.TextAlign = ContentAlignment.MiddleRight;

            cuadro.Location = new Point(20 + (contador * 120), 40);
            this.Controls.Add(cuadro);
            this.cuadros.Add(cuadro);

            cuadro.MouseDown += new MouseEventHandler(this.Cuadro_MouseClick);

            //Agregar la cola
            Panel panelCola = new Panel();
            panelCola.Size = new Size(cuadro.Width, 200);
            panelCola.Location = new Point(cuadro.Left, cuadro.Bottom + 5);
            panelCola.BorderStyle = BorderStyle.FixedSingle;
            panelCola.AutoScroll = true;
            this.Controls.Add(panelCola);
            consultorio.panelCola = panelCola;

        }

        public static void AgregarVisualPacienteACola(Panel panelCola, string nombre, string rutaImagen)
        {
            Panel item = new Panel
            {
                Height = 50,
                Width = Math.Max(panelCola.Width - 20, 150),
                Location = new Point(0, panelCola.Controls.Count * 50),
                Padding = new Padding(5),
                BackColor = Color.Transparent
            };

            PictureBox pic = new PictureBox
            {
                Image = Image.FromFile(rutaImagen),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(28, 28),
                Location = new Point(5, 10)
            };

            Label lbl = new Label
            {
                Text = nombre,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 9, FontStyle.Regular),
                Location = new Point(pic.Right + 8, 15),
                ForeColor = Color.Black,
            };

            item.Width = pic.Right + 10 + lbl.PreferredWidth + 10;

            item.Controls.Add(pic);
            item.Controls.Add(lbl);

            panelCola.Controls.Add(item);

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

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(genero))
            {
                MessageBox.Show("Debe ingresar un nombre y seleccionar un género.", "Error");
                return;
            }

            Pacientes paciente = new Pacientes(nombre, genero);

            DateTime tiempo = horaVirtualActual;
            string horaStr = tiempo.ToString("HH:mm");
            paciente.setHoraLlegada(horaStr);

            string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
            rutaBase = Path.GetFullPath(rutaBase);

            List<string> imagenesHombres = new List<string>()
            {
                Path.Combine(rutaBase, "hombre1.png"),
                Path.Combine(rutaBase, "hombre2.png"),
                Path.Combine(rutaBase, "hombre3.png"),
                Path.Combine(rutaBase, "hombre4.png"),
                Path.Combine(rutaBase, "hombre5.png"),
            };

            List<string> imagenesMujeres = new List<string>()
            {
                Path.Combine(rutaBase, "mujer1.png"),
                Path.Combine(rutaBase, "mujer2.png"),
                Path.Combine(rutaBase, "mujer3.png"),
                Path.Combine(rutaBase, "mujer4.png"),
                Path.Combine(rutaBase, "mujer5.png"),
            };

            Random random = new Random();
            if (genero == "Mujer")
            {
                paciente.setImagen(imagenesMujeres[random.Next(imagenesMujeres.Count)]);
            }
            else
            {
                paciente.setImagen(imagenesHombres[random.Next(imagenesHombres.Count)]);
            }

            string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad que necesita", "Seleccionar especialidad");
            Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
            Tiquete tiquete = new Tiquete(esp, paciente);
            paciente.asignarTiquete(tiquete);

            MessageBox.Show("Paciente creado: " + paciente.nombre + ", Hora de llegada: " + paciente.horaLlegada, "Éxito");

            Principal.ActualizarEspera();
        }


        public static void ActualizarEsperaVisual(ArrayList listaPacientes)
        {
            Form1 form = (Form1)GetInstancia();
            form.PanelEspera.Controls.Clear();
            int x = 10;
            foreach (Pacientes p in listaPacientes)
            {
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(p.imagen);
                pic.SizeMode = PictureBoxSizeMode.StretchImage;
                pic.Size = new Size(50, 50);
                pic.Location = new Point(x, 20);
                form.PanelEspera.Controls.Add(pic);

                Label labelNombre = new Label();
                labelNombre.Text = p.nombre;
                labelNombre.AutoSize = false;
                labelNombre.TextAlign = ContentAlignment.TopCenter;
                labelNombre.Size = new Size(50, 20);
                labelNombre.Location = new Point(x, 75);
                form.PanelEspera.Controls.Add(labelNombre);

                x += 80;
            }
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panelTiempo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void botonRepartir_Click(object sender, EventArgs e)
        {
            Principal.ActualizarColas();
        }
    }
}