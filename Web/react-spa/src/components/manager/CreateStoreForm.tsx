import storeAPI from 'features/stores/storeAPI'
import { useState, FormEvent } from 'react'
import { toast } from 'react-hot-toast'

export function CreateStoreForm() {
    const [name, setName] = useState('')
    const [description, setDescription] = useState('')

    const handleDescription = (event: FormEvent<HTMLInputElement>) => {
        setDescription(event.currentTarget.value)
    }
    const handleName = (event: FormEvent<HTMLInputElement>) => {
        setName(event.currentTarget.value)
    }

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        try{
            await storeAPI.createStore(name, description)
            toast.success('Store created')
        } catch{
            toast.error('Store was not created')
        }
    }

    return (
        <form onSubmit={handleSubmit}>
            <label htmlFor="name">Name</label>
            <input onChange={handleName} required value={name} type='text' placeholder="Name" />
            <label htmlFor="description">Description</label>
            <input onChange={handleDescription} required value={description} type='text' placeholder="text" />
            <input type='submit'></input>
        </form>
    )
}