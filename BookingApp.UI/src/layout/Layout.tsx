import { NavLink, Outlet } from "react-router";

export default function Layout(){

    return (
        <>
            <div className="flex gap-2">
                <NavLink to="/">
                    Home
                </NavLink>
                <NavLink to="/members">
                    Members
                </NavLink>
            </div>
            <main>
                <Outlet />
            </main>
        </>
    );
}