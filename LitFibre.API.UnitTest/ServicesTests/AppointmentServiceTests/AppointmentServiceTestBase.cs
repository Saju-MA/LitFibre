using AutoMapper;
using LitFibre.API.Data;
using Moq;
using LitFibre.API.Services;
using Microsoft.EntityFrameworkCore;

namespace LitFibre.API.UnitTest.ServicesTests.AppointmentServiceTests;

public class AppointmentServiceTestBase
{
    protected readonly AppDbContext _context;
    protected readonly Mock<IMapper> _mapper;
    protected readonly AppointmentService _service;

    public AppointmentServiceTestBase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);
        _mapper = new Mock<IMapper>();
        _service = new AppointmentService(_context, _mapper.Object);
    }
}
