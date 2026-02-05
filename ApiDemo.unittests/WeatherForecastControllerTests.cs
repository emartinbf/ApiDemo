using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApiDemo.UnitTests;

public class WeatherForecastControllerTests
{
    private static readonly string[] ExpectedSummaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [Fact]
    public void Get_ReturnsFiveForecasts()
    {
        // Arrange
        var logger = new Mock<ILogger<ApiDemo.Controllers.WeatherForecastController>>();
        var controller = new ApiDemo.Controllers.WeatherForecastController(logger.Object);

        // Act
        var result = controller.Get();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(5);
    }

    [Fact]
    public void Get_ReturnsForecastsWithExpectedDateRange()
    {
        // Arrange
        var today = DateOnly.FromDateTime(DateTime.Now);
        var logger = new Mock<ILogger<ApiDemo.Controllers.WeatherForecastController>>();
        var controller = new ApiDemo.Controllers.WeatherForecastController(logger.Object);

        // Act
        var result = controller.Get().ToArray();

        // Assert
        result.Should().AllSatisfy(forecast =>
        {
            forecast.Date.Should().BeOnOrAfter(today.AddDays(1));
            forecast.Date.Should().BeOnOrBefore(today.AddDays(5));
        });
    }

    [Fact]
    public void Get_ReturnsForecastsWithValidSummariesAndTemperatureRange()
    {
        // Arrange
        var logger = new Mock<ILogger<ApiDemo.Controllers.WeatherForecastController>>();
        var controller = new ApiDemo.Controllers.WeatherForecastController(logger.Object);
        var summarySet = ExpectedSummaries.ToHashSet();

        // Act
        var result = controller.Get().ToArray();

        // Assert
        result.Should().AllSatisfy(forecast =>
        {
            forecast.Summary.Should().NotBeNullOrWhiteSpace();
            summarySet.Should().Contain(forecast.Summary!);
            forecast.TemperatureC.Should().BeInRange(-20, 54);
        });
    }
}
