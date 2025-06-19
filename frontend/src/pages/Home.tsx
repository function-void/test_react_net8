import { UserTable } from "../components/UserTable";

export const Home: React.FC = () => {
    return <>
        <div className="Title">
            HOME
            <div>
                <UserTable/>
            </div>
        </div>
    </>
}