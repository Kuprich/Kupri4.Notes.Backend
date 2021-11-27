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

        public static Note[] NotesA = Enumerable.Range(1, 5).Select(i => new Note
        {
            CreationDate = DateTime.Today,
            Id = Guid.NewGuid(),
            Details = $"Details{i}",
            EditDate = null,
            Title = $"title{i}",
            UserId = UserAId
        }).ToArray();

        public static Note[] NotesB = Enumerable.Range(1, 5).Select(i => new Note
        {
            CreationDate = DateTime.Today,
            Id = Guid.NewGuid(),
            Details = $"Details{i}",
            EditDate = null,
            Title = $"title{i}",
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

            return context;

        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
