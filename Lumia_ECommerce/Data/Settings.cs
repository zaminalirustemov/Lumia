using System.ComponentModel.DataAnnotations;

namespace Lumia_ECommerce.Data;
public class Settings
{
    public int Id { get; set; }
    [StringLength(maximumLength:20)]
    public string Key { get; set; }
    [StringLength(maximumLength:150)]
    public string Value { get; set; }
}
