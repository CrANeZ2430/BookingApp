import { useQuery } from "@tanstack/react-query";
import { api } from "../../api/api";
import type PageResponse from "../../types/pageResponse";
import type Booking from "../../types/bookings/booking";
import BookingCard from "./BookingCard";
import PagingItem from "../PagingItem";
import { useState } from "react";
import type Member from "../../types/members/member";

export default function Bookings() {

    //temporary
    let memberId = "";
    api.get<PageResponse<Member>>("members")
        .then(data => memberId = data.data.data[0].memberId);

    console.log(memberId);

    const [page, setPage] = useState(0);
    const pageSize = 5;

    const {data:pageRes, isLoading} = useQuery({
            queryKey: ["bookings"],
            queryFn: async () => {
                const pageRes = await api.get<PageResponse<Booking>>(
                    `members/${memberId}/bookings?
                    page=${page}&pageSize=${pageSize}`);
                return pageRes.data;
            },
            staleTime: 10000,
            refetchOnWindowFocus: false
        });

    if (isLoading) return (
            <div>
                <p>Loading bookings...</p>
            </div>);

    if (pageRes?.totalCount === 0) return (
            <div>
                <p>Nothing to see there</p>
            </div>);

    return (
        <div className="flex flex-col gap-4">
            {pageRes?.data.map((x:Booking, index) => 
                <BookingCard 
                    index={(page * pageSize) + index + 1} 
                    booking={x} />
            )}
            <PagingItem 
                page={pageRes!.page}
                onPageChange={(newPage) => setPage(newPage)} 
                pageSize={pageRes!.pageSize} 
                totalCount={pageRes!.totalCount} />
        </div>);
}