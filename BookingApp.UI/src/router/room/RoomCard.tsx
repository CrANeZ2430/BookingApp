import type Room from "../../types/rooms/room";

interface RoomCardProps {
    room:Room
}

export default function RoomCard({ room }:RoomCardProps){

    return (
        <div className="flex flex-col border rounded-md p-4"
                key={room.roomId}>
            <p className="font-bold">{room.name}</p>
            <p>{room.floor} floor</p>
            <p>max {room.capacity} {room.capacity === 1 ? "person" : "people"}</p>
            <p>{room.isOperational ? "Operational" : "In repair"}</p>
            <p>{room.roomType.name}</p>
        </div>
    );
}