import authService from "app/authService";
import { useQuery } from "app/hooks";
import { FormEvent, useState } from "react"
import { useNavigate } from "react-router-dom";

export function LoginView() {

    const query = useQuery();
    const returnUrl = query.get('returnUrl')
    const navigate = useNavigate()

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')

    const handleUsername = (event: FormEvent<HTMLInputElement>) => {
        setUsername(event.currentTarget.value)
    };

    const handlePassword = (event: FormEvent<HTMLInputElement>) => {
        setPassword(event.currentTarget.value)
    };

    const handleLogin = async () => {
        await authService.login(username, password)
        if (!!returnUrl) {
            navigate('/' + returnUrl)
        } else {
            navigate('/')
        }
    }

    const handleLogout = async () => {
        await authService.logout()
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