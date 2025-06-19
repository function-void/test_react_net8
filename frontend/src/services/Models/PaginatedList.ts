 export interface PaginatedList<T> {
    items: T[];
    totalCount: number;
    totalPages: number;
    pageNumber: number;
}