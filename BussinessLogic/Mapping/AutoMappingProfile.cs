using AutoMapper;
using DataAccess.Entities;
using Shared.Models;

namespace BusinessLogic.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<DailyForecast, DailyForecastModel>()
                .ForMember(dfm => dfm.Date, df => df.MapFrom(x => x.Date.ToString("yyyy-MM-dd")))
                .ForMember(dfm => dfm.DayWithMonth, df => df.MapFrom(x => x.Date.ToString("dd MMMM")))
                .ForMember(dfm => dfm.DayOfWeek, df => df.MapFrom(x => x.Date.ToString("dddd")))
                .ForMember(dfm => dfm.Summary, df => df.MapFrom(x => x.Summary.Name))
                .ForMember(dfm => dfm.Region, df => df.MapFrom(x => x.Region.TerrainType == null ?
                                           x.Region.Name : $"{x.Region.Name} {x.Region.TerrainType}"));

            CreateMap<DailyForecast, DailyForecastDetailsModel>()
                .ForMember(dfm => dfm.Date, df => df.MapFrom(x => x.Date.ToString("yyyy-MM-dd")))
                .ForMember(dfm => dfm.DayWithMonth, df => df.MapFrom(x => x.Date.ToString("dd MMMM")))
                .ForMember(dfm => dfm.DayOfWeek, df => df.MapFrom(x => x.Date.ToString("dddd")))
                .ForMember(dfm => dfm.Region, df => df.MapFrom(x => x.Region.Name))
                .ForMember(dfm => dfm.Summary, df => df.MapFrom(x => x.Summary.Name));

            CreateMap<HourlyForecast, HourlyForecastModel>()
                .ForMember(hfm => hfm.Hour, hf => hf.MapFrom(x => x.Hour.ToString("00") + ":00"));

            CreateMap<DailyForecastInputModel, DailyForecast>();

            CreateMap<HourlyForecastInputModel, HourlyForecast>();

            CreateMap<DailyForecastUpdateModel, DailyForecast>()
                .ForMember(df => df.HourlyForecasts, dfm => dfm.Ignore());

            CreateMap<DailyForecastForWeeklyInputModel, DailyForecast>()
                .ForMember(df => df.Date, dfm => dfm.MapFrom(x => x.Date))
                .ForMember(df => df.SummaryId, dfm => dfm.MapFrom(x => x.SummaryId))
                .ForMember(df => df.HourlyForecasts, dfm => dfm.MapFrom(x => x.HourlyForecasts));

            CreateMap<Region, RegionModel>()
                .ForMember(rm => rm.Name, r => r.MapFrom(x => x.TerrainType == null ?
                                                x.Name : $"{x.Name} {x.TerrainType}"))
                .ReverseMap()
                .ForMember(r => r.Id, r => r.Ignore());

            CreateMap<Summary, SummaryModel>()
                .ReverseMap()
                .ForMember(s => s.Id, sm => sm.Ignore());
        }
    }
}