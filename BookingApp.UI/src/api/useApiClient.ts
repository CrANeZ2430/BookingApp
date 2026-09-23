import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useMemo } from "react";

export default function useApiClient(){

    const {getAccessTokenSilently} = useAuth0();

    const api = useMemo(() => {
        const instance = axios.create({
                baseURL: "https://localhost/api",
                headers: {
                    "Content-Type": "application/json"
                }
            });

        instance.interceptors.request.use(async (config) => {
            const token = await getAccessTokenSilently();
            //console.log(token);
            config.headers.Authorization = `Bearer ${token}`;
            return config;
        });

        return instance;
    }, [getAccessTokenSilently]);

    return api;
}