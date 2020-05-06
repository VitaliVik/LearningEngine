import { handleAction, handleActions } from 'redux-actions';
import { ReducerBuilder } from './reducerBuilder';
import { fetchThemes, getThemesFail, getThemeSuccess, fetchFullInfo, getFullInfoAboutThemeSuccess } from '../actions/fetchTheme';


interface Theme{
    isLoading: boolean,
    error: any,
    themes : [],
    isRoot : boolean
}

interface FullInfo{
    isLoading: boolean,
    error: any,    
    id : Number, 
    name : string,
    desсription : string, 
    isPublic : boolean, 
    subThemes : [],
    notes : [],
    cards : [],
    isRoot: boolean
}

const initialStateForRootThemes: Theme = {
    isLoading: false,
    error: undefined,
    isRoot : false,
    themes: []
}

const initialStateForFullInfo : FullInfo = {
    isLoading: false,
    error: undefined,    
    id : 0, 
    name : '',
    desсription : '', 
    isPublic : false, 
    subThemes : [],
    notes : [],
    cards : [],
    isRoot: true
}

const rootThemesReduser = new ReducerBuilder<Theme>()
    .handle(fetchThemes, (state: Theme) => ({ ...state, isLoading: false}))
    .handle(getThemesFail, (state: Theme, action) => ({ ...state, error: action.payload }))
    .handle(fetchFullInfo, (state: Theme, action) => ({ ...state, isLoading: false}))
    .handle(getThemeSuccess, (_, action) => ({
        isLoading: false,
        error: "",
        themes: action.payload.themes,
        isRoot : action.payload.isRoot
    }))
    .build();

const fullInfoAboutThemeReduser = new ReducerBuilder<FullInfo>()
.handle(getFullInfoAboutThemeSuccess, (_, action) => ({
    isLoading: false,
    error: "",
    id : action.payload.id,
    name : action.payload.name,
    desсription : action.payload.desсription,
    isPublic : action.payload.isPublic,
    subThemes : action.payload.subThemes,
    notes : action.payload.notes,
    cards : action.payload.cards,
    isRoot : action.payload.isRoot
}))
.build();

export const themes = handleActions<Theme, any>(rootThemesReduser , initialStateForRootThemes);
export const fullInfoAboutTheme = handleActions<FullInfo, any>(fullInfoAboutThemeReduser , initialStateForFullInfo);