import { useAppDispatch, useAppSelector } from "app/hooks"
import { fetchBrandsAsync, selectBrands, selectBrandsStatus } from "features/brands/brandsSlice"
import { fetchTypesAsync, selectTypesStatus, selectTypes } from "features/types/typesSlice"
import { FormEvent, useEffect } from "react"

export interface ProductsFilterProps{
    onSelectedTypesChange: (value: Array<string>) => void
    onSelectedBrandsChange: (value: Array<string>) => void
    selectedTypes: Array<string>
    selectedBrands: Array<string>
}

export function ProductsFilter({onSelectedTypesChange, onSelectedBrandsChange, selectedTypes, selectedBrands} : ProductsFilterProps) {
    const dispatch = useAppDispatch()
    const brands = useAppSelector(selectBrands)
    const types = useAppSelector(selectTypes)
    const brandsStatus = useAppSelector(selectBrandsStatus)
    const typesStatus = useAppSelector(selectTypesStatus)

    const handleChange = (event : FormEvent<HTMLSelectElement>) => {
        const value = Array.from(event.currentTarget.selectedOptions, option => option.value);
        event.currentTarget.name === 'types' ? onSelectedTypesChange(value) : onSelectedBrandsChange(value)
    }

    useEffect(() => {
        if (brandsStatus === 'idle') {
            dispatch(fetchBrandsAsync())
        }
        if (typesStatus === 'idle') {
            dispatch(fetchTypesAsync())
        }
    }, [brandsStatus, typesStatus, dispatch])

    return (
        <>
            <select value={selectedBrands} name="brands" multiple onChange={handleChange}>
                {brands.map(brand => <option key={brand.id} value={brand.name}>{brand.name}</option>)}
            </select>
            <select value={selectedTypes} name="types" multiple onChange={handleChange}>
                {types.map(type => <option key={type.id} value={type.name}>{type.name}</option>)}
            </select>
        </>
    )
}