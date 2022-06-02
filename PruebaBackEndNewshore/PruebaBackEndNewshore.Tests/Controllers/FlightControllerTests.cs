using Microsoft.VisualStudio.TestTools.UnitTesting;
using NewshoreAir.Domain.FlightService;
using NewshoreAir.Domain.Models;
using PruebaBackEndNewshore.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaBackEndNewshore.Controllers.Tests
{
    [TestClass()]
    public class FlightControllerTests
    {
        private readonly IFlightService _flightService;

        [TestMethod()]
        public void GetRouteTest()
        {
            //Disponer 
            FlightController controller = new FlightController(_flightService);

            //Actuar
            List<Flight> resutl = controller.GetRoute();
            Assert.IsNotNull(resutl);
        }
    }
}