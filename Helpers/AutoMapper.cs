using AutoMapper;
using Microsoft.Extensions.Logging;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {

            CreateMap<PatientMilitaireDto, Patients>();
            CreateMap<PatientporteurCarteSoinsDto, Patients>();
            CreateMap<PatientCnamDto, Patients>();
            CreateMap<PatientMilitaireBhseDto, PatientBhse>();


            


        }
    }
}
