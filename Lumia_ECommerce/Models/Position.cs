using System.ComponentModel.DataAnnotations;

namespace Lumia_ECommerce.Models;
public class Position
{
    public int Id { get; set; }
    [StringLength(maximumLength:30)]
    public string Name { get; set; }
    public bool isDeleted { get; set; }

    public List<Team> teams { get; set; }
}

