import axios from "axios";

export const api = axios.create(
    {
        baseURL: "http://localhost:8080/api",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6Ikh4V0ZGZFdnOG4xQVZLT05qSGFPZW1MYThyRVU0MHNyQGNsaWVudHMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJpYXQiOjE3ODY4OTM2ODQsImV4cCI6MTc4Njk4MDA4NCwiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiSHhXRkZkV2c4bjFBVktPTmpIYU9lbUxhOHJFVTQwc3IifQ.dNzLJJ9TSWVh9JoTREa7idZQ4B7iAXYAUcsGwoSP82nhAXBKrPehrnP1kXyh_CPa31v9FvJv0RrWaYmQJ0EUUbyIgY64bsoVsS7258QKAJXIy1yk8lmqN6rmT43j4nPb8N6v72cHdw0b1ORBGj_K7REk_agPO-eFyP4dDPjrE2lX8wPkCvPbCBcx6NDG2LyFN-OODzI_HQLR5sfBdGjp0OD7BKwZgVwIU4ZaaL-MVRYrmhuL-6fnwijNpVVNN-XkqLTfFScV7flCZXu4vfHNJBR1wL2H6qz-bpVr_h5GYHAkUpzKbjpAB2nLQ5q6ja2r0SkqecyFnyMCYq9K0iuNvA"

        }
    }
);