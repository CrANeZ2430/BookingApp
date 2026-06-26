interface MemberRoleProps {
    role: string;
}

export default function MemberRole({ role }: MemberRoleProps) {
    const normalizedRole = role?.trim().toLowerCase();

    const roleStyles: Record<string, string> = {
        staff: "bg-orange-500/10 text-orange-400 border-orange-500/20",
        customer: "bg-emerald-500/10 text-emerald-400 border-emerald-500/20"
    };

    const currentStyle = roleStyles[normalizedRole] || "bg-slate-500/10 text-slate-400 border-slate-500/20";

    return (
        <div className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium border ${currentStyle}`}>
            <p className="tracking-wide">{role || "Unknown"}</p>
        </div>
    );
}