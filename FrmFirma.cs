using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PryLedoEtapa1
{
    public partial class FrmFirma : Form
    {
        public FrmFirma()
        {
            InitializeComponent();
        }

        // Variables para el dibujo
        private bool dibujando = false;
        private Point posicionAnterior;
        private Bitmap firmaGuardada;
        private Bitmap firmaBitmap;

        private void FrmFirma_Load(object sender, EventArgs e)
        {
            // Crea un Bitmap para almacenar la firma
            firmaBitmap = new Bitmap(pbFirma.Width, pbFirma.Height);
            pbFirma.Image = firmaBitmap;

            // Inicializa los eventos del mouse
            pbFirma.MouseDown += pbFirma_MouseDown;
            pbFirma.MouseMove += pbFirma_MouseMove;
            pbFirma.MouseUp += pbFirma_MouseUp;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string carpetaImagenesFirmas = Path.Combine(Application.StartupPath, "Firmas");

            if (!Directory.Exists(carpetaImagenesFirmas))
            {
                Directory.CreateDirectory(carpetaImagenesFirmas);
            }

            string nombreArchivo = $"firma_{DateTime.Now.ToString("yyyy-MM-dd-HH,mm,ss")}.png";

            string rutaArchivo = Path.Combine(carpetaImagenesFirmas, nombreArchivo);

            firmaBitmap.Save(rutaArchivo, ImageFormat.Png);

            MessageBox.Show("¡Firma guardada con éxito!");
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            firmaBitmap = new Bitmap(pbFirma.Width, pbFirma.Height);
            pbFirma.Image = firmaBitmap;
        }

        private void pbFirma_MouseDown(object sender, MouseEventArgs e)
        {
            // Comenzar el dibujo cuando se presione el botón izquierdo del mouse
            if (e.Button == MouseButtons.Left)
            {
                dibujando = true;
                posicionAnterior = e.Location;
            }
        }

        private void pbFirma_MouseMove(object sender, MouseEventArgs e)
        {
            // Dibujar cuando se mueva el mouse mientras se mantiene presionado el botón izquierdo
            if (dibujando)
            {
                using (Graphics g = Graphics.FromImage(firmaBitmap))
                {
                    // Dibujar una línea desde la posición anterior a la posición actual
                    Pen pen = new Pen(Color.Black, 2); // Puedes ajustar el color y el grosor aquí
                    g.DrawLine(pen, posicionAnterior, e.Location);
                }
                // Actualizar la posición anterior con la nueva posición
                posicionAnterior = e.Location;
                pbFirma.Invalidate(); // Vuelve a dibujar el PictureBox
            }
        }

        private void pbFirma_MouseUp(object sender, MouseEventArgs e)
        {
            // Terminar el dibujo cuando se suelte el botón izquierdo del mouse
            if (e.Button == MouseButtons.Left)
            {
                dibujando = false;
            }
        }


    }
}