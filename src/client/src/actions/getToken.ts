import { createAction } from ".";


export const getToken = createAction('GET_TOKEN', (username:string, password:string) => ({
    username,
    password
}));
 
export const getTokenFail = createAction('GET_TOKEN_FAIL', (message: string) => message);

export const getTokenSuccess = createAction<GetTokenSuccessPayload, TokenResponse>('GET_TOKEN_SUCCESS', (res) => ({
    accessToken: res.accessToken,
    username: res.username
}));

export interface GetTokenSuccessPayload { accessToken: string, username: string }

interface TokenResponse {
    accessToken: string,
    username: string
}

