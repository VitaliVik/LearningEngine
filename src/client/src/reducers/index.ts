import {
    GET_TOKEN,
    GET_TOKEN_SUCCESS,
    GET_TOKEN_FAIL
} from '../actions/getToken';

import { combineReducers } from "redux";
import accounts from "./accounts";
import themes from "./themes";

export default combineReducers({
    accounts,
    themes
});