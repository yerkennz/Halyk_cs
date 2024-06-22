using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurrencyMvcApi.Data;
using CurrencyMvcApi.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Globalization;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly TestDbContext _context;

    public CurrencyController(TestDbContext context)
    {
        _context = context;
    }

    [HttpGet("save/{date}")]
    public async Task<IActionResult> SaveData(string date)
    {
        var url = $"https://nationalbank.kz/rss/get_rates.cfm?fdate={date}";
        var httpClient = new HttpClient();
        var response = await httpClient.GetStringAsync(url);

        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(response);

        var items = xmlDoc.SelectNodes("//item");
        var count = 0;

        foreach (XmlNode item in items)
        {
            var currency = new Currency
            {
                Title = item["fullname"].InnerText,
                Code = item["title"].InnerText,
                Value = decimal.Parse(item["description"].InnerText, CultureInfo.InvariantCulture),
                A_Date = DateTime.SpecifyKind(DateTime.Parse(date), DateTimeKind.Utc)
            };

            _context.R_CURRENCY.Add(currency);
            count++;
        }

        await _context.SaveChangesAsync();

        return Ok(new { count });
    }
    [HttpGet("{date}/{code?}")]
    public async Task<IActionResult> GetData(string date, string code = null)
    {
        var query = _context.R_CURRENCY.AsQueryable();
        
        if (DateTime.TryParse(date, out var parsedDate))
        {
            query = query.Where(c => c.A_Date == parsedDate);
        }

        if (!string.IsNullOrEmpty(code))
        {
            query = query.Where(c => c.Code == code);
        }

        var result = await query.ToListAsync();
        
        if (result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
}