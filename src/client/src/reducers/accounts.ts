import { handleActions } from 'redux-actions';
import { getToken, getTokenFail, getTokenSuccess } from '../actions/getToken';
import { ReducerBuilder } from './reducerBuilder';
import { registration, registrationFail, registrationSuccess } from '../actions/regitstration';

export interface Accounts {
    accessToken: string,
    userName: string,
    isLoading: boolean,
    error: any
}

const initialState: Accounts = {
    accessToken: '',
    userName: '',
    isLoading: false,
    error: undefined
};

const reducers = new ReducerBuilder<Accounts>()
    .handle(getToken, (state) => ({ ...state, isLoading: false, userName: '' }))
    .handle(getTokenFail, (state, action) => ({ ...state, error: action.payload }))
    .handle(getTokenSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        accessToken: action.payload.accessToken,
        userName: action.payload.userName
    }))
    .handle(registration, (state) => ({ ...state, isLoading: true, userName: "" }))
    .handle(registrationFail, (state, action) => ({ ...state, error: action.payload }))
    .handle(registrationSuccess, (_, action) => ({ 
        isLoading: false,
        error: '',
        userName: action.payload.userName,
        accessToken: action.payload.accessToken
    }))
    .build();

export const accounts = handleActions<Accounts, any>(reducers , initialState);

