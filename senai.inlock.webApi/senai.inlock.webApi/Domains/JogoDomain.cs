using System.ComponentModel.DataAnnotations;

namespace senai.inlock.webApi_.Domains
{
    public class JogoDomain
    {
        public int IdJogo { get; set; }
        public int IdEstudio { get; set; }
        [Required(ErrorMessage = "O nome do Jogo é obrigatório!")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "A descrição do Jogo é obrigatória!")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "A data de lançamento do Jogo é obrigatória!")]
        public DateTime DataLancamento { get; set; }
        [Required(ErrorMessage = "O valor do Jogo é obrigatório!")]
        public float? Valor { get; set; }
    }
}
