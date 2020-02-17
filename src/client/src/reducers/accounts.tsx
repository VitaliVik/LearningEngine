import { act } from "react-dom/test-utils";
import axios from "axios";
const initialState = {token: "", username: ""};

export default async function accounts(state = initialState, action: {type: String, payload: {login:string, password:string}}) : Promise<{token:String, username:String}> {
    if (action.type === "USER_SIGNIN")
    {
        let formData: FormData = new FormData();
        formData.set("username", action.payload.login);
        formData.set("password", action.payload.password);
        let data = await axios.post("https://localhost:5001/api/account/token", formData)
            .then(res => res.data);
        let res = {token: data.access_token, username: data.username};
        return res;
    }
    return state;
}