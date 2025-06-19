import { useState } from "react";
import { usePromise } from "../hooks/useFetch";
import { api } from "../services/api";
import type { PaginatedList } from "../services/Models/PaginatedList";
import type { User } from "../services/Models/User";


export const UserTable: React.FC = () => {

    const [search, setSearch] = useState<string>('');
    const [pageNumber, setPageNumber] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(5);
    const [paginatedList, setPaginatedList] = useState<PaginatedList<User>>({ items: [], totalCount: 0, totalPages: 0, pageNumber: 0 });

    const [seachClick, isLoading] = usePromise(
        () => api.users.getUsers(search, pageNumber, pageSize),
        (response: PaginatedList<User>) => {
            setPaginatedList({...response, items: [...response.items]})
        },
        false)

    const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearch(event.target.value);
    };

    const handlePrev = (_: any) => {
        setPageNumber(prev => prev - 1)
        seachClick()
    };

    const handleNext = (_: any) => {
        setPageNumber(prev => prev + 1)
        seachClick()
    };

    const changePageSize = (number: number) => {
        setPageSize(number)
    };

    return <>
        <div className="max-w-[720px] mx-auto">
            <div className="w-full flex justify-between items-center mb-3 mt-1 pl-3">
                <div>
                    <h3 className="text-lg font-semibold text-slate-800">User table</h3>

                </div>
                <div className="ml-3">
                    <div className="w-full max-w-sm min-w-[200px] relative">
                        <div className="relative">
                            <input
                                onChange={handleChange}
                                className="bg-white w-full pr-11 h-10 pl-3 py-2 bg-transparent placeholder:text-slate-400 text-slate-700 text-sm border border-slate-200 rounded transition duration-200 ease focus:outline-none focus:border-slate-400 hover:border-slate-400 shadow-sm focus:shadow-md"
                                placeholder="Search ..."
                            />
                            <button
                                onClick={seachClick}
                                className="absolute h-8 w-8 right-1 top-1 my-auto px-2 flex items-center bg-white rounded "
                                type="button"
                            >
                                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="3" stroke="currentColor" className="w-8 h-8 text-slate-600">
                                    <path stroke-linecap="round" stroke-linejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
                                </svg>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <div className="relative flex flex-col w-full h-full overflow-scroll text-gray-700 bg-white shadow-md rounded-lg bg-clip-border">
                <table className="w-full text-left table-auto min-w-max">
                    <thead>
                        <tr>
                            <th className="p-4 border-b border-slate-200 bg-slate-50">
                                <p className="text-sm font-normal leading-none text-slate-500">
                                    Id
                                </p>
                            </th>
                            <th className="p-4 border-b border-slate-200 bg-slate-50">
                                <p className="text-sm font-normal leading-none text-slate-500">
                                    Full name
                                </p>
                            </th>
                            <th className="p-4 border-b border-slate-200 bg-slate-50">
                                <p className="text-sm font-normal leading-none text-slate-500">
                                    Email
                                </p>
                            </th>
                            <th className="p-4 border-b border-slate-200 bg-slate-50">
                                <p className="text-sm font-normal leading-none text-slate-500">
                                    Role
                                </p>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            !isLoading && paginatedList?.items.map(x => (
                                <tr className="hover:bg-slate-50 border-b border-slate-200">
                                    <td className="p-4 py-5">
                                        <p className="block font-semibold text-sm text-slate-800">{x.id}</p>
                                    </td>
                                    <td className="p-4 py-5">
                                        <p className="text-sm text-slate-500">{x.fullName}</p>
                                    </td>
                                    <td className="p-4 py-5">
                                        <p className="text-sm text-slate-500">{x.email}</p>
                                    </td>
                                    <td className="p-4 py-5">
                                        <p className="text-sm text-slate-500">{x.role}</p>
                                    </td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>

                <div className="flex justify-between items-center px-4 py-3">
                    <div className="text-sm text-slate-500">
                        Showing <b>{pageNumber}-{paginatedList?.totalPages}</b> of {paginatedList?.totalCount}
                    </div>
                    <div className="flex space-x-1">
                        <button disabled={pageNumber <= 1} onClick={handlePrev} className="px-3 py-1 min-w-9 min-h-9 text-sm font-normal text-slate-500 bg-white border border-slate-200 rounded hover:bg-slate-50 hover:border-slate-400 transition duration-200 ease">
                            Prev
                        </button>
                        <button disabled={pageNumber >= paginatedList?.totalPages} onClick={handleNext} className="px-3 py-1 min-w-9 min-h-9 text-sm font-normal text-slate-500 bg-white border border-slate-200 rounded hover:bg-slate-50 hover:border-slate-400 transition duration-200 ease">
                            Next
                        </button>
                    </div>
                </div>
            </div>

        </div>
    </>
}
