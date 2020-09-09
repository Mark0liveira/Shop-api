using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    //[table("nome da tabela caso queira")]
    public class Product
    {
        // Data notation para agilizar o desenvolvimento, iniciando pelo code
        // Seta como deve ser o nome da tabela e as validações das propriedades
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Título é obrigatório!")]
        [MinLength(3, ErrorMessage = "Título deve conter entre 3 e 60 caracteres!")]
        [MaxLength(60, ErrorMessage = "Título deve conter entre 3 e 60 caracteres!")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "Descrição deve conter no máximo 1024 caracteres!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Preço é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "O Preço deve ser maior que zero")]
        public int Price { get; set; }

        [Required(ErrorMessage = "CategoriaId é obrigatório!")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}