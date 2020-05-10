import { combineReducers } from "redux";
import { accounts } from "./accounts";
import { themes } from "./themes";
import { reducer as formReducer } from 'redux-form';

export default combineReducers({
    accounts: accounts,
    themes: themes,
    form: formReducer,
	fullInfoAboutTheme: fullInfoAboutTheme
});