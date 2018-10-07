using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialtiesControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            SpecialtiesController controller = new SpecialtiesController();
            ActionResult indexView = controller.Index();
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateForm_ReturnsCorrectView_True()
        {
            SpecialtiesController controller = new SpecialtiesController();
            ActionResult createView = controller.CreateForm();
            Assert.IsInstanceOfType(createView, typeof(ViewResult));
        }
        [TestMethod]
        public void Details_ReturnsCorrectView_True()
        {
            SpecialtiesController controller = new SpecialtiesController();
            ActionResult detailsView = controller.Details(0);
            Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
        }
    }
}
