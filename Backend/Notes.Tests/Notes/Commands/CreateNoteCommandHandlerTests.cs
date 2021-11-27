using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.Commands.CreateNote;
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
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(Context);
            var noteTitle = "note title";
            var noteDetails = "note details";
            var request = new CreateNoteCommand()
            {
                UserId = NotesContextFactory.UserAId,
                Title = noteTitle,
                Details = noteDetails
            };

            //Act
            var noteId = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId && 
                note.Title == noteTitle && 
                note.Details == note.Details));

        }
    }
}
