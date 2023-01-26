import AuthService from "app/auth/authService";
import { FormEvent, useState } from "react"

export function LoginView(){

    let [login, setLogin] = useState('')
    let [password, setPassword] = useState('')

    const handleLogin = (event: FormEvent<HTMLInputElement>) => {
        setLogin(event.currentTarget.value)
    };

    const handlePassword = (event: FormEvent<HTMLInputElement>) => {
        setPassword(event.currentTarget.value)
    };

    const handleClick = () => {
        let auth = new AuthService()
        console.log(auth.login(login, password))
    }

    return(
        <>
        <input value={login} onChange={handleLogin}></input>
        <input value={password} onChange={handlePassword}></input>
        <button onClick={handleClick}>Click me</button>
        </>
    )
}