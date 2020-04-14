import { combineReducers } from "redux";
import { accounts } from "./accounts";
import { themes } from "./themes";
import {registrationReducer} from "./registration"

export default combineReducers({
    accounts,
    themes,
    registrationReducer
});