import { useAuth0 } from "@auth0/auth0-react";
import { useQuery } from "@tanstack/react-query";
import { jwtDecode } from "jwt-decode";

interface JWToken {
    permissions?:string[]
}

export default function usePermissions() {

    const { getAccessTokenSilently, isAuthenticated } = useAuth0();

    const { data: permissions = [], isLoading } = useQuery({
        queryKey: ["userPermissions"],
        queryFn: async () => {
            const token = await getAccessTokenSilently();
            const decodedToken = jwtDecode<JWToken>(token);
            return decodedToken.permissions || [];
        },
        enabled: isAuthenticated,
        staleTime: 1000 * 60 * 5
    });

    return { permissions, isLoading };
}