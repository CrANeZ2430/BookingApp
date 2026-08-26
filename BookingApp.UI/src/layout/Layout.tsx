import { Outlet } from "react-router";
import LayoutLink from "./LayoutLink";
import { useAuth0 } from "@auth0/auth0-react";
import LogoutButton from "./LogoutButton";
import LoginButton from "./LoginButton";

export default function Layout(){

    const {isAuthenticated} = useAuth0();

    return (
        <div className="flex flex-col h-screen w-full bg-slate-900 text-slate-300 overflow-hidden">
            <nav className="flex items-center justify-between bg-slate-950 p-4 border-b border-slate-800 shrink-0">
                <div className="flex gap-4 items-center">
                    <LayoutLink contents={"Home"} navLink={"/"} />
                    <LayoutLink contents={"Rooms"} navLink={"/rooms"} />
                    <LayoutLink contents={"Room types"} navLink={"/room-types"}/>
                    <LayoutLink contents={"Your bookings"} navLink={"/bookings"}/>
                </div>
                <div className="flex gap-4 items-center">
                    <LayoutLink contents={"Profile"} navLink={"/profile"} />
                    {isAuthenticated ? <LogoutButton/> : <LoginButton />}
                </div>
            </nav>
            <main className="flex-1 w-full overflow-y-auto p-6">
                <Outlet />
            </main>
        </div>
    );
}