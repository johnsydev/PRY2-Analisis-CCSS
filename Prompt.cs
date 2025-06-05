using System;
using System.Collections;
using System.Windows.Forms;

namespace PRY2_Analisis_CCSS
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() {
                Left = 50,
                Top = 20,
                Width = 400,
                Height = 40,
                AutoSize = false,
                Text = text
            };
            TextBox textBox = new TextBox() {
                Left = 50,
                Top = 50,
                Width = 400
            };
            Button confirmation = new Button() {
                Text = "Ok",
                Left = 350,
                Width = 100,
                Top = 70,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => {
                prompt.Close();
            };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return textBox.Text;
            }
            else
            {
                return "";
            }

        }

        public static string ShowEspecialidades(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Width = 400,
                Height = 40,
                AutoSize = false,
                Text = text
            };

            ComboBox comboBox1 = new ComboBox()
            {
                Left = 50,
                Top = 70,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            ArrayList especialidades = Especialidad.getEspecialidades();
            foreach (Especialidad esp in especialidades)
            {
                comboBox1.Items.Add(esp.nombre);
            }

            comboBox1.SelectedIndex = 0; 
            Button confirmation = new Button()
            {
                Text = "Ok",
                Left = 370,
                Width = 75,
                Top = 70,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(comboBox1);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;


            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return comboBox1.SelectedItem.ToString();
            }
            else
            {
                return "";
            }

        }

        public static int ShowEliminarConsultorio(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Width = 400,
                Height = 40,
                AutoSize = false,
                Text = text
            };

            ComboBox comboBox1 = new ComboBox()
            {
                Left = 50,
                Top = 70,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            ArrayList consultorios = Consultorios.getConsultorios();
            foreach (Consultorios cons in consultorios)
            {
                comboBox1.Items.Add("" + cons.id_consultorio);
            }

            comboBox1.SelectedIndex = 0;
            Button confirmation = new Button()
            {
                Text = "Ok",
                Left = 370,
                Width = 75,
                Top = 70,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(comboBox1);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;


            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return int.Parse(comboBox1.SelectedItem.ToString());
            }
            else
            {
                return -1;
            }

        }

        public static string ShowElegirGenero(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label()
            {
                Left = 50,
                Top = 20,
                Width = 400,
                Height = 40,
                AutoSize = false,
                Text = text
            };

            ComboBox comboBox1 = new ComboBox()
            {
                Left = 50,
                Top = 70,
                Width = 300,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            
            comboBox1.Items.Add("Mujer");
            comboBox1.Items.Add("Hombre");


            comboBox1.SelectedIndex = 0;
            Button confirmation = new Button()
            {
                Text = "Ok",
                Left = 370,
                Width = 75,
                Top = 70,
                DialogResult = DialogResult.OK
            };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(comboBox1);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;


            if (prompt.ShowDialog() == DialogResult.OK)
            {
                return comboBox1.SelectedItem.ToString();
            }
            else
            {
                return "";
            }

        }

    };
};