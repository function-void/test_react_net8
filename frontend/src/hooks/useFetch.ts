import type { AxiosResponse } from "axios";
import { useState, useEffect } from "react";
import { useNotify } from "./useNotify";
import { useErrorHandler } from "./useErrorHandler";

export function createRequest<T>(call: () => Promise<AxiosResponse<T>>, afterCall: (x: T) => void, setIsLoading: (boolean: boolean) => void) {
    let response: AxiosResponse<T> | undefined;
    return async function (reject: any) {
        try {
            setIsLoading(true);
            response = await call();
        } catch (error) {
            reject(error)
        } finally {
            setIsLoading(false);
        }
        if (response !== undefined) {
            if (response.status === 200)
                afterCall(response.data)
            else if (response.status === 201)
                afterCall(response.data)
            else if (response.status === 204)
                afterCall(response.data)
        }
    }
}

export function useFetch<T>(call: () => Promise<AxiosResponse<T>>, afterCall: (x: T) => void, showErrorView: boolean, dependencies: React.DependencyList = []) {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const notify = useNotify();
    const handleError = useErrorHandler();

     useEffect(() => {
        const sendRequest = createRequest(call, afterCall, setIsLoading);
        sendRequest((err: any) => showErrorView ? handleError(err) : notify('errorOccurred', { variant: 'error' }, err.response))
    }, dependencies)

    return isLoading;
}

export function usePromise<T>(promise: () => Promise<AxiosResponse<T>>, onSuccess: (data: T) => void, showErrorView: boolean): [() => Promise<void>, boolean] {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const notify = useNotify();

    const handleError = useErrorHandler();

    const sendRequest = async () => {
        try {
            setIsLoading(true)
            const response = await promise();
            if (response.status === 200) {
                onSuccess(response.data)
            }
        } catch (error: any) {
            if (showErrorView) {
                handleError(error)
            } else {
                notify('errorOccurred', { variant: 'error' }, error.response)
            }

        } finally {
            setIsLoading(false)
        }
    }

    return [sendRequest, isLoading];
}