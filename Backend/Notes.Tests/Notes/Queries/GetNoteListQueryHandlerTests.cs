using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;
        public GetNoteListQueryHandlerTests(QueryTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public async Task GetNoteListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetNoteListQueryHandler(_context, _mapper);
            var request = new GetNoteListQuery()
            {
                UserId = NotesContextFactory.UserAId
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NoteListVm>();
            result.Notes.Count.ShouldBe(NotesContextFactory.UserANotesCount);
        }

    }
}
