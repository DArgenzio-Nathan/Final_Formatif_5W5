using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    Mock<SeatsService> seatsServiceMock;
    Mock<SeatsController> seatsControllerMock;

    public SeatsControllerTests()
    {
        seatsServiceMock = new Mock<SeatsService>();
        seatsControllerMock = new Mock<SeatsController>(seatsServiceMock.Object) { CallBase = true };

        seatsControllerMock.Setup(c => c.UserId).Returns("12345");
    }

    [TestMethod]
    public void ReserveSeat()
    {
        Seat newSeat = new Seat();
        newSeat.Id = 1;
        newSeat.Number = 1;

        seatsServiceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Returns(newSeat);

        var actionresult = seatsControllerMock.Object.ReserveSeat(newSeat.Number);

        var result = actionresult.Result;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SeatAlreadyTaken()
    {
        seatsServiceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatAlreadyTakenException());

        var actionresult = seatsControllerMock.Object.ReserveSeat(1);

        var result = actionresult.Result as UnauthorizedResult;
        Assert.IsNotNull(result);
    }

    [TestMethod]
    public void SeatOutOfBound()
    {
        seatsServiceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new SeatOutOfBoundsException());

        var actionresult = seatsControllerMock.Object.ReserveSeat(1);

        var result = actionresult.Result as NotFoundObjectResult;
        Assert.IsNotNull(result);
        Assert.AreEqual("Could not find " + 1, result.Value);
    }

    [TestMethod]
    public void UserAlreadySeated()
    {
        seatsServiceMock.Setup(s => s.ReserveSeat(It.IsAny<string>(), It.IsAny<int>())).Throws(new UserAlreadySeatedException());

        var actionresult = seatsControllerMock.Object.ReserveSeat(1);

        var result = actionresult.Result as BadRequestResult;
        Assert.IsNotNull(result);
    }

}
