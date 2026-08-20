import { useMutation, useQueryClient } from "@tanstack/react-query";
import type Booking from "../../types/bookings/booking";
import { api } from "../../api/api";
import { toast } from "sonner";

interface BookingCardProps {

    index:number,
    booking:Booking
}

export default function BookingCard({index, booking}:BookingCardProps) {

    const queryClient = useQueryClient();

    const deleteMutation = useMutation({
        mutationFn: async (bookingId:string) => {
            await api.delete(`/bookings/${bookingId}`);
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({ queryKey: ["bookings"] });
        },
        onError: (error) => {
            console.error("Failed to delete booking:", error);
        }
    });

    return (
        <div className="flex justify-between border rounded-md p-4"
            key={booking.bookingId}>
            <div className="flex flex-col">
                <p className="font-bold">{`Booking #${index}`}</p>
                <p>{`${booking.attendeeCount} ${booking.attendeeCount === 1 ? "person" : "people"}`}</p>
                <p>{`from ${booking.startTime.slice(0, 10)} ${booking.startTime.slice(11, 16)}`}</p>
                <p>{`till ${booking.endTime.slice(0, 10)} ${booking.endTime.slice(11, 16)}`}</p>
                <p>{`created at ${booking.createdAt.slice(0, 10)} ${booking.createdAt.slice(11, 16)}`}</p>
            </div>
            <div className="flex flex-col">
                <button className="border-2 border-slate-500 rounded-md px-4 py-1 bg-slate-800 text-slate-300 hover:bg-slate-700 transition duration-100 ease-in-out active:border-blue-600"
                    onClick={(e) => {
                        toast("Do you want to delete this booking?", {
                            toasterId: "delete",
                            action: {
                                label: "Ok",
                                onClick: () => {
                                    deleteMutation.mutate(booking.bookingId);
                                    e.stopPropagation();
                                }
                            },
                            cancel: {
                                label: "Cancel",
                                onClick: () => {
                                    e.stopPropagation();
                                }
                            }
                        })
                    }}>
                    Delete
                </button>
            </div>
        </div>
    );
}