using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arbol_AVL
{
    public partial class FormAVL : Form
    {
        public FormAVL()
        {
            InitializeComponent();
        }

        int cont = 0;
        int dato = 0;
        int datb = 0;
        int cont2 = 0;
        int pintaR = 0;

        DibujaAVL arbolAVL = new DibujaAVL(null);
        DibujaAVL arbolAVL_Letra = new DibujaAVL(null);
        AVL ClaseAVL = new AVL();

        Graphics g;

        private void FormAVL_Load(object sender, EventArgs e)
        {

        }

        private void FormAVL_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            arbolAVL.DibujarArbol(g, this.Font, Brushes.White, Brushes.Black, Pens.White, datb, Brushes.Black);
            datb = 0;

            if(pintaR==1)
            {
                arbolAVL.colorear(g, this.Font, Brushes.Black, Brushes.Yellow, Pens.Blue, arbolAVL.Raiz, rbtnPostOrden.Checked, rbtnEnOrden.Checked, rbtnPreOrden.Checked);
                pintaR = 0;
            }

            if(pintaR == 2)
            {
                arbolAVL.colorearBuscar(g, this.Font, Brushes.White, Brushes.Red, Pens.White, arbolAVL.Raiz, int.Parse(txtValor.Text));
                pintaR = 0;
            }


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //PreOrden
            if(rbtnPreOrden.Checked) 
            {

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
