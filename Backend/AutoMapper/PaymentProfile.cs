using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BatchPayments.Utility.Models;

namespace BatchPayments.Utility.AutoMapper
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<CsvRecord, Payment>()
                .ForMember(d => d.PaymentId, 
                    opt => opt.MapFrom(s => s.PaymentId))
                .ForMember(d => d.PayeeReference,
                    opt => opt.MapFrom(s => s.PaymentRef))
                .ForMember(d => d.Amount,
                    opt => opt.MapFrom(s => s.NetAmount))
                .ForMember(d => d.RecipientId,
                    opt => opt.MapFrom(s => s.OfxClientId))
                .ForMember(d => d.RecipientType,
                    opt => opt.MapFrom(s => "user"))
            ;

        }
    }
}
