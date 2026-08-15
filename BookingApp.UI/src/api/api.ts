import axios from "axios";

export const api = axios.create(
    {
        baseURL: "http://localhost:8080/api",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6Ikh4V0ZGZFdnOG4xQVZLT05qSGFPZW1MYThyRVU0MHNyQGNsaWVudHMiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJpYXQiOjE3ODY4MDUxNjcsImV4cCI6MTc4Njg5MTU2NywiZ3R5IjoiY2xpZW50LWNyZWRlbnRpYWxzIiwiYXpwIjoiSHhXRkZkV2c4bjFBVktPTmpIYU9lbUxhOHJFVTQwc3IifQ.M0Tb__1KYwBXxjzoxZocS1xCHVrpPkIc3gNDgMSxkkcFRZVJXV_sChF1ukL87HnoiqOnX2a4WosQY5cXJQqpSaMy6RRqXWjl0WRbLWdmhvnuVfwPc8DNybSjpWKqbTvbZtD0Rn4L2Y-TedXKY3jAz_Lba4g_vR_gr8d-fnCeUgenR83nWlM6hQe3xY4neG7EzhgV0IVXMOczq3puJvggmOi8I2QrIDVbRDW-qLipcVfrYRZ193Z5x2B7oMC-pcJm2J1dD-huV8AKeumTiSg26i1R8EKf8qhbUmPjS3ymipTO4G6axt6ZGbGxPqDgUx_yJkcwXndokfJWapqA_a-x0w"

        }
    }
);