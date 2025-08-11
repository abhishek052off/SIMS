using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSWeb.Business.ServiceDTO.SubmissionDTO;
using SIMSWeb.Model.ViewModels;

namespace SIMSWeb.Business.ServiceDTO.ServiceProfile
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            CreateMap<SubmissionServiceModel, SubmissionViewModel>();
            CreateMap<SubmissionViewModel, SubmissionServiceModel>();
        }
        
    }
}
