using FluentAssertions;
using Xunit;

namespace ApiDemo.UnitTests;

public class WeatherForecastTests
{
    [Theory]
    [InlineData(0, 32)]
    [InlineData(20, 67)]
    [InlineData(-40, -39)]
    [InlineData(100, 211)]
    public void TemperatureF_ComputedFromTemperatureC_UsesExpectedFormula(int temperatureC, int expectedTemperatureF)
    {
        // Arrange
        var forecast = new WeatherForecast
        {
            TemperatureC = temperatureC
        };

        // Act
        var temperatureF = forecast.TemperatureF;

        // Assert
        temperatureF.Should().Be(expectedTemperatureF);
    }
}
