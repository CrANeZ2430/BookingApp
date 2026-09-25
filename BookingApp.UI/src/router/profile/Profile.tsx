import { useAuth0 } from "@auth0/auth0-react";
import useCurrentMember from "../../hooks/useCurrentMember";

export default function Profile(){

    const {user, isAuthenticated} = useAuth0();
    const { member } = useCurrentMember();

    return (isAuthenticated ? (<div>
        <img className="rounded-full border-3 border-slate-100"
            src={user?.picture}/>
        <p>{member?.firstName}</p>
        <p>{member?.lastName}</p>
        <p>{member?.role}</p>
        <p>{member?.email}</p>
        <p>{member?.phoneNumber}</p>
    </div>) : <div>You are not authorized</div>);
}