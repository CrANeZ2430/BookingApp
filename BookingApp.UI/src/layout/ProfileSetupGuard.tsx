import { useAuth0 } from "@auth0/auth0-react";
import { Navigate, Outlet, useLocation } from "react-router";
import useCurrentMember from "../hooks/useCurrentMember";

export default function ProfileSetupGuard() {

    const { isAuthenticated, isLoading:authLoading } = useAuth0();
    const location = useLocation();

    const { profileExists, isLoading:profileLoading } = useCurrentMember();

    if (authLoading || (isAuthenticated && profileLoading)) {
        return <div className="p-4 h-screen w-full bg-slate-900 text-slate-300">Loading...</div>;
    }

    if (isAuthenticated && !profileExists) {
        if (location.pathname !== "/profile-setup") {
            return <Navigate to="/profile-setup" replace />;
        }

        return <Outlet />;
    }

    if ((isAuthenticated && profileExists || !isAuthenticated) && location.pathname === "/profile-setup") {
        return <Navigate to="/" replace />;
    }

    return <Outlet />;
}