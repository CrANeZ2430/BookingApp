import { useQuery } from "@tanstack/react-query";
import { api } from "../../data/api";
import type Member from "../../data/member";

export default function Members() {

    const { data: members, isLoading } = useQuery({
    queryKey: ['members'],
    queryFn: async () => {
        const res = await api.get('/members');
        return res.data;
        }
    });

    if (isLoading) return <div>Loading...</div>;

    return (
        <div>
            {members?.map((member:Member) => (
                <p key={member.memberId}>{member.firstName}</p>
            ))}
        </div>
    );
}