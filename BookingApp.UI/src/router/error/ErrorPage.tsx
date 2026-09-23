import { AxiosError } from "axios";
import { isRouteErrorResponse, useRouteError } from "react-router";

export default function ErrorPage() {

    const error = useRouteError();

    const isForbidden = 
        (isRouteErrorResponse(error) && error.status === 403) ||
        (error instanceof AxiosError && error.response?.status === 403);

    if (isForbidden){

        return (
            <div className="flex flex-col min-h-screen w-full bg-slate-900 text-white">
                <p className="text-4xl">403</p>
                <p>You don't have appropriate permissions, buddy!</p>
            </div>
        );
    }

    return (
        <div className="flex flex-col min-h-screen w-full bg-slate-900 text-white">
            <p>An unexpected error has happened!</p>
        </div>
    );
}