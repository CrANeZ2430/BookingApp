import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "@tanstack/react-query";

export default function Profile(){

    const {user, isAuthenticated} = useAuth0();
    const {data:data} = useQuery({queryKey: ["currentMember"]});

    return (isAuthenticated ? (<div>
        <img className="rounded-full border-3 border-slate-100"
            src={user?.picture}/>
        <p>{data.member.firstName}</p>
        <p>{data.member.lastName}</p>
        <p>{data.member.role}</p>
        <p>{data.member.email}</p>
        <p>{data.member.phoneNumber}</p>
    </div>) : <div>You are not authorized</div>);
}