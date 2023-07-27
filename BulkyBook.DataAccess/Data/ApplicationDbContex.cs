using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess;

public class ApplicationDbContex : DbContext
{
    public ApplicationDbContex(DbContextOptions<ApplicationDbContex> options) : base(options)
    {

    }
        //   class name will be category table , Columns
    public DbSet<Category> categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
}
