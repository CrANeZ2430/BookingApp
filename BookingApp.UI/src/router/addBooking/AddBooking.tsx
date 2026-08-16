import { useParams } from "react-router";
import { api } from "../../api/api";
import type PageResponse from "../../types/pageResponse";
import type Member from "../../types/members/member";
import { useMutation, useQueryClient } from "@tanstack/react-query";
import { useState } from "react";

interface BookingDto {
    attendeeCount:number,
    startTime:string,
    endTime:string,
    roomId:string,
    memberId:string
}

export default function AddBooking() {

    const { id:roomId } = useParams();

    const [attendees, setAtendees] = useState<number | undefined>(undefined);
    const [startTime, setStartTime] = useState<Date | undefined>(undefined);
    const [endTime, setEndTime] = useState<Date | undefined>(undefined);

    const queryClient = useQueryClient();

    const createMutation = useMutation({
        mutationFn: async () => {

            //temporary
            const memberId = (await api.get<PageResponse<Member>>("members"))
                .data.data[0].memberId;

            const payload:BookingDto = {

                attendeeCount: attendees ? attendees : 0,
                startTime: startTime ? startTime.toISOString() : "",
                endTime: endTime ? endTime.toISOString() : "",
                roomId: roomId ? roomId : "",
                memberId: memberId
            };
            
            await api.post("bookings", payload);
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["bookings"] });
        },
        onError: (error) => {
            console.error("Failed to create booking:", error.response.data);
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
                        const newValue = e.target.value === "" ? 
                            undefined : Number(e.target.value);
                        setAtendees(newValue);
                        console.log(newValue);
                        e.stopPropagation();
                    }}
                    className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
                />
            </div>
            <div className="flex items-center gap-2">
                <label htmlFor="startTime" className="font-medium text-slate-400">Start time:</label>
                <input
                    id="startTime"
                    name="startTime"
                    type="datetime-local"
                    value={startTime ? toLocalISOString(startTime) : ""}
                    onChange={(e) => {
                        const newValue = e.target.value;
                        if (newValue){
                            setStartTime(new Date(newValue));
                        }
                        e.stopPropagation();
                    }}
                    className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
                />
            </div>
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
            <button
                className="border-2 border-slate-500 rounded-md px-4 py-1 bg-slate-700 text-slate-300 hover:bg-slate-600 transition duration-100 ease-in-out active:border-blue-600"
                onClick={(e) => {
                    createMutation.mutate();
                    e.stopPropagation();
                }}>
                Make a booking
            </button>
        </div> 
    );
}