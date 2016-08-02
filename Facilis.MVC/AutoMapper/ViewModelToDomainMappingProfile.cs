﻿using AutoMapper;
using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.MVC.ViewModels;

namespace Facilis.MVC.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Evento, EventoViewModel>();
            Mapper.CreateMap<Usuario, RegisterViewModel>();
        }
    }
}