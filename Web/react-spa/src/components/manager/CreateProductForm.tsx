import authService from "app/authService";
import { useAppDispatch, useAppSelector } from "app/hooks";
import { fetchBrandsAsync, selectBrands, selectBrandsStatus } from "features/brands/brandsSlice";
import ProductAPI from "features/products/productAPI";
import { fetchTypesAsync, selectTypes, selectTypesStatus } from "features/types/typesSlice";
import { useEffect, useState, useRef, FormEvent } from "react";
import { toast, Toaster } from "react-hot-toast";

export function CreateProductForm({ storeId }: { storeId: string }) {

    const dispatch = useAppDispatch()
    const brands = useAppSelector(selectBrands)
    const types = useAppSelector(selectTypes)
    const brandsStatus = useAppSelector(selectBrandsStatus)
    const typesStatus = useAppSelector(selectTypesStatus)


    const [state, setState] = useState<{ [index: string]: string }>({
        name: '',
        description: '',
        typeId: '',
        brandId: '',
        price: '',
        availableStock: ''
    })

    const handleChange = (event: FormEvent<HTMLInputElement> | FormEvent<HTMLSelectElement>) => {
        const newState = { ...state }
        const key = event.currentTarget.name
        newState[key] = event.currentTarget.value
        console.log(newState)
        setState(newState)
    }

    const handleSubmit = async (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        try {
            const id = await ProductAPI.createProduct({
                name: state.name,
                description: state.description,
                price: Number(state.price),
                availableStock: Number(state.availableStock),
                brandId: state.brandId,
                typeId: state.typeId,
                storeId
            })

            const token = await authService.getToken()
            const formdata = new FormData()
            for (let i = 0; i < ref.current!.files!.length; i++) {
                formdata.append('FormFiles', ref.current!.files![i])
                formdata.append('filesNames', ref.current!.files![i].name)
            }
            await fetch(process.env.REACT_APP_API_URL + `/api/c/picture/${id}`, {
                method: 'POST',
                headers: {
                    'Authorization': `Bearer ${token}`
                },
                body: formdata
            })
            toast.success('Order created')
        } catch {
            toast.error('Product was not created')
        }

    }

    const ref = useRef<HTMLInputElement>(null)

    useEffect(() => {
        if (brandsStatus === 'idle') {
            dispatch(fetchBrandsAsync())
        }
        if (typesStatus === 'idle') {
            dispatch(fetchTypesAsync())
        }
        if (state.brandId === '' && state.typeId === '' &&
            brandsStatus === 'succeeded' && typesStatus === 'succeeded') {
            const newState = { ...state }
            newState.brandId = brands[0].id
            newState.typeId = types[0].id
            setState(newState)
        }
    }, [brandsStatus, typesStatus, dispatch, state, brands, types])

    return (
        <form className="margin-bottom" onSubmit={handleSubmit}>
            <label htmlFor="name">Name</label>
            <input name="name" value={state.name} required onChange={handleChange} type='text' placeholder="Name" />
            <label htmlFor="description">Description</label>
            <input name="description" value={state.description} required onChange={handleChange} type='text' placeholder="Description" />
            <label htmlFor="brandId">Brand</label>
            <select name="brandId" value={state.brand} required onChange={handleChange}>
                {brands.map(brand => <option key={brand.id} value={brand.id}>{brand.name}</option>)}
            </select>
            <label htmlFor="typeId">Type</label>
            <select name="typeId" value={state.type} required onChange={handleChange}>
                {types.map(type => <option key={type.id} value={type.id}>{type.name}</option>)}
            </select>
            <label htmlFor="price">Price</label>
            <input name="price" value={state.price} required onChange={handleChange} type='number' placeholder="Price" />
            <label htmlFor="availableStock">Available Stock</label>
            <input name="availableStock" value={state.availableStock} required onChange={handleChange} type='number' placeholder="Available Stock" />
            <label htmlFor="photos">Photos</label>
            <input required type='file' accept="image/*" multiple ref={ref} />
            <input type='submit' />
            <Toaster position="bottom-right" reverseOrder={false} />
        </form>
    )
}