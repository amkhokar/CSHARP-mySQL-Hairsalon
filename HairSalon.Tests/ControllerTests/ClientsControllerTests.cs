using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientsControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult indexView = controller.Index();          
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateForm_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();          
            ActionResult createView = controller.CreateForm();
            Assert.IsInstanceOfType(createView, typeof(ViewResult));
        }
        [TestMethod]
        public void Details_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult detailsView = controller.Details(0);
            Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
        }
        [TestMethod]
        public void UpdateForm_ReturnsCorrectView_True()
        {
            ClientsController controller = new ClientsController();
            ActionResult updateView = controller.UpdateForm(0);
            Assert.IsInstanceOfType(updateView, typeof(ViewResult));
        }
    }
}
