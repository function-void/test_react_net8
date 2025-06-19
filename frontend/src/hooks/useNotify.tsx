import { type OptionsObject, type SnackbarKey, useSnackbar } from "notistack";
import SnackMessage from "../components/SnackMessage";

export function useNotify() {
    const { enqueueSnackbar } = useSnackbar();

    return (message: string, options: OptionsObject = { variant: "info" }, response?: any): SnackbarKey => {
        let title = "";
        let errorId = "";
        let reason;
        if (response?.data.message === undefined) {
            reason = response?.data?.title === undefined ? response?.data : response.data?.title;
        } else {
            reason = response.data?.message;
        }

        if (response && options.variant === 'error') {
            errorId = response.data?.errorId;
            switch (response.status) {
                case 400:
                    title = 'badRequest'
                    message = reason ?? 'error400'
                    break;
                case 401:
                    title = 'unAuthorized'
                    message = reason ?? 'error401'
                    break;
                case 403:
                    title = 'forbidden'
                    message = reason ?? 'error403'
                    break;
                case 404:
                    title = 'notFound'
                    message = reason ?? 'error404'
                    break;
                case 500:
                    title = 'serverError'
                    message = reason ?? 'errorOccurred'
                    break;
                case 503:
                    title = 'serviceUnavailable'
                    message = reason ?? 'error503'
                    break;
                default:
                    title = response.statusText && response.statusText !== '' ? response.statusText : `${'errorCode'} ${response.status}`
                    message = reason ?? 'errorOccurred'
            }
        }

        if (response && options.variant === 'info') {
            title = message
            message = reason
        }

        if (response && options.variant === 'warning') {
            title = message
            message = reason
        }

        const key = enqueueSnackbar(message, {
            autoHideDuration: 6000,
            persist: options.variant === 'error',
            content: (key, message) => (
                <SnackMessage id={key} message={message} variant={options.variant} title={title} errorId={errorId} />
            )
        });
        
        return key;
    }
}