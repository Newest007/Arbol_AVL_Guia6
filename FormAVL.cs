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

        bool post, preor, inor;
        

        DibujaAVL arbolAVL = new DibujaAVL(null);
        DibujaAVL arbolAVL_Letra = new DibujaAVL(null);
        AVL NodosAVL = new AVL();

        Graphics g;

        private void FormAVL_Load(object sender, EventArgs e)
        {
            post = inor = preor = false;

        }

        private void FormAVL_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;

            arbolAVL.DibujarArbol(g, this.Font, Brushes.White, Brushes.Black, Pens.White, datb, Brushes.White);
            
            datb = 0;

            if(pintaR==1)
            {
                arbolAVL.colorear(g, this.Font, Brushes.Black, Brushes.Yellow, Pens.Black, arbolAVL.Raiz, rbtnPostOrden.Checked, rbtnEnOrden.Checked, rbtnPreOrden.Checked);
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
            //InOrden
            if(rbtnEnOrden.Checked)
            {
                inor = true;
                pintaR = 1;
                NodosAVL.listaInorden.Clear();
                lstBox.Items.Clear();

                NodosAVL.Inorden(arbolAVL.Raiz);

                foreach (var valores in NodosAVL.listaInorden)
                {
                   lstBox.Items.Add(valores);
                }

                Refresh();
                
            }


            //PreOrden
            if(rbtnPreOrden.Checked) 
            {
                preor = true;
                pintaR = 1;
                //pintaR = 3;
                //timer1.Start();
                //arbolAVL.colorear(g, this.Font, Brushes.Aqua, Brushes.Black, Pens.Black, arbolAVL.Raiz, post, inor,preor);
                NodosAVL.listaPreorden.Clear();
                lstBox.Items.Clear();

                NodosAVL.Preorden(arbolAVL.Raiz);
                foreach (var valores in NodosAVL.listaPreorden)
                {
                    lstBox.Items.Add(valores);
                }
                Refresh();
            }

            //PostOrden
            if(rbtnPostOrden.Checked)
            {
                post = true;
                pintaR = 1;
                NodosAVL.listaPostorden.Clear();
                lstBox.Items.Clear();

                NodosAVL.Postorden(arbolAVL.Raiz);
                foreach(var valores in NodosAVL.listaPostorden)
                {
                    lstBox.Items.Add(valores);
                }
                Refresh();
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Valor Obligatorio");
            }
            else
            {
                try
                {
                    dato = int.Parse(txtValor.Text);
                    arbolAVL.Insertar(dato);
                    txtValor.Clear();
                    txtValor.Focus();
                    lblAltura.Text = arbolAVL.Raiz.getAltura(arbolAVL.Raiz).ToString();
                    cont++;
                    Refresh();
                    Refresh();

                }
                catch(Exception ex)
                {
                    errorProvider1.SetError(txtValor, "Debe ser un valor numérico");
                }

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            errorProvider1.Clear();
            if(txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Valor Obligatorio");
            }
            else
            {
                try
                {
                    datb = int.Parse(txtValor.Text);
                    arbolAVL.buscar(datb);
                    pintaR = 2;
                    Refresh();
                    txtValor.Clear();
                }
                catch(Exception ex)
                {
                    errorProvider1.SetError(txtValor, "Debe ser un valor numérico");
                }

            }


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if(txtValor.Text == "")
            {
                errorProvider1.SetError(txtValor, "Valor Obligatorio");
            }
            else
            {
                try
                {
                    dato = int.Parse(txtValor.Text);
                    txtValor.Clear();
                    arbolAVL.Eliminar(dato);
                    lblAltura.Text = arbolAVL.Raiz.getAltura(arbolAVL.Raiz).ToString();
                    Refresh();
                    Refresh();
                    cont2++;
                }
                catch(Exception ex)
                {
                    errorProvider1.SetError(txtValor, "Debe de ser un valor numérico");
                }
                

            }

            Refresh(); Refresh(); Refresh();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
