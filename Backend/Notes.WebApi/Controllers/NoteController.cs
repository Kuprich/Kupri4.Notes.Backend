using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.WebApi.Models;
using System;
using System.Threading.Tasks;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;

        public NoteController(IMapper mapper) => _mapper = mapper;

        /// <summary>Gets a list of notes</summary>
        /// <remarks>
        /// Sample request: 
        /// GET /note
        /// </remarks> 
        /// <returns>Returns NoteListVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If the user is unauthorized</responce>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        /// <summary>Gets the details about the note by id</summary>
        /// <remarks>
        /// Sample request: 
        /// GET /note/DC3840D0-A25C-43AD-B88E-7437D82D6F80
        /// </remarks> 
        /// <param name="id">Note id (guid)</param>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <responce code="200">Success</responce>
        /// <responce code="401">If the user is unauthorized</responce>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                Id = id,
                UserId = UserId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>Creates a new note</summary>
        /// <remarks>
        /// Sample request: 
        /// POST /note/
        /// {
        ///     "Title": "note title",
        ///     "Details": "note details"
        /// }
        /// </remarks> 
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <responce code="201">Success</responce>
        /// <responce code="401">If the user is unauthorized</responce>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserId;

            var noteId = await Mediator.Send(command);

            return Ok(noteId);

        }

        /// <summary>Updates the note</summary>
        /// <remarks>
        /// Sample request: 
        /// PUT /note/
        /// {
        ///     "Title": "updated note title",
        ///     "Details": "updated note details"
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">UpdateNoteDto object</param> 
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If the user is unauthorized</responce>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);

            await Mediator.Send(command);

            return NoContent();

        }

        /// <summary>Delete the note by id</summary>
        /// <remarks>
        /// Sample request: 
        /// DELETE /note/{2F301B1D-06AF-40E1-BB9D-1D265E1B2007}
        /// </remarks> 
        /// <param name="id">Note id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <responce code="204">Success</responce>
        /// <responce code="401">If the user is unauthorized</responce>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                UserId = UserId,
                Id = id
            };

            await Mediator.Send(command);

            return NoContent();

        }
    }
}
