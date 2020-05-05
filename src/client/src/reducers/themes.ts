import { handleAction, handleActions } from 'redux-actions';
import { ReducerBuilder } from './reducerBuilder';
import { fetchThemes, getThemesFail, getThemeSuccess, fetchSubThemes } from '../actions/fetchTheme';


interface Theme{
    isLoading: boolean,
    error: any,
    themes : [],
    isRoot : boolean
}

const initialState: Theme = {
    isLoading: false,
    error: undefined,
    isRoot : false,
    themes: []
}

const reducers = new ReducerBuilder<Theme>()
    .handle(fetchThemes, (state: Theme) => ({ ...state, isLoading: false}))
    .handle(getThemesFail, (state: Theme, action) => ({ ...state, error: action.payload }))
    .handle(fetchSubThemes, (state: Theme, action) => ({ ...state, isLoading: false}))
    .handle(getThemeSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        themes: action.payload.themes,
        isRoot : action.payload.isRoot
    }))
    .build();

export const themes = handleActions<Theme, any>(reducers , initialState);