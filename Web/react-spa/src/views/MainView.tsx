import { ProductsFilter } from "components/ProductsFilter"
import { MainProductsList } from "components/MainProductsList"
import { SearchBar } from "components/SearchBar"
import { useState } from "react"


export function MainView() {

    const [searchTerm, updateSearchTerm] = useState('')
    const [selectedTypes, setSelectedTypes] = useState(new Array<string>())
    const [selectedBrands, setSelectedBrands] = useState(new Array<string>())

    const handleSearchTermChange = (searchTerm: string) => {
        updateSearchTerm(searchTerm)
    }

    const handleSelectedTypesChange = (types: Array<string>) => {
        setSelectedTypes(types)
    }

    const handleSelectedBrandsChange = (brands: Array<string>) => {
        setSelectedBrands(brands)
    }

    return (
        <>
            <SearchBar searchTerm={searchTerm} onSearchTermChange={handleSearchTermChange} />
            <ProductsFilter selectedBrands={selectedBrands} selectedTypes={selectedTypes}
                onSelectedBrandsChange={handleSelectedBrandsChange}
                onSelectedTypesChange={handleSelectedTypesChange} />
            <MainProductsList filter={searchTerm} types={selectedTypes} brands={selectedBrands} />
        </>
    )
}