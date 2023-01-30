import { ProductsFilter } from "components/catalog/ProductsFilter"
import { CatalogProductsList } from "components/catalog/CatalogProductsList"
import { SearchBar } from "components/catalog/SearchBar"
import { useState } from "react"


export function CatalogView() {

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
        <section className="container margin-top">
            <SearchBar searchTerm={searchTerm} onSearchTermChange={handleSearchTermChange} />
            <ProductsFilter selectedBrands={selectedBrands} selectedTypes={selectedTypes}
                onSelectedBrandsChange={handleSelectedBrandsChange}
                onSelectedTypesChange={handleSelectedTypesChange} />
            <CatalogProductsList filter={searchTerm} types={selectedTypes} brands={selectedBrands} />
        </section>
    )
}