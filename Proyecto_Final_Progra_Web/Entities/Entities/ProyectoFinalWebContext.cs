using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Entities.Entities;

public partial class ProyectoFinalWebContext : DbContext
{
    public ProyectoFinalWebContext()
    {
    }

    public ProyectoFinalWebContext(DbContextOptions<ProyectoFinalWebContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer();
    }

    public DbSet<Carta> Cartas { get; set; }

    public DbSet<Mazo> Mazos { get; set; }

    public DbSet<ApplicationUser> Users { get; set; }
}
