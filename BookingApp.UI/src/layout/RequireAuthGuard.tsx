import { useAuth0 } from "@auth0/auth0-react";
import { Outlet } from "react-router";

export default function ProtectedTwo(){

    const { isAuthenticated, isLoading: authLoading } = useAuth0();

    if (authLoading) {
        return <div className="p-4 text-slate-300">Loading account...</div>;
    }

    if (!isAuthenticated) {
        return <div className="p-4 text-slate-300">Please authorize first</div>;
    }

    return <Outlet />;
}