import { handleActions } from 'redux-actions';
import { registration, registrationFail, registrationSuccess } from '../actions/regitstration';
import { ReducerBuilder } from './reducerBuilder';

interface RegistrationInfo {
    isLoading: boolean,
    error: any
}

const initialState: RegistrationInfo = {
    isLoading: false,
    error: undefined
};

const reducers = new ReducerBuilder<RegistrationInfo>()
    .handle(registration, (state) => ({ ...state, isLoading: false, userName: "" }))
    .handle(registrationFail, (state, action) => ({ ...state, error: action.payload }))
    .handle(registrationSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        userName: action.payload.username
    }))
    .build();

export const registrationReducer = handleActions<RegistrationInfo, any>(reducers , initialState);

