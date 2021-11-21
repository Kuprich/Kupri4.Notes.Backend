using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;
using System;

namespace Notes.Application.Notes.Queries.GetNoteList
{ 

    public class NoteLookupDto : IMapping
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<Note, NoteLookupDto>();
    }
}
