namespace Kupri4.Notes.Identity.Data
{
    public static class DbInitializer
    {
        public static void Initialze(AuthDbContext dbContext) =>
            dbContext.Database.EnsureCreated();
    }
}
