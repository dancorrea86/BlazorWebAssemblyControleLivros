namespace BlazorWebAssemblyControleLivros.Models
{
    public class Livro
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = "Ficção";
        public DateTime? DataLeitura { get; set; } = DateTime.Now;
        public int Avaliacao { get; set; } = 5;
        public string Comentarios { get; set; } = string.Empty;
    }
}
