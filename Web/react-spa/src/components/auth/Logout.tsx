import authService from "app/authService";
import { useEffect } from "react";
import { useNavigate } from "react-router-dom";

export function Logout() {

    const navigate = useNavigate()

    useEffect(() => {
        authService.logout()
        navigate('/')
    })

    return <></>
}