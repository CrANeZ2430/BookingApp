export default interface CheckMemberResponse {

    profileExists:boolean,
    member:Member
}

interface Member {

    memberId:string,
    auth0Id:string,
    firstName:string,
    lastName:string,
    role:string,
    email:string,
    phoneNumber:string
}