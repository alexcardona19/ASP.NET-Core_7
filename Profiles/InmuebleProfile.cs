using AutoMapper;
using NetKubernetes.Dtos.InmueblesDtos;
using NetKubernetes.Models;

namespace NetKubernetes.Profiles
{
	public class InmuebleProfile : Profile
	{
		public InmuebleProfile() 
		{
			CreateMap<Inmueble, InmuebleResponseDto>();
			CreateMap<InmuebleResponseDto, Inmueble>();
		}
	}
}
