import { useAuth0 } from "@auth0/auth0-react";

export default function LogoutButton(){

    const {logout: auth0Logout} = useAuth0();

    const logout = () => auth0Logout({ logoutParams: { returnTo: window.location.origin } });

    return (
        <button
            className="border-2 border-slate-500 rounded-md px-4 py-1 bg-red-900 text-slate-300 hover:bg-red-800 transition duration-100 ease-in-out active:border-blue-600"
            onClick={logout}>
            Logout
        </button>
    );
}