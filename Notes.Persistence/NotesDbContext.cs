using Microsoft.EntityFrameworkCore;
using Notes.Persistence.EntityTypeConfiguration;

namespace Notes.Persistence
{
    public sealed class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
