interface PagingItemProps {
    page:number,
    pageSize:number,
    totalCount:number,
    onPageChange: (newPage:number) => void
}

export default function Pagingitem({page, pageSize, totalCount, onPageChange}:PagingItemProps) {

    return (
        <div className="items-center flex w-fit border rounded-md">
            <button 
                onClick={() => onPageChange(page - 1)}
                className="p-1 active:text-slate-100 disabled:text-slate-500" 
                disabled={page+1 === 1}>
                {"Prev"}
            </button>
            <div className="border-slate-300 border-x p-1 text-amber-600">
                {`${page+1}/${Math.ceil(totalCount/pageSize)}`}
            </div>
            <button 
                onClick={() => onPageChange(page + 1)}
                className="p-1 active:text-slate-100 disabled:text-slate-500" 
                disabled={page+1 === Math.ceil(totalCount/pageSize)}>
                {"Next"}
            </button>
        </div>
    );
}