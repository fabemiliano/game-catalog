using System;
using System.ComponentModel.DataAnnotations;
namespace ApiCatalogo.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve ter entre 3 e 100 carecteres")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome do jogo deve ter entre 31e 100 carecteres")]
        public string Developer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "O preço deve ser entre 1 e 1000 reais")]
        public double Price { get; set; }

    }
}
