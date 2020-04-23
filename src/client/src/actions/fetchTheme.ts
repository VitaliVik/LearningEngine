import { createAction } from ".";

export const fetchThemes = createAction('FETCH_THEMES');

export const getThemesFail = createAction('GET_THEMES_FAIL', (message: string) => message);

export const getThemeSuccess = createAction<GetThemeSuccessPayload, ThemeResponce>('GET_THEMES_SUCCESS', (res) => ({
    themes : res.themes
}));

export interface GetThemeSuccessPayload{themes : []}

interface ThemeResponce{themes : []}