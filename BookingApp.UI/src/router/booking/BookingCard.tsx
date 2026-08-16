import type Booking from "../../types/bookings/booking";

interface BookingCardProps {

    index:number,
    booking:Booking
}

export default function BookingCard({index, booking}:BookingCardProps) {

    return (
        <div className="flex flex-col border rounded-md p-4"
            key={booking.bookingId}>
            <p className="font-bold">{`Booking #${index}`}</p>
            <p>{`${booking.attendeeCount} ${booking.attendeeCount === 1 ? "person" : "people"}`}</p>
            <p>{`from ${booking.startTime.slice(0, 10)} ${booking.startTime.slice(11, 16)}`}</p>
            <p>{`till ${booking.endTime.slice(0, 10)} ${booking.endTime.slice(11, 16)}`}</p>
            <p>{`created at ${booking.createdAt.slice(0, 10)} ${booking.createdAt.slice(11, 16)}`}</p>
        </div>
    );
}