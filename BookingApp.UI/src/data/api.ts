import axios from "axios";

export const api = axios.create(
    {
        baseURL: "https://localhost:7079/api",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjNNS1ktTkV0aVZ2MEFXX1libkYtciJ9.eyJpc3MiOiJodHRwczovL2Rldi1jcm4uZXUuYXV0aDAuY29tLyIsInN1YiI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyQGNsaWVudHMiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo3MDc5IiwiaWF0IjoxNzgyMzIyMjI2LCJleHAiOjE3ODI0MDg2MjYsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyIsImF6cCI6IlFjcDhXRHZDaEk1bms5V01qaEV3Ym1nRzhxVXBMaXFyIn0.JdFSl_u0f0i5Hg5trREsnG4FgThsjJ0MrVU157EHWhowcsIh0B53fx5DcbEXKSRc4HpuqxAAMk04pL2QaBAVUUQV0KZ-EBSIZwL90E0HqJTq5vwMks7ULKSTIL7c1Sdr04sgQqpHXyumTzpO60hsBluTlIuxdJAqoU2Wo5LVDdy21VUZh0WsOYesPjYqOG-ufwVID0e5mPxWkG09fIRGvy9uFzsXNHBEqCk1EaDOrrvFtsQQffWbZAZaqjkyQFiNnHEPrcS-cCroJNFqYGqdOPAyR4_WH6aS_Ct5Dw82XXkC_IqV4cq9FjTFae5iwJVNTm91PDZzVbnmgJh7LvEpJg"
        }
    }
);