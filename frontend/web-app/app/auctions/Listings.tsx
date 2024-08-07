'use client'

import { Auction, PagedResult } from "@/types";
import AuctionCard from "./AuctionCard";
import AppPagination from "../components/AppPagination";
import { useEffect, useState } from "react";
import { getData } from "../actions/auctionActions";
import Filters from "./Filters";

export default function Listings() {
    const [auctions, setAuctions] = useState<Auction[]>([]);
    const [pageCount, setPageCount] = useState(0);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(4);

    useEffect(() => {
        getData(pageNumber, pageSize).then(data => {
            setAuctions(data.results);
            setPageCount(data.pageCount);
        })
    }, [pageNumber, pageSize])

    if(auctions.length === 0)
        return <h3>Loading...</h3>

    return (
        <>
            <Filters pageSize={pageSize} setPageSize={setPageSize}></Filters>
            <div className="grid grid-cols-4 gap-6">
                {auctions.map((auction) => (
                    <AuctionCard auction={auction} key={auction.id}></AuctionCard>
                ))}
            </div>
            <div className="flex justify-center mt-4">
                <AppPagination currentPage={pageNumber} pageCount={pageCount} pageChanged={setPageNumber}/>
            </div>
        </>

    )
}