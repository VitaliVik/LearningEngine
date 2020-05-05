import { createAction } from ".";

export const fetchThemes = createAction('FETCH_THEMES');

export const fetchSubThemes = createAction('FETCH_SUBTHEMES', (id : number) => id);

export const getThemesFail = createAction('GET_THEMES_FAIL', (message: string) => message);

export const getThemeSuccess = createAction<GetThemeSuccessPayload, ThemeResponce>('GET_THEMES_SUCCESS', (res) => ({
    themes : res.themes,
    isRoot : res.isRoot
}));

export interface GetThemeSuccessPayload{themes : [], isRoot: boolean}

interface ThemeResponce{themes : [], isRoot: boolean}