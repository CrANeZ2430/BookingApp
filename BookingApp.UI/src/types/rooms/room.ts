import type RoomType from "./roomType";

export default interface Room {
    roomId:string,
    name:string,
    floor:number,
    capacity:number,
    isOperational:boolean,
    roomType:RoomType
};