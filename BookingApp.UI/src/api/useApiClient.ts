import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";
import { useMemo } from "react";

export default function useApiClient(){

    const {getAccessTokenSilently} = useAuth0();

    const api = useMemo(() => {
        const instance = axios.create({
            baseURL: `${import.meta.env.VITE_API_URL}/api`,
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

        instance.interceptors.response.use(
            (response) => response,
            async (error) => {
                if (error.response?.status === 403) {
                    console.warn("Permission denied. Session claims may be stale.");
                }

                return Promise.reject(error);
            });

        return instance;
    }, [getAccessTokenSilently]);

    return api;
}