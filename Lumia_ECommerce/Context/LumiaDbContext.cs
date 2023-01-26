using Lumia_ECommerce.Data;
using Lumia_ECommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumia_ECommerce.Context;
public class LumiaDbContext : IdentityDbContext
{
	public LumiaDbContext(DbContextOptions<LumiaDbContext> options) : base(options) { }

	public DbSet<Position> Positions { get; set; }
	public DbSet<Team> Teams { get; set; }
	public DbSet<AppUser> AppUsers { get; set; }
	public DbSet<Settings> Settings { get; set; }
}

