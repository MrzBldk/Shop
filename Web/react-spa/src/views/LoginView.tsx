import AuthService from "app/authService";
import { FormEvent, useState } from "react"

export function LoginView() {

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')

    const auth = new AuthService()

    const handleUsername = (event: FormEvent<HTMLInputElement>) => {
        setUsername(event.currentTarget.value)
    };

    const handlePassword = (event: FormEvent<HTMLInputElement>) => {
        setPassword(event.currentTarget.value)
    };

    const handleLogin = async () => {
        await auth.login(username, password)
    }

    const handleLogout = async () => {
        await auth.logout()
    }

    return (
        <>
            <input value={username} onChange={handleUsername}></input>
            <input value={password} onChange={handlePassword}></input>
            <button onClick={handleLogin}>Click me</button>
            <button onClick={handleLogout}>Dom't click me</button>
        </>
    )
}