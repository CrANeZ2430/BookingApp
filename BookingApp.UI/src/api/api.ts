import axios from "axios";

export const api = axios.create(
    {
        baseURL: "https://localhost:7079/api",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDc5IiwiaWF0IjoxNzgyNDY4NzY2LCJleHAiOjE3ODI1NTUxNjYsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyIsImF6cCI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyIn0.KYIgrpp14snzmiiEC_5uL84fGCOH0YiBnVc-9E6HHBUA-O6e3QKcjPfkKyNS4NhvDV5akL93Wvp_mhUcIIh4Q5LImOMo3vkIvbslcgl1PlT23gnj2KZgUjihLhs4ZkPPEXNXkC9KjYNGgkn6A7DxzLI4zJ5MRqahOXLWYq2sRhlQV9zQZbl5SZAW_WWwD1NDBi0ZC6p6YYr-NlPi-NKClnenABdCm_3RvYSeR4ejZlD61qor3ViOvZka4BTXsRMWK9nPnnMkuElznAfkZQ7DVyjrF0_MJfbBRYoLA0JTuLZL84bO4JVgTPcKse0L5WzY1nbzy7-1kED9asACjKVpIg"
        }
    }
);