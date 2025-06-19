import axios, {type AxiosInstance} from "axios";
import environment from "../helpers/environment";

const axiosInstance = axios.create({
    baseURL: environment.apiUrl,
    withCredentials: true,
    headers: { "Content-Type": "application/json" }
});

axiosInstance.interceptors.request.use(
    async (config) => {
        return config;
    },
    (error) => Promise.reject(error)
);

export abstract class BaseApiService {
    protected abstract prefixUrl: string;
    protected api: AxiosInstance = axiosInstance
    protected baseUrl = environment.apiUrl;
}