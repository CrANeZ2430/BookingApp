import { useQuery } from "@tanstack/react-query";
import RoomTypeCard from "./RoomTypeCard";
import type RoomType from "../../types/roomTypes/roomType";
import Pagingitem from "../PagingItem";
import type PageResponse from "../../types/pageResponse";
import { useState } from "react";
import SearchBar from "./SearchBar";
import useDebouncer from "../../hooks/useDebouncer";
import useApiClient from "../../api/api";

export default function RoomTypes(){

    const api = useApiClient();
    const [page, setPage] = useState(0);
    const pageSize = 5;
    const [search, setSearch] = useState("");
    const debouncedSearch = useDebouncer<string>(
        search,
        "");

    const {data:pageRes, isLoading} = useQuery({
        queryKey: ["roomTypes", page, debouncedSearch],
        queryFn: async () => {
            const pageRes = await api.get<PageResponse<RoomType>>(
                `/room-types?page=${page}&pageSize=${pageSize}
                &searchTerm=${debouncedSearch}`);
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
            />
        <p>Loading room types...</p>
    </div>);

    if (pageRes?.totalCount === 0) return (
    <div>
        <SearchBar 
            page={page}
            search={search}
            onSearchChange={setSearch}
            />
        <p>Nothing to see there</p>
    </div>);

    return (
        <div className="flex flex-col gap-4">
            <SearchBar 
                page={page}
                search={search}
                onSearchChange={setSearch}
                />
            {pageRes?.data.map((x:RoomType) => 
                <RoomTypeCard roomType={x} />
                )}
            <Pagingitem 
                page={pageRes!.page}
                pageSize={pageRes!.pageSize}
                totalCount={pageRes!.totalCount}
                onPageChange={(newPage) => setPage(newPage)}/>
        </div>);
}