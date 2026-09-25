import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "@tanstack/react-query";
import useApiClient from "../api/useApiClient";
import type CheckMemberResponse from "../types/checkMember/checkMemberResponse";

export default function useCurrentMember() {

    const api = useApiClient();
    const { isAuthenticated } = useAuth0();

    const { data, isLoading } = useQuery<CheckMemberResponse>({
        queryKey: ["currentMember"],
        queryFn: async () => {
            const res = await api.get("members/me");
            return res.data;
        },
        enabled: isAuthenticated,
        retry: false,
        staleTime: 100
    });

    return { 
        member: data?.member, 
        profileExists: data?.profileExists, 
        isLoading 
    };
}