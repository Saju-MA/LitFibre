using LitFibre.API.Services;
using LitFibre.API.Controllers;
using Moq;

namespace LitFibre.API.UnitTest.ControllerTests.AppointmentControllerTests;

public class AppointmentControllerTestBase
{
    protected readonly Mock<IAppointmentService> _service;
    protected readonly AppointmentsController _controller;

    public AppointmentControllerTestBase()
    {
        _service = new Mock<IAppointmentService>();
        _controller = new AppointmentsController(_service.Object);
    }
}
