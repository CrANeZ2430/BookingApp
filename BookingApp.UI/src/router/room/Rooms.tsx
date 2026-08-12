import { useQuery } from "@tanstack/react-query";
import { api } from "../../api/api";
import type Room from "../../types/rooms/room";
import RoomCard from "./RoomCard";
import { useState } from "react";
import Pagingitem from "../PagingItem";
import type PageResponse from "../../types/pageResponse";

export default function Rooms(){

    const [page, setPage] = useState(0);
    const pageSize = 2;

    const {data:pageRes, isLoading} = useQuery({
        queryKey: ["rooms", page],
        queryFn: async () => {
            const res = await api.get<PageResponse<Room>>(`/rooms?page=${page}&pageSize=${pageSize}`);
            return res.data;
        },
        staleTime: 10000,
        refetchOnWindowFocus: false
    });

    if (isLoading) return <div>Loading rooms...</div>;

    return (
        <div className="flex flex-col gap-4">
            {pageRes?.data.map((x:Room) => 
                <RoomCard room={x} />
                )}
            <Pagingitem 
                page={pageRes!.page} 
                pageSize={pageRes!.pageSize}
                totalCount={pageRes!.totalCount}
                onPageChange={(newPage) => setPage(newPage)}/>
        </div>
    );
}