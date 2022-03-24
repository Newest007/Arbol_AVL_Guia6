using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbol_AVL
{
    class AVL
    {
        public int valor;
        public AVL NodoIzquierdo;
        public AVL NodoDerecho;
        public AVL NodoPadre;
        public int altura;
        //public Rectangle prueba;
        public DibujaAVL arbol;

        public AVL() { }

        public DibujaAVL Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }

        //Constructor predeterminado
        public AVL(int valorNuevo, AVL izquiero, AVL derecho, AVL padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquiero;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }

        //======================================================//
        // Función para insertar un nuevo valor en el árbol AVL //
        //======================================================//
        public AVL Insertar( int valorNuevo, AVL Raiz)
        {
            if (Raiz == null)
                Raiz = new AVL(valorNuevo, null, null, null);
            else if(valorNuevo < Raiz.valor)
            {
                Raiz.NodoIzquierdo = Insertar(valorNuevo, Raiz.NodoIzquierdo);
            }

            else if(valorNuevo>Raiz.valor)
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho);
            }

            else
            {
                System.Windows.Forms.MessageBox.Show("Rey ese valor ya existe en el árbol, prueba con otro","Error",System.Windows.Forms.MessageBoxButtons.OK);
            }

            //Para realizar las rotaciones simples o dobles según el caso
            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if (valorNuevo < Raiz.NodoIzquierdo.valor)
                    Raiz = RotacionIzquierdaSimple(Raiz);
                else
                    Raiz = RotacionIzquierdaDoble(Raiz);

            }

            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo > Raiz.NodoDerecho.valor)
                    Raiz = RotacionDerechaSimple(Raiz);
                else
                    Raiz = RotacionDerechaDoble(Raiz);

            }

            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;
            return Raiz;
        }

        //Función para determinar que rama es mayor
        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }

        //Función para determinar la altura del árbol
        private static int Alturas(AVL Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }

        //=========================================================//
        //       Secciones para las funciones de rotaciones        //
        //=========================================================//

        //Rotación Izquierda Simple
        private static AVL RotacionIzquierdaSimple (AVL n2)
        {
            AVL n1 = n2.NodoIzquierdo;
            n2.NodoIzquierdo = n1.NodoDerecho;
            n1.NodoDerecho = n2;
            n2.altura = max(Alturas(n2.NodoIzquierdo), Alturas(n2.NodoDerecho)) + 1;
            n1.altura = max(Alturas(n1.NodoIzquierdo), n2.altura) + 1;
            return n1;

        }

        //Rotación Derecha Simple
        private static AVL RotacionDerechaSimple(AVL n1)
        {
            AVL n2 = n1.NodoDerecho;
            n1.NodoDerecho = n2.NodoIzquierdo;
            n2.NodoIzquierdo = n1;
            n1.altura = max(Alturas(n1.NodoIzquierdo), Alturas(n1.NodoDerecho)) + 1;
            n2.altura = max(Alturas(n2.NodoDerecho), n1.altura) + 1;
            return n2;
        }

        //Doble Rotación Izquierda
        private static AVL RotacionIzquierdaDoble(AVL n3)
        {
            n3.NodoIzquierdo = RotacionDerechaSimple(n3.NodoIzquierdo);
            return RotacionDerechaSimple(n3);
        }

        //Doble Rotacion Derecha
        private static AVL RotacionDerechaDoble(AVL n1)
        {
            n1.NodoDerecho = RotacionIzquierdaSimple(n1.NodoDerecho);
            return RotacionDerechaSimple(n1);
        }

        //===============================================================//

        //Función para obtener la altura del árbol
        public int getAltura(AVL nodoActual)
        {
            if (nodoActual == null)
                return 0;
            else
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));
        }

        //           Función para eliminar un nodo                 //
        AVL nodoE, nodoP;
        public AVL Eliminar(int valorEliminar, ref AVL Raiz)
        {

            if (Raiz != null) 
            {
                if (valorEliminar < Raiz.valor)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if(valorEliminar > Raiz.valor)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        //Posicionando sobre el elemento a eliminar

                        AVL NodoEliminar = Raiz;
                        if(NodoEliminar.NodoDerecho == null)
                        {
                            Raiz = NodoEliminar.NodoIzquierdo;
                            if (Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                //System.Windows.Forms.MessageBox.Show("nodoE"+valor.ToString());
                                if (valorEliminar < nodoE.valor)
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaSimple(nodoE);
                            }

                            if (Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if (valorEliminar > nodoE.NodoDerecho.valor)
                                    nodoE = RotacionDerechaSimple(nodoE);
                                else
                                    nodoE = RotacionDerechaDoble(nodoE);
                                nodoP = RotacionDerechaSimple(nodoE);
                            }

                        }
                        else
                        {
                            if(NodoEliminar.NodoIzquierdo == null)
                            {
                                Raiz = NodoEliminar.NodoDerecho;
                            }
                            else
                            {
                                if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    AVL AuxiliarNodo = null;
                                    AVL Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }

                                    Raiz.valor = Auxiliar.valor;
                                    NodoEliminar = Auxiliar;

                                    if(Bandera == true)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                    //Realiza las rotaciones simples o dobles segun el caso
                                }

                                else
                                {

                                    if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {
                                        AVL AuxiliarNodo = null;
                                        AVL Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        
                                        while (Auxiliar.NodoIzquierdo != null) 
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }

                                        Raiz.valor = Auxiliar.valor;
                                        NodoEliminar = Auxiliar;

                                        if(Bandera == true)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }

                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }

                                    }

                                    else
                                    {
                                        if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0) 
                                        {
                                            AVL AuxiliarNodo = null;
                                            AVL Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            NodoEliminar = Auxiliar;

                                            if(Bandera == true)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }    

                                        }

                                    }

                                }


                            }

                        }



                    }

                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Ese ya existe Rey, pruebe con otro","Error",System.Windows.Forms.MessageBoxButtons.OK);
            }

            return nodoP;
        }

        //         Función para buscar un valor en el árbol      //
        public void buscar(int valorBuscar, AVL Raiz)
        {
            if (Raiz != null)
            {
                if (valorBuscar < Raiz.valor)
                {
                    buscar(valorBuscar, Raiz.NodoIzquierdo);
                }

                else
                {
                    if (valorBuscar > Raiz.valor)
                    {
                        buscar(valorBuscar, Raiz.NodoDerecho);
                    }

                }

            }

            else
                System.Windows.Forms.MessageBox.Show("Rey fijate que el valor no ha sido encontrado, proba con otro", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }




    }
}
