import { useQuery } from "@tanstack/react-query";
import type Room from "../../types/rooms/room";
import RoomCard from "./RoomCard";
import { useState } from "react";
import Pagingitem from "../PagingItem";
import type PageResponse from "../../types/pageResponse";
import SearchBar from "./SearchBar";
import useDebouncer from "../../hooks/useDebouncer";
import useApiClient from "../../api/api";

export default function Rooms(){

    const api = useApiClient();
    const [page, setPage] = useState(0);
    const pageSize = 5;

    const [search, setSearch] = useState("");
    const debouncedSearch = useDebouncer<string>(
        search,
        "");
    const [minCap, setMinCap] = 
        useState<number | undefined>(undefined);

    const debouncedMinCap = 
        useDebouncer<number | undefined>(
            minCap,
            undefined);

    const [isOp, setIsOp] = useState(true);
    const debouncedIsOp = useDebouncer<boolean>(
        isOp,
        true);

    const {data:pageRes, isLoading} = useQuery({
        queryKey: ["rooms", page, 
            debouncedSearch, debouncedMinCap, 
            debouncedIsOp],
        queryFn: async () => {
            const pageRes = await api.get<PageResponse<Room>>(
                `/rooms?page=${page}&pageSize=${pageSize}
                &searchTerm=${debouncedSearch}
                &minCapacity=${debouncedMinCap === 
                    undefined ? "" : minCap}
                &isOperational=${debouncedIsOp}`);    

            return pageRes.data;
        },
        staleTime: 10000,
        refetchOnWindowFocus: false
    });

    if (isLoading) return (
        <div>
            <SearchBar 
                page={page}
                search={search}
                onSearchChange={setSearch}
                minCap={minCap}
                onMinCapChange={setMinCap}
                isOp={isOp}
                onIsOpChange={setIsOp}/>
            <p>Loading rooms...</p>
        </div>);

    if (pageRes?.totalCount === 0) return (
        <div>
            <SearchBar 
                page={page}
                search={search}
                onSearchChange={setSearch}
                minCap={minCap}
                onMinCapChange={setMinCap}
                isOp={isOp}
                onIsOpChange={setIsOp}/>
            <p>Nothing to see there</p>
        </div>);

    return (
        <div className="flex flex-col gap-4">
            <SearchBar 
                page={page}
                search={search}
                onSearchChange={setSearch}
                minCap={minCap}
                onMinCapChange={setMinCap}
                isOp={isOp}
                onIsOpChange={setIsOp}/>
            {pageRes?.data.map((x:Room) => 
                <RoomCard room={x} />
                )}
            <Pagingitem 
                page={pageRes!.page} 
                pageSize={pageRes!.pageSize}
                totalCount={pageRes!.totalCount}
                onPageChange={(newPage) => setPage(newPage)}/>
        </div>);
}