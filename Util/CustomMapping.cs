using AutoMapper;
using Shifts_ETL.Models;
using Shifts_ETL.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifts_ETL.Util
{
    public static class CustomMapping
    {
        public static Shifts MapShifts(Shift shift)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Shift, Shifts>()
                    .ForMember(dest => dest.shift_id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.shift_date, act => act.MapFrom(src => src.Date))
                    .ForMember(dest => dest.shift_start, act => act.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(src.Start)))
                    .ForMember(dest => dest.shift_finish, act => act.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(src.Finish)))
                );

            double cost = 0;

            foreach(var item in shift.Allowances)
                cost += item.Cost;

            foreach (var item in shift.AwardInterpretations)
                cost += item.Cost;

            var mapper = new Mapper(config);
            var result = mapper.Map<Shifts>(shift);
            result.shift_cost = cost;

            return result;
        }

        public static Allowances MapAllowences(Allowance allowance)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Allowance, Allowances>()
                    .ForMember(dest => dest.allowance_id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.allowance_value, act => act.MapFrom(src => src.Value))
                    .ForMember(dest => dest.allowance_cost, act => act.MapFrom(src => src.Cost))
                    .ForMember(dest => dest.shift_id, act => act.Ignore())
                );

            var mapper = new Mapper(config);
            var result = mapper.Map<Allowances>(allowance);

            return result;
        }

        public static AwardInterpretations MapAwards(AwardInterpretation award)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<AwardInterpretation, AwardInterpretations>()
                    .ForMember(dest => dest.award_id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.award_date, act => act.MapFrom(src => src.Date))
                    .ForMember(dest => dest.award_units, act => act.MapFrom(src => src.Units))
                    .ForMember(dest => dest.award_cost, act => act.MapFrom(src => src.Cost))
                    .ForMember(dest => dest.shift_id, act => act.Ignore())
                );

            var mapper = new Mapper(config);
            var result = mapper.Map<AwardInterpretations>(award);

            return result;
        }

        public static Breaks MapBreaks(Break @break)
        {
            var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Break, Breaks>()
                    .ForMember(dest => dest.break_id, act => act.MapFrom(src => src.Id))
                    .ForMember(dest => dest.break_start, act => act.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(src.Start)))
                    .ForMember(dest => dest.break_finish, act => act.MapFrom(src => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(src.Finish)))
                    .ForMember(dest => dest.is_paid, act => act.MapFrom(src => src.Paid))
                    .ForMember(dest => dest.shift_id, act => act.Ignore())
                );

            var mapper = new Mapper(config);
            var result = mapper.Map<Breaks>(@break);

            return result;
        }
    }
}
