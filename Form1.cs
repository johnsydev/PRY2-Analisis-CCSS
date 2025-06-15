using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace PRY2_Analisis_CCSS
{
    public partial class Form1 : Form
    {
        ArrayList cuadros = new ArrayList();
        public static DateTime horaVirtualActual;
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

            while (true) //(horaVirtualActual < DateTime.Today.AddHours(17)) 
            {
                lblHora.Text = horaVirtualActual.ToString("HH:mm");
                await Task.Delay(300);     
                if (Principal.horasParada.Contains(horaVirtualActual.ToString("HH:mm")))
                {
                    Principal.Atender(horaVirtualActual);
                }
                horaVirtualActual = horaVirtualActual.AddMinutes(1);
            }

            lblHora.Text = "Fin de la jornada laboral";
        }

        public static DateTime GetHoraVirtualActual()
        {
            return horaVirtualActual;
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

            int nuevoID = Consultorios.ObtenerPrimerIDDisponible();
            Consultorios consultorio = new Consultorios(nuevoID);

            string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad", "Asignar especialidad");
            Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
            consultorio.AgregarEspecialidad(esp);

            MessageBox.Show("Consultorio creado con ID: " + consultorio.id_consultorio, "Éxito");
            int contador = Consultorios.cantidadConsultorios - 1;

            string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
            rutaBase = Path.GetFullPath(rutaBase);
            string rutaImagen = Path.Combine(rutaBase, "consultorioA.png");

            Label labelTiempo = new Label();
            labelTiempo.Size = new Size(80, 20);
            labelTiempo.Text = "" + consultorio.getTotalTiempoAtencion();

            Button cuadro = new Button();
            cuadro.Size = new Size(80, 120);
            cuadro.Text = "" + consultorio.id_consultorio;
            Image escalada = Image.FromFile(rutaImagen).GetThumbnailImage(cuadro.Width, cuadro.Height, null, IntPtr.Zero);
            cuadro.Image = escalada;
            cuadro.TextAlign = ContentAlignment.MiddleRight;

            int espacio = 20;
            HashSet<int> ocupados = new HashSet<int>();
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn && btn.Image != null && int.TryParse(btn.Text, out _))
                {
                    ocupados.Add(btn.Left);
                }
            }

            while (ocupados.Contains(espacio))
            {
                espacio += 120;
            }

            labelTiempo.Location = new Point(espacio, 20);
            this.Controls.Add(labelTiempo);
            consultorio.labelTiempo = labelTiempo;

            // Asignar posición encontrada
            cuadro.Location = new Point(espacio, 40); 
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

        public static void AgregarVisualPacienteACola(Panel panelCola, string nombre, string rutaImagen, Tiquete tiquete)
        {
            Color color = Color.Transparent;
            Color fore = Color.Black;
            if (panelCola != null && tiquete.estaAtendido)
            {
                color = ColorTranslator.FromHtml("#005187");
                fore = Color.White;
            }

            Panel item = new Panel
            {
                Height = 50,
                Width = Math.Max(panelCola.Width - 20, 150),
                Location = new Point(0, panelCola.Controls.Count * 50),
                Padding = new Padding(5),
                BackColor = color,
                ForeColor = fore
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
                ForeColor = fore,
            };

            item.Width = pic.Right + 10 + lbl.PreferredWidth + 10;

            item.Controls.Add(pic);
            item.Controls.Add(lbl);

            panelCola.Controls.Add(item);

        }



        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string nombre = Prompt.ShowDialog("Ingrese el nombre", "Agregar especialidad");

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("Debe escribir un nombre para el paciente", "Error");
                return;
            }

            if (Especialidad.buscarEspecialidadPorNombre(nombre) != null)
            {
                MessageBox.Show("La especialidad ya existe", "Error");
                return;
            }
       
            int tiempoAtencion = int.Parse(Prompt.ShowDialog("Ingrese el tiempo de atencion de la especialidad (minutos)", "Agregar especialidad"));

            Especialidad especialidad = new Especialidad(nombre);
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

        private static void Paciente_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenuStrip menu = new ContextMenuStrip();
                //Button btn = sender as Button;
                //Pacientes cons = Pacientes.buscarPaciente(int.Parse(btn.Text));

                PictureBox pic = sender as PictureBox;
                Pacientes paciente = Pacientes.buscarPaciente((int)pic.Tag);

                menu.Items.Add("Asignar especialidad adicional", null, (s, ev) =>
                {
                    string nombreEspecialidad = Prompt.ShowEspecialidades("Seleccione la especialidad adicional que necesita", "Asignar especialidad");
                    Especialidad esp = Especialidad.buscarEspecialidadPorNombre(nombreEspecialidad);
                    paciente.setEspecialidad(esp);
                    Tiquete tiquete = new Tiquete(esp, paciente);
                    paciente.asignarTiquete(tiquete);

                });
                menu.Items.Add("Ver especialidades", null, (s, ev) =>
                {
                    string mensaje = "Especialidades de " + paciente.nombre + ":\n";
                    foreach (Tiquete tiq in paciente.tiquetes)
                    {
                        mensaje += "• " + tiq.especialidad.nombre + "\n";
                    }
                    if (paciente.tiquetes == null || paciente.tiquetes.Count <= 0)
                    {
                        mensaje = "No tiene especialidades asignadas";
                    }
                    MessageBox.Show(mensaje, "Lista de especialidades:");

                });

                menu.Show(Cursor.Position);
            }
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
                pic.Tag = p.id_paciente;
                form.PanelEspera.Controls.Add(pic);

                Label labelNombre = new Label();
                labelNombre.Text = p.nombre;
                labelNombre.AutoSize = false;
                labelNombre.TextAlign = ContentAlignment.TopCenter;
                labelNombre.Size = new Size(50, 20);
                labelNombre.Location = new Point(x, 75);
                form.PanelEspera.Controls.Add(labelNombre);

                pic.MouseDown += new MouseEventHandler(Form1.Paciente_MouseClick);

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

            Consultorios cons = Consultorios.buscarConsultorio(idConsultorio);

            if (cons != null)
            {
                if (cons.panelCola != null && this.Controls.Contains(cons.panelCola))
                {
                    this.Controls.Remove(cons.panelCola);
                    cons.panelCola.Dispose();
                }
                Control[] controles = new Control[this.Controls.Count];
                this.Controls.CopyTo(controles, 0);

                foreach (Control ctrl in controles)
                {
                    if (ctrl is Button btn && btn.Text.Contains(idConsultorio.ToString()))
                    {
                        this.Controls.Remove(btn);
                        btn.Dispose();
                        break;
                    }
                }

                Consultorios.EliminarConsultorio(idConsultorio);

                MessageBox.Show($"Consultorio {idConsultorio} y su panel fueron eliminados.", "Éxito");
            }
            else
            {
                MessageBox.Show("No se encontró el consultorio con ese ID.", "Error");
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
            Principal.AgregarHoraParada(horaVirtualActual.AddMinutes(1));
            Principal.ActualizarColas();
        }

        public class ModeloXML
        {
            public List<Pacientes> listaPacientes = new List<Pacientes>();
            public List<Especialidad> listaEspecialidades = new List<Especialidad>();
            public List<Tiquete> listaTiquetes = new List<Tiquete>();

            public ModeloXML() { }
        }

        private void cargarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos XML (*.xml)|*.xml";
            openFileDialog.Title = "Cargar datos desde archivo";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string rutaArchivo = openFileDialog.FileName;
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ModeloXML));

                    StreamReader sr = new StreamReader(rutaArchivo);

                    ModeloXML datos = serializer.Deserialize(sr) as ModeloXML;

                    string rutaBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\resources");
                    rutaBase = Path.GetFullPath(rutaBase);

                    if (datos != null)
                    {
                        string msg = "Datos cargados correctamente:\n";
                        int pCount = Pacientes.listaPacientes.Count;

                        int baseEspecialidades = Especialidad.cantidadEspecialidades;
                        int basePacientes = Pacientes.cantidadPacientes;
                        int baseTiquetes = Tiquete.cantidadTiquetes;

                        foreach (Especialidad especialidad in datos.listaEspecialidades)
                        {
                            especialidad.id_especialidad += baseEspecialidades;
                            Especialidad esp = new Especialidad(especialidad.nombre);
                            esp.setTiempoAtencion(especialidad.tiempoAtendido);
                        }

                        msg += $"Se cargaron {datos.listaEspecialidades.Count} especialidades.\n";

                        foreach (Pacientes paciente in datos.listaPacientes)
                        {
                            paciente.id_paciente += basePacientes;
                            
                            string rutaImagen = Path.Combine(rutaBase, paciente.imagen);

                            Pacientes pac = new Pacientes(paciente.nombre, paciente.genero);
                            pac.setHoraLlegada(paciente.horaLlegada);
                            pac.setImagen(rutaImagen);
                            DateTime tiempo = horaVirtualActual;
                            string horaStr = tiempo.ToString("HH:mm");
                            pac.setHoraLlegada(horaStr);

                            foreach (Especialidad especialidad in paciente.getEspecialidades())
                            {
                                Especialidad esp = Especialidad.buscarEspecialidad(especialidad.id_especialidad+baseEspecialidades);
                                if (esp != null)
                                {
                                    pac.setEspecialidad(esp);
                                }
                            }
                        }

                        msg += $"Se cargaron {datos.listaPacientes.Count} pacientes.\n";

                        foreach (Tiquete tiquete in datos.listaTiquetes)
                        {
                            tiquete.id_tiquete += baseTiquetes;
                            tiquete.paciente.id_paciente += basePacientes;
                            tiquete.especialidad.id_especialidad += baseEspecialidades;
                            int idPaciente = tiquete.paciente.id_paciente;
                            Pacientes pacienteInvolucrado = Pacientes.buscarPaciente(idPaciente);

                            Especialidad esp = Especialidad.buscarEspecialidad(tiquete.especialidad.id_especialidad);
                            Tiquete tiq = new Tiquete(esp, pacienteInvolucrado);
                            tiq.prioridad = tiquete.prioridad;
                            pacienteInvolucrado.asignarTiquete(tiq);

                        }

                        msg += $"Se cargaron {datos.listaTiquetes.Count} tiquetes.";

                        Principal.ActualizarEspera();

                        MessageBox.Show(msg, "Éxito");
                    }
                    else
                    {
                        MessageBox.Show("No se pudieron cargar los datos correctamente, verifique que el formato sea el correcto.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudieron cargar los datos correctamente, verifique que el formato sea el correcto.", "Error");
                    MessageBox.Show(ex.Message, "Error Detalle");
                }
            }
        }
    }
}