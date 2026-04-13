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
        private const string UrlPlanilha = "https://script.google.com/macros/s/AKfycbyfOjgRGWaxW2N_b8whd1y8saMIHEf7zIdz63RQtyxPJcC5hUGiHLi_XvcWIQeYJOmL/exec";
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

            // Garante que o livro tenha um ID único antes de ir para a planilha
            if (string.IsNullOrEmpty(livro.Id))
            {
                livro.Id = Guid.NewGuid().ToString().Substring(0, 8); // ID curto de 8 caracteres
            }

            HttpClient? client = _httpClientFactory.CreateClient();
            string? json = JsonSerializer.Serialize(livro);

            StringContent? content = new StringContent(json, Encoding.UTF8, "application/json");

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                string urlFinal = $"{UrlPlanilha}?id={id}";

                HttpResponseMessage response = await client.GetAsync(urlFinal);

                if (response.IsSuccessStatusCode)
                {
                    string resultado = await response.Content.ReadAsStringAsync();

                    if (resultado.Contains("Sucesso"))
                    {
                        return Ok(new { message = resultado });
                    }
                    return BadRequest(resultado);
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
