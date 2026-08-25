import { useAuth0 } from "@auth0/auth0-react";

export default function Profile(){

    const {user, isAuthenticated} = useAuth0();

    return (isAuthenticated ? (<div>
        <img className="rounded-full border-3 border-slate-100"
            src={user?.picture}/>
        <p>{user?.name}</p>
        <p>{user?.email}</p>
    </div>) : <div>You are not authorized</div>);
}