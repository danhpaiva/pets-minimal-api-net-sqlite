using System.ComponentModel.DataAnnotations;

namespace PetsApi.Models;

public class Animal
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string Cor { get; set; }
    public string Tipo { get; set; }
    public int Peso_kg { get; set; }
}
