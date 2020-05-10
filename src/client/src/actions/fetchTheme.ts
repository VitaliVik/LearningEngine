import { createAction } from ".";

export const fetchThemes = createAction('FETCH_THEMES');

export const fetchFullInfo = createAction('FETCH_FULL_INFO', (id : number) => id);

export const getThemesFail = createAction('GET_THEMES_FAIL', (message: string) => message);

export const getThemeSuccess = createAction<GetThemeSuccessPayload, ThemeResponce>('GET_THEMES_SUCCESS', (res) => ({
    themes : res.themes
}));

export const getFullInfoAboutThemeSuccess = createAction<GetFullInfoAboutThemePayload, GetFullInfoAboutThemeResponce, boolean>
                                                                            ('GET_FULL_INFO_SUCCESS', (res, isRoot) => ({
    id : res.id,
    name : res.name,
    desсription : res.desсription,
    isPublic : res.isPublic,
    subThemes : res.subThemes,
    notes : res.notes,
    cards : res.cards,
    isRoot : isRoot
}));

export interface GetThemeSuccessPayload{themes : []}

interface ThemeResponce{themes : []}

export interface GetFullInfoAboutThemePayload{
    id : Number, 
    name : string,
    desсription : string, 
    isPublic : boolean, 
    subThemes : [],
    notes : [],
    cards : [],
    isRoot: boolean
}

interface GetFullInfoAboutThemeResponce{
    id : Number, 
    name : string,
    desсription : string, 
    isPublic : boolean, 
    subThemes : [],
    notes : [],
    cards : [],
    isRoot: boolean
}