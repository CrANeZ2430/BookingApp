import { NavLink } from "react-router";

interface LayoutLinkProps {
    contents:string,
    navLink:string
}

export default function LayoutLink({ contents, navLink }:LayoutLinkProps) {

    return (
        <NavLink 
            to={navLink}
            className={({ isActive }) => 
                `font-medium transition-colors ${isActive ? "text-amber-400" : "text-slate-300 hover:text-white"}`}>
                {contents}
        </NavLink>
    );
}