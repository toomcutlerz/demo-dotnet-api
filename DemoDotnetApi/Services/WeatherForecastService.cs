using DemoDotnetApi.ViewModels;

namespace DemoDotnetApi.Services;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly string[] _summaries = new[]
    {
        // "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        "Freezing: หนาวจัด, เย็นจนเป็นน้ำแข็ง",
        "Bracing: หนาวสดชื่น, หนาวแต่รู้สึกกระปรี้กระเปร่า",
        "Chilly: หนาวเย็น, หนาวแบบขนลุกนิดๆ",
        "Cool: เย็น, เย็นสบาย",
        "Mild: อากาศอุ่นๆ, ไม่หนาวไม่ร้อน",
        "Warm: อบอุ่น, รู้สึกสบายๆ",
        "Balmy: อุ่นสบาย, มักใช้กับลม/อากาศที่นุ่มนวล",
        "Hot: ร้อน",
        "Sweltering: ร้อนอบอ้าว, ร้อนจนแทบทนไม่ได้",
        "Scorching: ร้อนระอุ, ร้อนแผดเผา"
    };

    public WeatherForecast[] GetWeatherForecasts()
    {
        var result = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                _summaries[Random.Shared.Next(_summaries.Length)]
            ))
            .ToArray();

        return result;
    }
}
