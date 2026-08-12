import axios from "axios";

export const api = axios.create(
    {
        baseURL: "http://localhost:8080/api",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6Ikh4V0ZGZFdnOG4xQVZLT05qSGFPZW1MYThyRVU0MHNyQGNsaWVudHMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJpYXQiOjE3ODY1NDI5NTMsImV4cCI6MTc4NjYyOTM1MywiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiSHhXRkZkV2c4bjFBVktPTmpIYU9lbUxhOHJFVTQwc3IifQ.rV0KSgRl2lfwMiODjn3ctUv25EVGjg7lX9d3ltNrqIpH4ayX4ecvbxyXh2WswLEskUS36IZfWiTgYDxSPxpwdtpy2rkNsJx4xyd4WMBat05wc3XLkTDfUTXhWi7Xca7jSsR6-QTEzw6ky3kpi1Q1KTAS1hJH3POUnJylBBtwzg0RRXTUAmaNhHu5IRr40LHe-ixZ69VQXLJAY-w6ubab27sQmdTUxoLDS062brsDmjw7Jq8hl_Su5sHWW9LzK0Ka9-zlrvIIjuCg9Xi1a77-ctrAMIKCtI4gR_oXlBEQ-sNdJQbv0kIfjaSUXvFKQi8rKydhWbqCevTPfKXGlzeMgw"

        }
    }
);