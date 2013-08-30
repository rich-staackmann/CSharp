using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTrackr.Models;
using TimeTrackr.Helpers;
using TimeTrackr.DAL;
using TimeTrackr.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using System.Web;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;

namespace TimeTrackr.Tests
{
    [TestClass]
    public class TaskTest
    {

        [TestMethod]
        public void Index_Contains_All_Tasks()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>(); //we mock an http context, so we can set our User Identity

            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = testUser },
                new Task {ID = 2, Description = "T2", UserProfile = testUser },
                new Task {ID = 3, Description = "T3", UserProfile = testUser } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object; //this will authenticate a user named brad-greene 
            Task[] result = ((IEnumerable<Task>)target.Index().
                ViewData.Model).ToArray();
            
            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("T1", result[0].Description);
            Assert.AreEqual("T2", result[1].Description);
            Assert.AreEqual("T3", result[2].Description);
        }

        [TestMethod]
        public void Cannot_View_Other_Users_Tasks()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile user1 = new UserProfile { UserId = 1, UserName = "brad-greene" };
            UserProfile user2 = new UserProfile { UserId = 2, UserName = "john-smith" };
            var contextMock = new Mock<ControllerContext>(); //we mock an http context, so we can set our User Identity

            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = user1 },
                new Task {ID = 2, Description = "T2", UserProfile = user2 },
                new Task {ID = 3, Description = "T3", UserProfile = user2 } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("john-smith");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object;
            Task[] result = ((IEnumerable<Task>)target.Index().
                ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.AreEqual("T2", result[0].Description);
            Assert.AreEqual("T3", result[1].Description);
        }

        [TestMethod]
        public void Can_Edit_Task()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>();

            mock.Setup(m => m.GetTaskByID(It.IsAny<int>())).Returns(new Task {ID = 1, Description = "T1", UserProfile = testUser, UserID = 1 });
               
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object;

            Task T1 = ((ViewResult)target.Edit(1)).ViewData.Model as Task; //for some reason this wont work in the unit test, it comes out as null...

            //Assert
            Assert.AreEqual(1, T1.ID);

        }

        [TestMethod]
        public void Cannot_Edit_Nonexistent_Task()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>();

            mock.Setup(m => m.GetTaskByID(It.IsAny<int>())).Returns((Task) null);

            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object;

            ActionResult T1 = target.Edit(2); //for some reason this wont work in the unit test, it comes out as null...

            //Assert
            Assert.IsInstanceOfType(T1, typeof(HttpNotFoundResult));

        }

        [TestMethod]
        public void Can_Save_Changes()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>();
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object;

            Task t = new Task { Category = "misc" };
            ActionResult result = target.Edit(t);

            mock.Verify(m => m.UpdateTask(t));
        }

        [TestMethod]
        public void Can_Delete()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>();
            Task t2 = new Task { ID = 2, Description = "T2", UserProfile = testUser, UserID = 1 };
            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = testUser, UserID = 1 },
                t2,
                new Task {ID = 3, Description = "T3", UserProfile = testUser, UserID = 1 } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);
            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object;

            target.DeleteConfirmed(2);

            mock.Verify(m => m.DeleteTask(t2.ID));
        }

        [TestMethod]
        public void Can_Get_Tasks_By_Category()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>(); //we mock an http context, so we can set our User Identity

            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = testUser, Category = "misc" },
                new Task {ID = 2, Description = "T2", UserProfile = testUser, Category = "misc" },
                new Task {ID = 3, Description = "T3", UserProfile = testUser, Category = "diary" } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object; //this will authenticate a user named brad-greene 

            Task[] result = ((IEnumerable<Task>)target.TasksByCategory("misc").ViewData.Model).ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1, result[0].ID);
            Assert.AreEqual(2, result[1].ID);
        }

        [TestMethod]
        public void Can_Get_Tasks_By_Date()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>(); //we mock an http context, so we can set our User Identity
            DateTime yesterday = new DateTime(2013, 8, 5);

            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = testUser, StartTime = new DateTime(2013, 8, 6), EndTime = new DateTime(2013, 8, 6).AddHours(0.5) },
                new Task {ID = 2, Description = "T2", UserProfile = testUser, StartTime = new DateTime(2013, 8, 6), EndTime = new DateTime(2013, 8, 6).AddHours(1) },
                new Task {ID = 3, Description = "T3", UserProfile = testUser, StartTime = DateTime.MinValue, EndTime = DateTime.MinValue.AddHours(1) } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object; //this will authenticate a user named brad-greene 

            Task[] result = ((IEnumerable<Task>)target.TasksByDate(yesterday, null).ViewData.Model).ToArray();

            Assert.AreEqual(2, result.Length);
            Assert.AreEqual(1, result[0].ID);
            Assert.AreEqual(2, result[1].ID);
        }

        [TestMethod]
        public void Cannot_Get_Tasks_By_Invalid_Date()
        {
            Mock<ITaskRepository> mock = new Mock<ITaskRepository>();
            UserProfile testUser = new UserProfile { UserId = 1, UserName = "brad-greene" };
            var contextMock = new Mock<ControllerContext>(); //we mock an http context, so we can set our User Identity
            DateTime yesterday = new DateTime(2013, 8, 5);
            DateTime dayBeforeYesterday = new DateTime(2013, 8, 4); //for testing an invalid search query
            mock.Setup(m => m.Tasks).Returns(new Task[] {
                new Task {ID = 1, Description = "T1", UserProfile = testUser, StartTime = new DateTime(2013, 8, 6), EndTime = new DateTime(2013, 8, 6).AddHours(0.5) },
                new Task {ID = 2, Description = "T2", UserProfile = testUser, StartTime = new DateTime(2013, 8, 6), EndTime = new DateTime(2013, 8, 6).AddHours(1) },
                new Task {ID = 3, Description = "T3", UserProfile = testUser, StartTime = DateTime.MinValue, EndTime = DateTime.MinValue.AddHours(1) } 
                }.AsQueryable());
            contextMock.SetupGet(p => p.HttpContext.User.Identity.Name).Returns("brad-greene");
            contextMock.SetupGet(p => p.HttpContext.Request.IsAuthenticated).Returns(true);

            TaskController target = new TaskController(mock.Object);
            target.ControllerContext = contextMock.Object; //this will authenticate a user named brad-greene 

            String result = (target.TasksByDate(yesterday, dayBeforeYesterday).ViewName);

            Assert.AreEqual("TasksByDate", result); //if the validation fails then we send the user back to the tasks by date view
        }
    }
}
