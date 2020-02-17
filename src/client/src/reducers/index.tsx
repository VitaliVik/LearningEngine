import { combineReducers } from "redux";
import accounts from "./accounts";
import themes from "./themes";

export default combineReducers({
    accounts,
    themes
});