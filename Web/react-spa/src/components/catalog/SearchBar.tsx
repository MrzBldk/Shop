import { FormEvent } from "react";

export interface SearchBarProps {
    onSearchTermChange: (value: string) => void
    searchTerm: string
}

export function SearchBar({ onSearchTermChange, searchTerm }: SearchBarProps,) {

    const handleSearch = (event: FormEvent<HTMLInputElement>) => {
        onSearchTermChange(event.currentTarget.value)
    };

    return <input
        onChange={handleSearch}
        value={searchTerm}
        type="text"
        placeholder="Search" />
}