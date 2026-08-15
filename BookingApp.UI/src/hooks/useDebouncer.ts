import { useEffect, useState } from "react";

export default function useDebouncer<T>(
    filterVar:T,
    initialValue:T
    ) : T {

    const [debouncer, setDebouncer] = useState<T>(initialValue);

    useEffect(() => {
        const timer = setTimeout(() => 
            setDebouncer(filterVar), 400);

        return () => clearTimeout(timer);
    }, [filterVar]);

    return debouncer;
}