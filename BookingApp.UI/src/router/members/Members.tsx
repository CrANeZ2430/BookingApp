import { useState } from "react";
import type PageResponse from "../../types/pageResponse";
import { useQuery } from "@tanstack/react-query";
import MemberCard from "./MemberCard";
import type Member from "../../types/members/member";
import PagingItem from "../PagingItem";
import useApiClient from "../../api/useApiClient";
import type { AxiosError } from "axios";

export default function Members() {

    const api = useApiClient();

    const pageSize = 5;
    const [page, setPage] = useState(0);

    const {data:pageRes, isLoading} = useQuery({
        queryKey: ["members"],
        queryFn: async () => {
            
            const pageRes = await api.get<PageResponse<Member>>(
                `/members?page=${page}&pageSize=${pageSize}`);    

            //console.log(pageRes);

            return pageRes.data;
        },
        staleTime: 10000,
        retry: false,
        refetchOnWindowFocus: false,
        throwOnError: (error: AxiosError) => error.response?.status === 403
    });

    if (isLoading) return (
        <div>
            <p>Loading members...</p>
        </div>);
    
    if (pageRes?.totalCount === 0) return (
        <div>
            <p>Nothing to see there</p>
        </div>);

    return (
        <div className="flex flex-col gap-4">
            <div>
                {pageRes?.data.map(x => 
                    <MemberCard member={x} />
                )}
            </div>
            <PagingItem 
                page={pageRes!.page} 
                pageSize={pageRes!.pageSize}
                totalCount={pageRes!.totalCount}
                onPageChange={(newPage) => setPage(newPage)}/>
        </div>);
}