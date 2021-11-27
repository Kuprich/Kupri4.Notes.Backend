using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            //Arrange
            var updatedTitle = "updated Title";
            var updatedDetails = "updated Details";

            var noteId = NotesContextFactory.NotesB[2].Id;
            var userId = NotesContextFactory.UserBId;

            var handler = new UpdateNoteCommandHandler(Context);
            var request = new UpdateNoteCommand()
            {
                Id = noteId,
                UserId = userId,
                Title = updatedTitle,
                Details = updatedDetails
            };

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId &&
                note.UserId == userId &&
                note.Title == updatedTitle &&
                note.Details == updatedDetails));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailWrongNoteIdOrUserId()
        {
            //Arrange
            var updatedTitle = "updated Title";
            var updatedDetails = "updated Details";

            var noteId = NotesContextFactory.NotesA[2].Id;
            var userId = NotesContextFactory.UserBId;

            var handler = new UpdateNoteCommandHandler(Context);
            var request = new UpdateNoteCommand()
            {
                Id = noteId,
                UserId = userId,
                Title = updatedTitle,
                Details = updatedDetails
            };

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => 
                await handler.Handle(request, CancellationToken.None));
        }
    }
}
