import { handleActions } from 'redux-actions';
import { getToken, getTokenFail, getTokenSuccess } from '../actions/getToken';
import { ReducerBuilder } from './reducerBuilder';

interface Accounts {
    accessToken: string,
    userName: string,
    isLoading: boolean,
    error: string
}

const initialState: Accounts = {
    accessToken: "",
    userName: "",
    isLoading: false,
    error: ""
};

const reducers = new ReducerBuilder<Accounts>()
    .handle(getToken, (state) => ({ ...state, isLoading: false, userName: "" }))
    .handle(getTokenFail, (state, action) => ({ ...state, error: action.payload }))
    .handle(getTokenSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        accessToken: action.payload.accessToken,
        userName: action.payload.username
    }))
    .build();

export const accounts = handleActions<Accounts, any>(reducers , initialState);

