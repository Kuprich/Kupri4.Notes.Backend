using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistence;
using System;
using System.Linq;

namespace Notes.Tests.Common
{
    internal class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static int UserANotesCount = 5;
        public static int UserBNotesCount = 5;

        public static Note[] NotesA = Enumerable.Range(1, UserANotesCount).Select(i => new Note
        {
            CreationDate = DateTime.Today,
            Id = Guid.NewGuid(),
            Details = $"DetailsA{i}",
            EditDate = null,
            Title = $"titleA{i}",
            UserId = UserAId
        }).ToArray();

        public static Note[] NotesB = Enumerable.Range(1, UserBNotesCount).Select(i => new Note
        {
            CreationDate = DateTime.Today,
            Id = Guid.NewGuid(),
            Details = $"DetailsB{i}",
            EditDate = null,
            Title = $"titleB{i}",
            UserId = UserBId
        }).ToArray();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new NotesDbContext(options);
            context.Database.EnsureCreated();

            context.Notes.AddRangeAsync(NotesA);
            context.Notes.AddRangeAsync(NotesB);
            context.SaveChangesAsync();

            return context;

        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
