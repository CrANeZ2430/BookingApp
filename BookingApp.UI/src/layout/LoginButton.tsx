import { useAuth0 } from "@auth0/auth0-react";

export default function LoginButton(){

    const {loginWithRedirect: login} = useAuth0();

    const signup = () => 
        login({ authorizationParams: { screen_hint: "signup" } });

    return (
        <button
            className="border-2 border-slate-500 rounded-md px-4 py-1 bg-blue-900 text-slate-300 hover:bg-blue-800 transition duration-100 ease-in-out active:border-blue-600"
            onClick={signup}>
            Login
        </button>
    );
}