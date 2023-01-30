import authService from "app/authService";
import { useQuery } from "app/hooks";
import { FormEvent, useState } from "react"
import { useNavigate } from "react-router-dom";

export function Login() {

    const query = useQuery();
    const returnUrl = query.get('returnUrl')
    const navigate = useNavigate()

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [showPass, setShowPass] = useState(false)

    const handleUsername = (event: FormEvent<HTMLInputElement>) => {
        setUsername(event.currentTarget.value)
    }

    const handlePassword = (event: FormEvent<HTMLInputElement>) => {
        setPassword(event.currentTarget.value)
    }

    const handleCheckbox = () => {
        setShowPass(!showPass)
    }

    const handleLogin = async () => {
        await authService.login(username, password)
        if (!!returnUrl) {
            navigate('/' + returnUrl)
        } else {
            navigate('/')
        }
    }

    return (
        <section className="small-container margin-top">
            <input placeholder="Username" type="text" value={username} onChange={handleUsername}></input>
            <input placeholder="Password" type={showPass ? 'text' : 'password'} value={password} onChange={handlePassword}></input>
            <label>
                <input checked={showPass} onChange={handleCheckbox} type="checkbox" /> Show Password
            </label>
            <button className="full-button" onClick={handleLogin}>Login</button>
        </section>
    )
}