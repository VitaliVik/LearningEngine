import { handleAction, handleActions } from 'redux-actions';
import { ReducerBuilder } from './reducerBuilder';
import { fetchThemes, getThemesFail, getThemeSuccess } from '../actions/fetchTheme';


interface Theme{
    isLoading: boolean,
    error: any,
    themes : []
}

const initialState: Theme = {
    isLoading: false,
    error: undefined,
    themes: []
}

const reducers = new ReducerBuilder<Theme>()
    .handle(fetchThemes, (state: Theme) => ({ ...state, isLoading: false}))
    .handle(getThemesFail, (state: Theme, action) => ({ ...state, error: action.payload }))
    .handle(getThemeSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        themes: action.payload.themes
    }))
    .build();

export const themes = handleActions<Theme, any>(reducers , initialState);