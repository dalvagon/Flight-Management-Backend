﻿using AutoMapper;

namespace FlightManagement.Application.Mappers
{
    public static class FlightMapper
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<FlightMappingProfile>();
            });
            var mapper = config.CreateMapper();

            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}