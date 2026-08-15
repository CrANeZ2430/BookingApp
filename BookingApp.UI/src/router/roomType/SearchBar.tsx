interface SearchBarProps {
  page:number,
  search:string,
  onSearchChange:(value:string)=>void
}

export default function SearchBar({
  page,
  search: searchTerm,
  onSearchChange: onSearchTermChange}
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
    </div>
   ) : <></>
  );
}