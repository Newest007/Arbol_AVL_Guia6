using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace Arbol_AVL
{
    class DibujaAVL
    {
        public AVL Raiz;
        public AVL aux;

        //Constructor
        public DibujaAVL()
        {
            aux = new AVL();
        }

        public DibujaAVL(AVL RaizNueva)
        {
            Raiz = RaizNueva;
        }

        //Para agregar un nuevo valor al árbol
        public void Insertar(int dato)
        {
            if (Raiz == null)
                Raiz = new AVL(dato, null, null, null);
            else
                Raiz = Raiz.Insertar(dato, Raiz);

        }

        //Para eliminar un valor del árbol
        public void Eliminar(int dato)
        {
            if (Raiz == null)
                Raiz = new AVL(dato, null, null, null);
            else
                Raiz.Eliminar(dato, ref Raiz);
        }

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodoRecorrido(ref int xmin, ref int ymin)
        {
            CoordenadaY = (int)(ymin + Radio / 2);
            CoordenadaX = (int)(xmin + Radio / 2);
            xmin += Radio;
        }


        //==============================================================================//
        //             Función para colorear los recorridos según sea el caso           //
        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, AVL Raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;

            if (inor == true)
            {
                if(Raiz != null)
                {
                    colorear(grafo, fuente, Brushes.Black, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);

                }


            }

            else if(preor == true)
            {
                if(Raiz != null)
                {
                    Raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Black, Pens.Black);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, Brushes.White, Brushes.Black, Pens.Black);
                    colorear(grafo, fuente, Brushes.Blue, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);

                }

            }

            else if (post == true)
            {
                if(Raiz != null)
                {
                    colorear(grafo, fuente, Brushes.Black, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);

                }

            }


        }


        //=================================================================//
        //      Función para colorear el nodo que se esta buscando         //
        public void colorearBuscar(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, AVL Raiz, int busqueda)
        {
            Brush entorno = Brushes.Red;
            if(Raiz != null)
            {
                Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);

                if(busqueda < Raiz.valor)
                {
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, entorno, Brushes.Black, Lapiz);
                    colorearBuscar(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, busqueda);
                }
                else
                {
                    if(busqueda > Raiz.valor)
                    {
                        Thread.Sleep(500);
                        Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                        colorearBuscar(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, busqueda);
                    }
                    else
                    {
                        Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                        Thread.Sleep(500);
                    }
                }
            }
        }

        //=================================================================//
        //                     Para dibujar el árbol                       //
        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, int dato, Brush encuentro)
        {
            int x = 100;
            int y = 75;
            if (Raiz == null) return;

            //Posición de todos los nodos
            Raiz.PosicionNodo(ref x, y);

            //Para dibujar los enlaces entre nodos
            Raiz.DibujarRamas(grafo, Lapiz);

            //Para dibujar todos los nodos
            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, dato, encuentro);
        
        }

        //=================================================================//
        //               Para restablecer valores                          // 
        public int x1 = 100;
        public int y2 = 75;
        public void restablecer_Valores()
        {
            x1 = 100;
            y2 = 75;
        }
        //=================================================================//
        //                      Para buscar                                //
        public void buscar(int x)
        {
            if (Raiz == null)
                MessageBox.Show("El Árbol AVL esta vacío, empieza añadiendo datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            else
                Raiz.buscar(x, Raiz);
        }

    }
}
