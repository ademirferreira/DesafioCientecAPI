using AutoMapper;
using DesafioCientec.API.ViewModels;
using DesafioCientec.Business.Models;

namespace DesafioCientec.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Fundacao, FundacaoViewModel>().ReverseMap();
        }
    }
}