using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Details).NotEmpty().MaximumLength(2000);
        }
    }
}
