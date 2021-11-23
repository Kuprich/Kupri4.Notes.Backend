﻿using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;
using System;

namespace Notes.WebApi.Models
{
    public class UpdateNoteDto : IMapping
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile) =>
            profile.CreateMap<UpdateNoteCommand, UpdateNoteDto>(); 
    }
}
