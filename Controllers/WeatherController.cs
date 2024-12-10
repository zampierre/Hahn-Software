using Microsoft.AspNetCore.Mvc;
using WeatherApi.Data;
using WeatherApi.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherDbContext _context;

    public WeatherController(WeatherDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeatherRecord>>> GetWeather()
    {
        return await _context.WeatherRecords.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WeatherRecord>> GetWeatherById(int id)
    {
        var record = await _context.WeatherRecords.FindAsync(id);

        if (record == null)
        {
            return NotFound();
        }

        return record;
    }

    [HttpPost]
    public async Task<ActionResult<WeatherRecord>> AddWeather([FromBody] WeatherRecord record)
    {
        _context.WeatherRecords.Add(record);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWeatherById), new { id = record.Id }, record);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWeather(int id, [FromBody] WeatherRecord record)
    {
        if (id != record.Id)
        {
            return BadRequest();
        }

        _context.Entry(record).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.WeatherRecords.Any(e => e.Id == id))
            {
                return NotFound();
            }
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWeather(int id)
    {
        var record = await _context.WeatherRecords.FindAsync(id);

        if (record == null)
        {
            return NotFound();
        }

        _context.WeatherRecords.Remove(record);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
