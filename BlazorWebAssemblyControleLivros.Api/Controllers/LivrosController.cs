using BlazorWebAssemblyControleLivros.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace BlazorWebAssemblyControleLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string UrlPlanilha = "https://script.google.com/macros/s/AKfycbyZaJmunoDpFGCJWYWaoZL7q8_-3Znr3xyvF23f6VX5ZDK9QFCl2fT72BTEMgcxz88x/exec";
        private List<Livro>? livros;

        public LivrosController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Livro livro)
        {
            if (livro == null || string.IsNullOrEmpty(livro.Titulo))
            {
                return BadRequest("Dados inválidos.");
            }

            HttpClient? client = _httpClientFactory.CreateClient();
            string? json = JsonSerializer.Serialize(livro);
            StringContent? content = new StringContent(json, Encoding.UTF8, "text/plain");

            try
            {
                HttpResponseMessage? response = await client.PostAsync(UrlPlanilha, content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(new { mensagem = "Salvo com sucesso na planilha!" });
                }
                    
                return StatusCode((int)response.StatusCode, "Erro ao integrar com Google Sheets.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            HttpClient? client = _httpClientFactory.CreateClient();

            try
            {
                HttpResponseMessage? response = await client.GetAsync(UrlPlanilha);

                if (response.IsSuccessStatusCode)
                {
                    // Precisa ler a string e converter manualmente
                    var json = await response.Content.ReadAsStringAsync();
                    livros = JsonSerializer.Deserialize<List<Livro>>(json);


                    if (livros != null)
                    {
                        return Ok(livros);
                    }
                }

                return StatusCode((int)response.StatusCode, "Erro ao integrar com Google Sheets.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
