interface SearchBarProps {
  page:number,
  search:string,
  onSearchChange:(value:string)=>void,
  minCap:number | undefined,
  onMinCapChange:(value:number | undefined)=> void
  isOp:boolean,
  onIsOpChange:(value:boolean)=>void
}

export default function SearchBar({
  page,
  search: searchTerm,
  onSearchChange: onSearchTermChange,
  minCap,
  onMinCapChange,
  isOp,
  onIsOpChange}
  :SearchBarProps) {

  return (
   page === 0 ? (
    <div className="flex flex-wrap items-center gap-4 rounded-md border border-slate-700 bg-slate-800 p-3 text-sm text-slate-200">
      <div className="flex items-center gap-2">
        <label htmlFor="searchTerm" className="font-medium text-slate-400">Search term:</label>
        <input
          id="searchTerm"
          name="searchTerm"
          type="text"
          value={searchTerm}
          onChange={(e) => {
            onSearchTermChange(e.target.value);
            e.stopPropagation();
          }}
          className="rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
        />
      </div>

      <div className="flex items-center gap-2">
        <label htmlFor="minCap" className="font-medium text-slate-400">Min capacity:</label>
        <input
          id="minCap"
          name="minCap"
          type="number"
          min="0"
          value={minCap}
          onChange={(e) => {
            const newValue = e.target.value === "" ? 
              undefined : Number(e.target.value);
            onMinCapChange(newValue);
            e.stopPropagation();
          }}
          className="w-16 rounded border border-slate-600 bg-slate-900 px-2 py-1 text-slate-100 focus:border-blue-500 focus:outline-none"
        />
      </div>

      <div className="flex items-center gap-2">
        <input
          id="isOp"
          name="isOp"
          type="checkbox"
          checked={isOp}
          onChange={(e) => {
            onIsOpChange(e.target.checked);
            e.stopPropagation();
          }}
          className="h-4 w-4 rounded border-slate-600 bg-slate-900 accent-blue-600"
        />
        <label htmlFor="isOp" className="cursor-pointer font-medium text-slate-400 select-none">Is available</label>
      </div>
    </div>
   ) : <></>
  );
}