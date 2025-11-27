using dotNetWedEve;
using Microsoft.EntityFrameworkCore;

public class dotNetWedEveContext : DbContext
{
    public dotNetWedEveContext(
        DbContextOptions options) : base(options)
    {
    }

}