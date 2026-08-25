import { Navigate, Outlet } from "react-router";
import useApiClient from "../api/api";
import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "@tanstack/react-query";

export default function ProtectedRoute(){

    const { isAuthenticated, isLoading: authLoading } = useAuth0();
    const api = useApiClient();

    const { data: data, isLoading: profileLoading} = useQuery({
        queryKey: ["currentMember"],
        queryFn: async () => {
        const res = await api.get("members/me");
        return res.data;
        },
        enabled: isAuthenticated,
        retry: false,
    });

    if (authLoading || (isAuthenticated && profileLoading)) {
        return <div className="p-4 text-slate-300">Loading account...</div>;
    }

    if (!isAuthenticated) {
        return <Navigate to="/" replace />;
    }

    if (!data.profileExists) {
        return <Navigate to="/profile-setup" replace />;
    }

    return <Outlet />
}