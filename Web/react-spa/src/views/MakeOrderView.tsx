import authService from "app/authService"
import { Toaster, toast } from 'react-hot-toast'
import { FormEvent, useState } from 'react'

export function MakeOrderView() {


    const [state, setState] = useState<{ [index: string]: string }>({
        street: '',
        city: '',
        state: '',
        country: '',
        zipCode: ''
    })

    const makeOrder = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        console.log(state)
        const token = await authService.getToken()
        const id = await authService.getUserId()
        const url = process.env.REACT_APP_API_URL + `/api/agg/order/create/${id}`
        const responce = await fetch(url, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`,
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                street: state.street,
                city: state.city,
                state: state.state,
                country: state.country,
                zipCode: state.zipCode
            })
        })
        if (responce.ok) {
            toast.success('Order was made')
        } else {
            toast.error(await responce.text())
        }
    }

    const handleChange = (event: FormEvent<HTMLInputElement>) => {
        const newState = { ...state }
        const key = event.currentTarget.name
        newState[key] = event.currentTarget.value
        setState(newState)
    }

    return (<section className="small-container">
        <form onSubmit={makeOrder}>
            <label htmlFor='country'>Country</label>
            <input required onChange={handleChange} type='text' placeholder='country' name="country" id='country'></input>
            <label htmlFor='state'>State</label>
            <input required onChange={handleChange} type='text' placeholder='state' name="state" id='state'></input>
            <label htmlFor='city'>City</label>
            <input required onChange={handleChange} type='text' placeholder='city' name="city" id='city'></input>
            <label htmlFor='street'>Street</label>
            <input required onChange={handleChange} type='text' placeholder='street' name="street" id='street'></input>
            <label htmlFor='zipCode'>Zip Code</label>
            <input required onChange={handleChange} type='number' placeholder='zipCode' name="zipCode" id='zipCode'></input>
            <input type='submit' className="full-button"></input>
        </form>
        <Toaster
            position="bottom-right"
            reverseOrder={false}
        />
    </section>
    )
}