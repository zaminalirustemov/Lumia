using Lumia_ECommerce.Context;
using Lumia_ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Lumia_ECommerce.Services;
public class LayoutService
{
    private readonly LumiaDbContext _lumiaDbContext;

    public LayoutService(LumiaDbContext lumiaDbContext)
    {
        _lumiaDbContext = lumiaDbContext;
    }
    public async Task<List<Settings>> GetSettings()
    {
        return await _lumiaDbContext.Settings.ToListAsync();
    }
}

