export default interface PageResponse<T> {
    page:number,
    pageSize:number,
    totalCount:number,
    data:T[]
}