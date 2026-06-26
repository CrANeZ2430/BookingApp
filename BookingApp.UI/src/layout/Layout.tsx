import { Outlet } from "react-router";
import LayoutLink from "./LayoutLink";

export default function Layout(){

    return (
        <div className="flex flex-col h-screen w-full bg-slate-900 text-slate-300 overflow-hidden">
            <nav className="flex gap-4 bg-slate-950 p-4 border-b border-slate-800 shrink-0">
                <LayoutLink contents={"Home"} navLink={"/"} />
                <LayoutLink contents={"Members"} navLink={"/members"} />
            </nav>
            <main className="flex-1 w-full overflow-y-auto p-6">
                <Outlet />
            </main>
        </div>
    );
}