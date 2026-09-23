// import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
// import type CheckMemberResponse from "../../types/checkMember/checkMemberResponse";
// import usePermissions from "../../hooks/usePermissions";
// import useApiClient from "../../api/useApiClient";

// interface MemberDto {
//     firstName: string | undefined,
//     lastName: string | undefined,
//     role: string,
//     email: string | undefined,
//     phoneNumber: string | undefined
// }

export default function Home(){

    // const queryClient = useQueryClient();
    // const api = useApiClient();

    // const {data:data} = useQuery<CheckMemberResponse>(
    //     {
    //         queryKey: ["currentMember"]
    //     });

    // const currentMember = data?.member;

    // const updateMutation = useMutation({
    //     mutationFn: async (memberRole:string) => {

    //         const payload:MemberDto = {
    //             firstName: currentMember?.firstName,
    //             lastName: currentMember?.lastName,
    //             role: memberRole,
    //             email: currentMember?.email,
    //             phoneNumber: currentMember?.phoneNumber
    //         }

    //         await api.put(`members/${currentMember?.memberId}`, payload);
    //     },
    //     onSuccess: async () => {

    //         await queryClient.invalidateQueries({ queryKey: ["bookings"] });
    //     },
    //     onError: (error) => {

    //         console.error("Failed to create booking:", error);
    //     }
    // });

    // const {permissions} = usePermissions();

    // if (currentMember?.role === "Customer" && permissions.includes("read:members")) {
    //     //console.log("Im here");
    //     updateMutation.mutate("Staff");
    // }
    // else if (currentMember?.role === "Staff" && !permissions.includes("read:members")) {
    //     updateMutation.mutate("Customer");
    // }

    return (
    <div>
        <p>Sweat home</p>
    </div>);
}