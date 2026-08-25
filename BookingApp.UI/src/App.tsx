import { createBrowserRouter } from "react-router";
import { RouterProvider } from "react-router/dom";
import Layout from "./layout/Layout";
import Home from "./router/home/Home";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import ErrorPage from "./router/error/ErrorPage";
import Rooms from "./router/room/Rooms";
import RoomTypes from "./router/roomType/RoomTypes";
import Bookings from "./router/booking/Bookings";
import AddBooking from "./router/addBooking/AddBooking";
import ProfileSetup from "./router/profileSetup/ProfileSetup";
import Profile from "./router/profile/Profile";
import ProtectedRoute from "./layout/ProtectedRoute";

export default function App() {

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      errorElement: <ErrorPage />,
      children: [
        {
          path: "profile-setup",
          element: <ProfileSetup />,
        },
        {
          element: <ProtectedRoute />,
          children: [
            {
              path: "",
              element: <Home />,
            },
            {
              path: "profile",
              element: <Profile />,
            },
            {
              path: "rooms",
              element: <Rooms />,
            },
            {
              path: "room-types",
              element: <RoomTypes />,
            },
            {
              path: "bookings",
              element: <Bookings />,
            },
            {
              path: "rooms/:id/booking",
              element: <AddBooking />,
            },
          ],
        },
      ],
    },
  ]);

  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router}/>
    </QueryClientProvider>
  );
};
