using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Notes.Tests.Common;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private NotesDbContext _context;
        private IMapper _mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetNoteDetailsQueryHandler(_context, _mapper);
            var request = new GetNoteDetailsQuery()
            {
                Id = NotesContextFactory.NotesB[3].Id,
                UserId = NotesContextFactory.UserBId
            };

            var expectedDetails = NotesContextFactory.NotesB[3].Details;
            var expectedTitle = NotesContextFactory.NotesB[3].Title;

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<NoteDetailsVm>();
            result.Title.ShouldBe(expectedTitle);
            result.Details.ShouldBe(expectedDetails);
        }
    }
}
