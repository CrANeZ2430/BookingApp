import { useAuth0 } from "@auth0/auth0-react";
import useApiClient from "../api/api";
import { useQuery } from "@tanstack/react-query";
import { Navigate, Outlet, useLocation } from "react-router";

export default function ProfileSetupGuard() {

    const { isAuthenticated, isLoading:authLoading } = useAuth0();
    const api = useApiClient();
    const location = useLocation();

    const { data, isLoading:profileLoading } = useQuery({queryKey: ["currentMember"],
        queryFn: async () => {
        const res = await api.get("members/me");
        return res.data;
        },
        enabled: isAuthenticated,
        retry: false
    });

    if (authLoading || (isAuthenticated && profileLoading)) {
        return <div className="p-4 h-screen w-full bg-slate-900 text-slate-300">Loading...</div>;
    }

    if (isAuthenticated && !data?.profileExists) {
        if (location.pathname !== "/profile-setup") {
            return <Navigate to="/profile-setup" replace />;
        }

        return <Outlet />;
    }

    if ((isAuthenticated && data?.profileExists || !isAuthenticated) && location.pathname === "/profile-setup") {
        return <Navigate to="/" replace />;
    }

    return <Outlet />;
}