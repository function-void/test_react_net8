import { BaseApiService } from "./BaseApiService";
import type { CreateUserRequest } from "./Models/CreateUserRequest";
import type { PaginatedList } from "./Models/PaginatedList";
import type { UpdateUserRequest } from "./Models/UpdateUserRequest";
import type { User } from "./Models/User";

export class UsersService extends BaseApiService {
    protected prefixUrl = "api/v1/users";

    getUsers(search: string | null, pageNumber: number, pageSize: number) {
        return this.api.get<PaginatedList<User>>(`${this.prefixUrl}`, {params: {search, pageNumber, pageSize }})
    }

    createUser(request: CreateUserRequest) {
        return this.api.post<string>(`${this.prefixUrl}`, request)
    }

    updateUser(id: string, request: UpdateUserRequest) {
        return this.api.put(`${this.prefixUrl}/${id}`, request)
    }

    deleteUser(id: string) {
        return this.api.delete(`${this.prefixUrl}/${id}`)
    }
}