using System.Text.Json.Serialization;

namespace BlazorWebAssemblyControleLivros.Models
{
    public class Livro
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("titulo")]
        public string Titulo { get; set; }

        [JsonPropertyName("autor")]
        public string Autor { get; set; }

        [JsonPropertyName("genero")]
        public string Genero { get; set; }

        [JsonPropertyName("dataLeitura")]
        public DateTime? DataLeitura { get; set; }

        [JsonPropertyName("avaliacao")]
        public int Avaliacao { get; set; }

        [JsonPropertyName("comentarios")]
        public string Comentarios { get; set; }

        [JsonPropertyName("dataRegistro")]
        public DateTime? DataRegistro { get; set; }
    }
}
