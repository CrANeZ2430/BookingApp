import { NavLink } from "react-router";
import type Room from "../../types/rooms/room";

interface RoomCardProps {
    room:Room
}

export default function RoomCard({ room }:RoomCardProps){

    return (
        <div className="flex border rounded-md p-4 justify-between"
                key={room.roomId}>
            <div className="flex flex-col">
                <p className="font-bold">{room.name}</p>
                <p>{room.floor} floor</p>
                <p>max {room.capacity} {room.capacity === 1 ? "person" : "people"}</p>
                <p>{room.isOperational ? "Operational" : "In repair"}</p>
                <p>{room.roomType.name}</p>
            </div>
            <div className="flex flex-col">
                {room.isOperational ? 
                (<NavLink className="border-2 border-slate-500 rounded-md px-4 py-1 bg-slate-800 text-slate-300 hover:bg-slate-700 transition duration-100 ease-in-out active:border-blue-600"
                    to={`/rooms/${room.roomId}/booking`}>
                    Book
                </NavLink>) :
                (<button disabled
                    className="border-2 border-slate-600 rounded-md px-4 py-1 bg-slate-800 text-slate-400">
                    Book
                </button>)}
            </div>
        </div>
    );
}