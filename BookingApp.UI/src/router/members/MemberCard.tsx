import type Member from "../../types/members/member";

interface MemberCardProps {
    member: Member
}

export default function MemberCard({ member }:MemberCardProps) {

    return (
        <div className="flex flex-col border rounded-md p-4 justify-between"
                key={member.memberId}>
            <p>{`${member.firstName} ${member.lastName}`}</p>
            <p>{member.role}</p>
            <p>{member.email}</p>
            <p>{member.phoneNumber}</p>
        </div>);
}