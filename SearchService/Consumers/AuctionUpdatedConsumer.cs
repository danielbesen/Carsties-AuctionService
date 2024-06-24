﻿using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionUpdatedConsumer : IConsumer<AuctionUpdated>
    {
        private readonly IMapper _mapper;

        public AuctionUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
            Console.WriteLine();

            var itemUpdated = _mapper.Map<Item>(context.Message);

            var result = await DB.Update<Item>()
                .Match(x => x.ID == context.Message.Id)
                .ModifyOnly(x => new
                {
                    x.Color,
                    x.Make,
                    x.Model,
                    x.Year,
                    x.Mileage
                }, itemUpdated).ExecuteAsync();

            if (!result.IsAcknowledged)
                throw new MessageException(typeof(AuctionUpdated), "Problem updating mongodb");
            
        }
    }
}
