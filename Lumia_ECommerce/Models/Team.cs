using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia_ECommerce.Models;
public class Team
{
    public int Id { get; set; }
    public int PositionId { get; set; }


    [StringLength(maximumLength: 100)]
    public string? ImageName { get; set; }
    [StringLength(maximumLength: 50)]
    public string Fulllname { get; set; }
    [StringLength(maximumLength: 200)]
    public string Description { get; set; }
    [StringLength(maximumLength: 200)]
    public string? TwURl { get; set; }
    [StringLength(maximumLength: 200)]
    public string? FbUrl { get; set; }
    [StringLength(maximumLength: 200)]
    public string? InstUrl { get; set; }
    [StringLength(maximumLength: 200)]
    public string? LInUrl { get; set; }
    public bool isDeleted { get; set; }

    public Position? Position { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

}

