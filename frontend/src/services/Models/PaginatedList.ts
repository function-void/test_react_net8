 export interface PaginatedList<T> {
    Items: T[];
    TotalCount: number;
    TotalPages: number;
    PageNumber: number;
}