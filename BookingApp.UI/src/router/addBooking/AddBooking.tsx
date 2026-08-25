import { useParams } from "react-router";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";
import mapApiErrors from "../../utilities/mapApiErrors";
import { toast } from "sonner";
import useApiClient from "../../api/api";

interface BookingDto {
    attendeeCount:number,
    startTime:string,
    endTime:string,
    roomId:string,
    memberId:string
}

export default function AddBooking() {

    const api = useApiClient();
    const { id:roomId } = useParams();
    const { data: data } = useQuery({queryKey:["currentMember"]});

    const [attendees, setAtendees] = useState<number | undefined>(undefined);
    const [startTime, setStartTime] = useState<Date | undefined>(undefined);
    const [endTime, setEndTime] = useState<Date | undefined>(undefined);
    const [errors, setErrors] = useState<Record<string, string[]>>({});

    const queryClient = useQueryClient();

    const createMutation = useMutation({
        mutationFn: async () => {

            const payload:BookingDto = {

                attendeeCount: attendees ? attendees : 0,
                startTime: startTime ? startTime.toISOString() : "",
                endTime: endTime ? endTime.toISOString() : "",
                roomId: roomId ? roomId : "",
                memberId: data.member.memberId
            };
            
            await api.post("bookings", payload);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["bookings"] });
            setErrors({});
            toast.success("Booking was created successfully!", {toasterId:"info"});
        },
        onError: (error) => {
            console.error("Failed to create booking:", error.response.data);
            const errors = mapApiErrors(error.response.data.errors as Record<string, string[]>)
            setErrors(errors);
            toast.error("You have errors!", {toasterId:"info"});
        }
    });

    const toLocalISOString = (date: Date) => {
            const tzOffset = date.getTimezoneOffset() * 60000;
            return new Date(date.getTime() - tzOffset).toISOString().slice(0, 16);
        };

    return (
        <div className="flex flex-col items-center gap-4 rounded-md border border-slate-700 bg-slate-800 p-3 text-sm text-slate-200">
            <div className="flex items-center gap-2">
                <label htmlFor="attendees" className="font-medium text-slate-400">Atendees number:</label>
                <input
                    id="attendees"
                    name="attendees"
                    type="number"
                    value={attendees}
                    onChange={(e) => {
                        e.stopPropagation();
                        const newValue = e.target.value === "" ? 
                            undefined : Number(e.target.value);
                        setAtendees(newValue);
                    }}
                    className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
                />
            </div>
            {errors["attendeeCount"] && 
            errors["attendeeCount"].map((value) => 
                <span className="text-red-500 font-bold">
                    {value}
                </span>)}
            <div className="flex items-center gap-2">
                <label htmlFor="startTime" className="font-medium text-slate-400">Start time:</label>
                <input
                    id="startTime"
                    name="startTime"
                    type="datetime-local"
                    value={startTime ? toLocalISOString(startTime) : ""}
                    onChange={(e) => {
                        e.stopPropagation();
                        const newValue = e.target.value;
                        if (newValue){
                            setStartTime(new Date(newValue));
                        }
                    }}
                    className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
                />
            </div>
            {errors["startTime"] && 
            errors["startTime"].map((value) => 
                <span className="text-red-500 font-bold">
                    {value}
                </span>)}
            <div className="flex items-center gap-2">
                <label htmlFor="endTime" className="font-medium text-slate-400">End time:</label>
                <input
                    id="endTime"
                    name="endTime"
                    type="datetime-local"
                    value={endTime ? toLocalISOString(endTime) : ""}
                    onChange={(e) => {
                        const newValue = e.target.value;
                        if (newValue){
                            setEndTime(new Date(newValue));
                        }
                        e.stopPropagation();
                    }}
                    className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
                />
            </div>
            {errors["endTime"] && 
            errors["endTime"].map((value) => 
                <span className="text-red-500 font-bold">
                    {value}
                </span>)}
            <button
                className="border-2 border-slate-500 rounded-md px-4 py-1 bg-slate-700 text-slate-300 hover:bg-slate-600 transition duration-100 ease-in-out active:border-blue-600"
                onClick={(e) => {
                    e.stopPropagation();
                    createMutation.mutate();
                }}>
                Make a booking
            </button>
        </div> 
    );
}