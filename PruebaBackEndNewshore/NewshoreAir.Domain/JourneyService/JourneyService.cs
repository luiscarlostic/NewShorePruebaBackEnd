using NewshoreAir.Domain.FlightService;
using NewshoreAir.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NewshoreAir.Domain.JourneyService
{
    public class JourneyService : IJourneyService
    {
        #region Private Fields
        private IFlightService _flightService;
        #endregion


        #region Constructor
        public JourneyService(IFlightService flightService)
        {
            _flightService = flightService;
        }
        #endregion

        #region Methods
        public Journey CalculateJourney(string origin, string destination)
        {
            var journey = new Journey();
            var route = new List<Flight>();
            var tempRoute = new List<Flight>();
            bool isCompleted = false;
            // Se obtienen todas las rutas
            var flights = _flightService.GetFlights();

            var originFlights = GetRouteFlights(origin, flights, true);
            var destinationFlights = GetRouteFlights(destination, flights, false);
            //if (destinationFlights== null)
            //{
            //    return journey;
            //}
            flights = flights.Where(x => !x.Origin.Equals(origin) && !x.arrivalStation.Equals(destination)).ToList();

            for (var flight = 0; flight < originFlights.Count; flight++)
            {
                var path = VerifyPath(originFlights[flight], destinationFlights, flights, out isCompleted);
                if (isCompleted)
                {
                    route.AddRange(path);
                    route.AddRange(destinationFlights);
                    break;
                }

                CheckNodes(originFlights, flights, out destinationFlights, destination);

                foreach (var item in destinationFlights)

                {
                    //CheckNodes(originFlights);
                }
            }

            journey = new Journey()
            {
                Destination = destination,
                Origin = origin,
                Flights = route,
                Price = (from flight in route
                         select flight.Price).Sum()
            };

            return journey;
        }
        #endregion


        #region Private Fields
        /// <summary>
        /// 
        /// </summary>
        /// <param name="originFlight"></param>
        /// <param name="destinationFlights"></param>
        /// <param name="flights"></param>
        /// <returns></returns>
        private List<Flight> VerifyPath(Flight originFlight, List<Flight> destinationFlights, List<Flight> flights, out bool isCompleted)
        {
            isCompleted = false;
            var path = new List<Flight>();
            var destination = destinationFlights.Where(x => originFlight.arrivalStation.Equals(x.Origin)).SingleOrDefault();
            if (destination != null)
            {
                path.Add(originFlight);
                isCompleted = true;
                return path;
            }
            //Se Compara contra todos los vuelos disponibles
            for (var flight = 0; flight < flights.Count; flight++)
            {
                var currentFligth = flights[flight];
                var matchDestination = destinationFlights.Where(x => x.Origin.Equals(currentFligth.arrivalStation)).SingleOrDefault();
                if (matchDestination != null)
                {
                    //Ruta encontrada
                    path.Add(currentFligth);
                    break;
                }

                if (flights.Count.Equals(flight + 1))
                {
                    //Se limpia la ruta al no encontrar el destino
                    path.Clear();
                }

                path.Add(currentFligth);
                //Se buscan los siguientes vuelos a partir del Destino del vuelo actual
                //var newOrigin = currentFligth.arrivalStation;
                //var crossroad = GetRouteFlights(newOrigin, flights, true);
                //foreach (var item in crossroad)
                //{

                //}
            }

            return path;
        }


        /// <summary>
        /// Se obtienen las rutas
        /// </summary>
        /// <param name="flights"></param>
        /// <param name="spot"></param>
        /// <returns></returns>
        private List<Flight> GetRouteFlights(string spot, List<Flight> flights, bool isOrigin)
        {
            var originFlights = (from flight in flights
                                 where isOrigin ? flight.Origin == spot : flight.arrivalStation == spot
                                 select new Flight()
                                 {
                                     Origin = flight.Origin,
                                     arrivalStation = flight.arrivalStation,
                                     FlightCarrier = flight.FlightCarrier,
                                     FlightNumber = flight.FlightNumber,
                                     Price = flight.Price
                                 }).ToList();

            return originFlights;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originFlights">Todos los vuelos de origen</param>
        /// <param name="flights">Los demas vuelos restante</param>
        /// <param name="path">Donde se guardará la ruta con el destino</param>
        /// <returns></returns>
        private void CheckNodes(List<Flight> originFlights, List<Flight> flights, out List<Flight> destinationsFlight, string destination)
        {
            destinationsFlight = new List<Flight>();
            var finalDestination = new Flight();
            for (var i = 0; i < originFlights.Count; i++)
            {
                List<Flight> nextDestinations = NextDestinations(originFlights[i], flights);

                if (nextDestinations.Count == 0)
                    continue;
                else
                {
                    finalDestination = nextDestinations.Where(x => x.arrivalStation == destination).SingleOrDefault();
                    if (finalDestination != null)
                    {
                        destinationsFlight.Add(finalDestination);
                        //isCompleted = true;

                    }
                    else
                    {

                    }

                    //Se limpia la lista al no encotrar camino
                    foreach (var itemDestination in nextDestinations)
                    {
                        finalDestination = itemDestination.arrivalStation.Equals(destination) ? itemDestination : null;

                        if (finalDestination != null)
                        {
                            //path.Add(originFlight);
                            //isCompleted = true;
                            //return path;
                        }
                    }
                }


                if (i.Equals(0)) destinationsFlight.Clear();
            }
        }

        private static List<Flight> NextDestinations(Flight originFlight, List<Flight> flights)
        {
            //Obtengo las siguientes Destinos
            return flights.Where(x => x.Origin == originFlight.arrivalStation).ToList();
        }

        //public int NODOS { get; set; }
        //private void short_path(int matriz[][NODOS], int s, int t, int* pd, int precede[])
        //{


        //    int distancia[NODOS];
        //    int perm[NODOS];
        //    int corriente, i, k, dc, menor_dist, nueva_dist;

        //    for (i = 0; i < NODOS; i++)
        //    {
        //        perm[i] = NO_MIEMBRO;
        //        distancia[i] = INFINITO;
        //    }
        //    perm[s] = MIEMBRO;
        //    distancia[s] = 0;
        //    corriente = s;
        //    while (corriente != t)
        //    {

        //        menor_dist = INFINITO;
        //        dc = distancia[corriente];

        //        for (i = 0; i < NODOS; i++)
        //        {
        //            nueva_dist = dc + matriz[corriente][i];
        //            if (nueva_dist < distancia[i])
        //            {
        //                distancia[i] = nueva_dist;
        //                precede[i] = corriente;
        //            }
        //            if (distancia[i] < menor_dist)
        //            {
        //                menor_dist = distancia[i];
        //                k = i;
        //            }
        //        }//fin de for
        //        corriente = k;
        //        perm[corriente] = MIEMBRO;
        //    }//fin de while
        //    *pd = distancia[i];

        //}
        #endregion
    }
}
