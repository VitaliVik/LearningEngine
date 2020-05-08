import { combineReducers } from "redux";
import { accounts } from "./accounts";
import { themes, fullInfoAboutTheme } from "./themes";

export default combineReducers({
    accounts,
    themes,
    fullInfoAboutTheme
});