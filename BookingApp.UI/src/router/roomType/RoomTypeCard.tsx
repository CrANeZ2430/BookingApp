import type RoomType from "../../types/roomTypes/roomType";

interface RoomTypeCardProps {
    roomType:RoomType
}

export default function RoomTypeCard({ roomType }:RoomTypeCardProps) {

    return (
        <div className="flex flex-col border rounded-md p-4"
            key={roomType.roomTypeId}>
            <p className="font-bold">{roomType.name}</p>
            <p>{roomType.description}</p>
        </div>
    );
}