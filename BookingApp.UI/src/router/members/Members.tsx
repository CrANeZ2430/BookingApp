import { useQuery } from "@tanstack/react-query";
import { api } from "../../api/api";
import MemberCard from "./MemberCard";
import type Member from "../../types/member";
import PagingItem from "./PagingItem";
import { useState } from "react";

export default function Members() {

    const [page, setPage] = useState(0);
    const pageSize = 5;

    const { data: members, isLoading, isFetching } = useQuery({
        queryKey: ['members', page],
        queryFn: async () => {
            const res = await api.get(`/members?page=${page}&pageSize=${pageSize}`);
            return res.data;
            },
        staleTime: 10000,
        refetchOnWindowFocus: false
    });

    if (isLoading) return <div>Loading...</div>;

    return (
        <div className="flex flex-col gap-4">
            {isFetching && <div className="text-xs text-amber-500 animate-pulse">Syncing...</div>}

            <div className="flex flex-col gap-4">
                {members?.map((member:Member) => (
                    <MemberCard 
                        key={member.memberId} 
                        member={member} />
                ))}
            </div>
            <PagingItem 
                page={page}
                onPageChange={(newPage) => setPage(newPage)}  />
        </div>
    );
}