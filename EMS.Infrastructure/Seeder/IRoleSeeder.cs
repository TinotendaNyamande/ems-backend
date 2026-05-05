namespace EMS.Infrastructure.Seeder
{
    public interface IRoleSeeder
    {
        Task SeedAsync(IServiceProvider serviceProvider);
    }
}
