using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Tests.Common;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            //Arrange 
            var removedNoteId = NotesContextFactory.NotesA[3].Id;
            var handler = new DeleteNoteCommandHandler(Context);
            var request = new DeleteNoteCommand()
            {
                UserId = NotesContextFactory.UserAId,
                Id = removedNoteId
            };

            // Act
            await handler.Handle(request, CancellationToken.None);

            //Assert
            Assert.Null(await Context.Notes.SingleOrDefaultAsync(note => note.Id == removedNoteId));

        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailOnWrongNoteIdOrUserId()
        {
            //Arrange 
            var removedNoteId = Guid.NewGuid();
            var handler = new DeleteNoteCommandHandler(Context);
            var request = new DeleteNoteCommand()
            {
                UserId = NotesContextFactory.UserAId,
                Id = removedNoteId
            };

            // Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
              await handler.Handle(request, CancellationToken.None));

        }
    }
}
