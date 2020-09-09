using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    //[table("nome da tabela caso queira")]
    public class User
    {
        // Data notation para agilizar o desenvolvimento, iniciando pelo code
        // Seta como deve ser o nome da tabela e as validações das propriedades
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username é obrigatório!")]
        [MinLength(2, ErrorMessage = "Username deve conter entre 2 e 20 caracteres!")]
        [MaxLength(20, ErrorMessage = "Username deve conter entre 2 e 20 caracteres!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password é obrigatório!")]
        [MinLength(2, ErrorMessage = "Password deve conter entre 2 e 20 caracteres!")]
        [MaxLength(20, ErrorMessage = "Password deve conter entre 2 e 20 caracteres!")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}