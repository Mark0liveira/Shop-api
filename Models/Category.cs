using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    //[table("nome da tabela caso queira")]
    public class Category
    {
        // Data notation para agilizar o desenvolvimento, iniciando pelo code
        // Seta como deve ser o nome da tabela e as validações das propriedades
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title é obrigatório!")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres!")]
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres!")]
        public string Title { get; set; }
    }
}