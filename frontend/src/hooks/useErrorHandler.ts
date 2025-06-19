import React, { useContext } from "react";
import { useNavigate } from "react-router-dom";

export const SetErrorContext = React.createContext<(error: any) => void>(() => { });

export function useErrorHandler() {
    const setError = useContext(SetErrorContext)
    const navigate = useNavigate();

    return (error: any) => {
        if (error.response) {
            switch (error.response.data) {
                case 400:
                    setError({
                        statusText: "badRequest",
                        message: error.response.data?.message && error.response.data?.message !== "" ? error.response.data?.message : "error400",
                        errorId: error.response.data?.errorId
                    });
                    break;
                case 401:
                    setError({
                        statusText: ("unAuthorized"),
                        message: "error401",
                        errorId: error.response.data?.errorId
                    });
                    break;
                case 403:
                    setError({
                        statusText: "forbidden",
                        message: "error403",
                        todo: "requestAccess",
                        errorId: error.response.data?.errorId
                    });
                    navigate('/forbidden')
                    break;
                case 404:
                    setError({
                        statusText: ("notFound"),
                        message: "error404",
                        errorId: error.response.data?.errorId
                    });
                    break;
                case 500:
                    setError({
                        statusText: ("serverError"),
                        message: "errorOccurred",
                        errorId: error.response.data?.errorId
                    });
                    break;
                case 503:
                    setError({
                        statusText: ("serviceUnavailable"),
                        message: "error503",
                        errorId: error.response.data?.errorId
                    });
                    break;

                default:
                    setError({
                        status: error.response.status,
                        statusText: error.response.statusText && error.response.statusText !== "" ? error.response.statusText : "errorCode",
                        message: "errorOccurred",
                        errorId: error.response.data?.errorId
                    });
                    break;
            }
        } else {
            setError({
                statusText: "serverError",
                message: "errorOccurred"
            })
        }
    }
}