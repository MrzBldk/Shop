import authService from "app/authService"
import { useEffect, useState } from "react"
import { Link } from "react-router-dom"

export function LoginMenu() {

    const [isAuthenticated, setAuthenticated] = useState(false)


    const populateState = async () => {
        setAuthenticated(await authService.isAuthenticated())
    }

    useEffect(() => {
        const subscription = authService.subscribe(populateState)
        populateState()
        return () => authService.unsubscribe(subscription)
    })

    return (
        <>
            {isAuthenticated ?
                <Link to="/logout">Logout</Link> :
                <Link to="/login">Login</Link>
            }
        </>
    )
}