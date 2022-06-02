using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewshoreAir.Domain.Models
{
    public class Grafo22
    {
        static void Main(string[] args)
        {
            int incio = 0;
            int final = 0;
            int distancia = 0;
            int n = 0;
            int cantNodos = 7;
            string dato = "";
            int actual = 0;
            int columna = 0;

            Grafo miGrafo = new Grafo(cantNodos);

            miGrafo.AdicionaArista(0, 1, 2);
            miGrafo.AdicionaArista(0, 3, 1);
            miGrafo.AdicionaArista(1, 3, 3);
            miGrafo.AdicionaArista(0, 1, 2);
            miGrafo.AdicionaArista(0, 1, 2);
            miGrafo.AdicionaArista(0, 1, 2);
            miGrafo.AdicionaArista(0, 1, 2);

            miGrafo.MuestraAdyacencia();
        }

    }
}
