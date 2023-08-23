
export interface AdvancedSearchProps {
    onSearch: (searchParams: AdvancedSearchParams) => void;
}

export interface AdvancedSearchParams {
    startDate: string | null;
    endDate: string | null;
    location: string;
    ageGroup: number;
    carGroupId: number;
}
