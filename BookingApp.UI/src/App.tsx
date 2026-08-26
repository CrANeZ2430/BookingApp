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
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import ProfileSetupGuard from "./layout/ProfileSetupGuard";
import ProtectedTwo from "./layout/RequireAuthGuard";

export default function App() {

  const router = createBrowserRouter([
    {
      element: <ProfileSetupGuard />,
      errorElement: <ErrorPage />,
      children: [
        {
          path: "/",
          element: <Layout />,
          children: [
            {
              index: true,
              element: <Home />
            },
            {
              element: <ProtectedTwo />,
              children: [
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
                }
              ]
            }
          ]
        },
        {
          path: "/profile-setup",
          element: <ProfileSetup />
        }
      ]
    }
  ]);

  const queryClient = new QueryClient();

  return (
    <QueryClientProvider client={queryClient}>
      <RouterProvider router={router}/>
      <ReactQueryDevtools client={queryClient} initialIsOpen={false} />
    </QueryClientProvider>
  );
};
