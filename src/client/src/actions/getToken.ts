import { createAction } from ".";


export const getToken = createAction('GET_TOKEN');

export const getTokenFail = createAction('GET_TOKEN_FAIL', (message: string) => message);

export const getTokenSuccess = createAction<GetTokenSuccessPayload, TokenResponse>('GET_TOKEN_FAIL', (res) => ({
    accessToken: res.access_token,
    username: res.username
}));

export interface GetTokenSuccessPayload { accessToken: string, username: string }

interface TokenResponse {
    access_token: string,
    username: string
}

