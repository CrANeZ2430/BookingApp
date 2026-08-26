import { useMutation, useQueryClient } from "@tanstack/react-query";
import useApiClient from "../../api/api";
import { useState } from "react";
import { toast } from "sonner";
import { useNavigate } from "react-router";
import { useAuth0 } from "@auth0/auth0-react";

interface SyncMemberRequest{
    firstName:string,
    lastName:string,
    phoneNumber:string
}

export default function ProfileSetup(){

    const queryClient = useQueryClient();
    const api = useApiClient();
    const navigate = useNavigate();
    const {user} = useAuth0();

    const createMutation = useMutation({
        mutationFn: async () => {
                const request:SyncMemberRequest = {
                firstName: fName,
                lastName: lName,
                phoneNumber: phone
            }
            console.log(request);
            await api.post("members/sync", request);
        },
        onSuccess: async () => {
          queryClient.setQueryData(["currentMember"], {profileExists:true, 
            member:{
              firstName:fName, 
              lastName:lName, 
              role:"Customer", 
              email:user?.email, 
              phoneNumber:phone}});
          toast.success("The profile was created successfully!", {toasterId:"info"});
          await queryClient.invalidateQueries({ queryKey: ["currentMember"] });
        },
        onError: (error) => {
          console.error("Failed to create member:", error);
          toast.error("You have errors!", {toasterId:"info"});
        }
    });

    const [fName, setFName] = useState("");
    const [lName, setLName] = useState("");
    const [phone, setPhone] = useState("");

    return (
    <div className="flex flex-col h-screen w-full bg-slate-900 text-slate-300 overflow-hidden">
      <div className="flex flex-wrap items-center gap-4 rounded-md border border-slate-700 bg-slate-800 p-3 text-sm text-slate-200">
      <div className="flex items-center gap-2">
        <label htmlFor="fName" className="font-medium text-slate-400">First name</label>
        <input
          id="fName"
          name="fName"
          type="text"
          value={fName}
          onChange={(e) => {
            e.stopPropagation();
            setFName(e.target.value);
          }}
          className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
        />
      </div>

      <div className="flex items-center gap-2">
        <label htmlFor="lName" className="font-medium text-slate-400">Last name</label>
        <input
          id="lName"
          name="lName"
          type="text"
          value={lName}
          onChange={(e) => {
            e.stopPropagation();
            setLName(e.target.value);
          }}
          className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
        />
      </div>

      <div className="flex items-center gap-2">
        <label htmlFor="phone" className="font-medium text-slate-400">Phone number</label>
        <input
          id="phone"
          name="phone"
          type="text"
          value={phone}
          onChange={(e) => {
            e.stopPropagation();
            setPhone(e.target.value);
          }}
          className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
        />
      </div>

        <button
            onClick={(e) => {
                e.stopPropagation();
                createMutation.mutate();
            }}
            className="border-2 border-slate-500 rounded-md px-4 py-1 bg-slate-700 text-slate-300 hover:bg-slate-600 transition duration-100 ease-in-out active:border-blue-600">            
            Add
        </button>
      </div>
    </div>);
}