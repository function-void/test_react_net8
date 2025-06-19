
import { SnackbarContent, useSnackbar, type VariantType } from "notistack";
import { forwardRef, useCallback } from "react";



const SnackMessage = forwardRef<HTMLDivElement, { id: string | number, message: string | React.ReactNode, variant: VariantType | undefined, title?: string, errorId?: string }>(
    (props, ref) => {
        const { closeSnackbar } = useSnackbar();

        const handleDismiss = useCallback(() => {
            closeSnackbar(props.id)
        }, [props.id, closeSnackbar])

        return (
            <SnackbarContent ref={ref} className={""}>
                <div>
                    <div>
                        Icon
                    </div>
                    <div>
                        Title
                        message
                    </div>
                    <div>
                        <button onClick={handleDismiss}>
                            Close
                        </button>
                    </div>
                </div>
            </SnackbarContent>
        )
    }
);

export default SnackMessage;