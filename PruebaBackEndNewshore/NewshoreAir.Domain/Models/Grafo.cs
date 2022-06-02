using System;

namespace NewshoreAir.Domain.Models
{
    public class Grafo
    {
        private int[,] Adyacencia;
        private int[] indegree;
        private int nodos;

        public Grafo(int Nodos)
        {
            nodos = Nodos;
            //Instanciamos matriz de adyacencia
            Adyacencia = new int[nodos, nodos];
            //Instanciamos arreglo de indegree
            indegree = new int[nodos];
        }

        public void AdicionaArista(int NodoInicio, int NodoFinal)
        {
            Adyacencia[NodoInicio, NodoFinal] = 1;
        }

        public void AdicionaArista(int NodoInicio, int NodoFinal, int Peso)
        {
            Adyacencia[NodoInicio, NodoFinal] = Peso;
        }

        public void MuestraAdyacencia()
        {
            int n = 0;
            int m = 0;

            for (n = 0; n < nodos; n++)
            {
                Console.Write("\t{0}", n);
            }
            Console.WriteLine();

            for (n = 0; n < nodos; n++)
            {
                Console.Write(n);
                for (m = 0; m < nodos; m++)
                {
                    Console.Write("\t{0}", Adyacencia[n, m]);
                }
                Console.WriteLine();
            }
        }

        public int ObtenAdyacencia(int Fila, int Columna)
        {
            return Adyacencia[Fila, Columna];
        }

        public void CalcularIndegree()
        {
            int n = 0;
            int m = 0;

            for (n = 0; n < nodos; n++)
            {
                if (Adyacencia[m, n] == 1)
                    indegree[n]++;
            }
        }

        public void MostrarIndegree()
        {
            int n = 0;
            for (n = 0; n < nodos; n++)
            {
                Console.WriteLine("Nodo: {0}, {1}", n, indegree[n]);
            }
        }

        public int EncuentraIO()
        {

            bool encontrado = false;
            int n = 0;
            for (n = 0; n < nodos; n++)
            {
                if (indegree[n] == 0)
                {
                    encontrado = true;
                    break;
                }
            }

            if (encontrado)
                return n;
            else return -1;//Codigo que indica que no se encontró

        }

        public void DecrementaIndigree(int Nodo)
        {
            indegree[Nodo] = -1;
            int n = 0;
            for (n = 0; n < nodos; n++)
            {
                if (Adyacencia[Nodo, n] == 1)
                    indegree[n]--;
            }
        }
    }
}
