import { useQuery } from "@tanstack/react-query";
import { api } from "../../api/api";
import RoomTypeCard from "./RoomTypeCard";
import type RoomType from "../../types/roomTypes/roomType";
import Pagingitem from "../PagingItem";
import type PageResponse from "../../types/pageResponse";
import { useState } from "react";

export default function RoomTypes(){

    const [page, setPage] = useState(0);

    const pageSize = 2;

    const {data:pageRes, isLoading} = useQuery({
        queryKey: ["roomTypes", page],
        queryFn: async () => {
            const res = await api.get<PageResponse<RoomType>>(`/room-types?page=${page}&pageSize=${pageSize}`);
            return res.data;
        },
        staleTime: 10000,
        refetchOnWindowFocus: false
    });

    if (isLoading) return <div>Loading room types...</div>;

    return (
        <div className="flex flex-col gap-4">
            {pageRes?.data.map((x:RoomType) => 
                <RoomTypeCard roomType={x} />
                )}
            <Pagingitem 
                page={pageRes!.page}
                pageSize={pageRes!.pageSize}
                totalCount={pageRes!.totalCount}
                onPageChange={(newPage) => setPage(newPage)}/>
        </div>
    );
}