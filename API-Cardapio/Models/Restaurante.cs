using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API_Cardapio.Models;

public partial class Restaurante
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Descricao { get; set; }

    public string? Foto { get; set; }

    public string? Endereco { get; set; }

    public int? CidadeId { get; set; }

    public int? TipoId { get; set; }

    public int? DonoId { get; set; }

    public DateTime? DeletedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<Cardapio> Cardapios { get; set; } = new List<Cardapio>();
    [JsonIgnore]
    public virtual Cidade? Cidade { get; set; }
    [JsonIgnore]
    public virtual Usuario? Dono { get; set; }
    [JsonIgnore]
    public virtual TiposRestaurate? Tipo { get; set; }
}
