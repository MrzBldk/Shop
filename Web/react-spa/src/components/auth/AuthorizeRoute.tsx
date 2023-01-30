import authService from "app/authService";
import React, { useEffect, useState } from "react";
import { Navigate } from "react-router-dom";

export function AuthorizeRoute({ element, path }: { element: React.Component, path: string }) {
    
    const [ready, setReady] = useState(false)
    const [authenticated, setAuthenticated] = useState(false)

    const authenticationChanged = async () => {
        setReady(false)
        setAuthenticated(false)
        populateAuthenticationState()
    }

    const populateAuthenticationState = async () => {
        const authenticated = await authService.isAuthenticated();
        setAuthenticated(authenticated)
        setReady(true)
    }

    useEffect(() => {
        const subscription = authService.subscribe(authenticationChanged)
        populateAuthenticationState()
        return () => authService.unsubscribe(subscription)
    })
    
    return (
        <>
            {!ready ?
                <div></div> :
                authenticated ?
                    element :
                    <Navigate replace to={`login?returnUrl=${path}`}></Navigate>
            }
        </>
    )
}