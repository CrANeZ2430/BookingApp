import { createBrowserRouter } from "react-router";
import { RouterProvider } from "react-router/dom";
import Layout from "./layout/Layout";
import Home from "./router/home/Home";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import ErrorPage from "./router/error/ErrorPage";
import Rooms from "./router/room/Rooms";
import RoomTypes from "./router/roomType/RoomTypes";

export default function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      errorElement: <ErrorPage />,
      children: [
        {
          path: "/",
          element: <Home />
        },
        // {
        //   path: "/member",
        //   element: <Members />
        // },
        {
          path: "/rooms",
          element: <Rooms />
        },
        {
          path: "/room-types",
          element: <RoomTypes />
        }
      ]
    }
  ]);

  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router}/>
    </QueryClientProvider>
  );
};
