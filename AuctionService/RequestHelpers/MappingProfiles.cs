using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
            CreateMap<Auction, AuctionDto>()
                .IncludeMembers(x => x.Item);

            CreateMap<Item, AuctionDto>();

            CreateMap<CreateAuctionDto, Auction>()
                .ForMember(d => d.Item, o => o.MapFrom(s => s));

            CreateMap<CreateAuctionDto, Item>();

            CreateMap<UpdateAuctionDto, Auction>()
                .ForMember(d => d.Item, o => o.MapFrom(s => s))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateAuctionDto, Item>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null)
                        return false;

                    if (srcMember is int intValue)
                    {
                        return intValue != 0;
                    }

                    return true;
                }));

            CreateMap<AuctionDto, AuctionCreated>();

            CreateMap<Auction, AuctionUpdated>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionUpdated>();
        }
    }
}
