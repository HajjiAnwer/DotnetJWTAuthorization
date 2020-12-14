using AutoMapper;
using Commander.DTOModels;
using Commander.Models;

namespace Commander.Profiles
{
    public class CommandProfile : Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandCreateDTO>();
            CreateMap<UserModel, UserCreateDTO>();
            CreateMap<UserCreateDTO, UserModel>();
        }
    }
}