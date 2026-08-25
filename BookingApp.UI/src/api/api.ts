import { useAuth0 } from "@auth0/auth0-react";
import axios from "axios";

export default function useApiClient(){

    const {getAccessTokenSilently} = useAuth0();

    const api = axios.create(
        {
            baseURL: "http://localhost:8080/api",
            headers: {
                "Content-Type": "application/json"
            }
        }
    );

    api.interceptors.request.use(async (config) => {
        const token = await getAccessTokenSilently();
        config.headers.Authorization = `Bearer ${token}`;
        return config;
    });

    return api;
}