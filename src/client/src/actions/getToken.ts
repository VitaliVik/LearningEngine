import { createAction } from "redux-actions";

export const GET_TOKEN = 'GET_TOKEN';
export const GET_TOKEN_SUCCESS = 'GET_TOKEN_SUCCESS';
export const GET_TOKEN_FAIL = 'GET_TOKEN_FAIL';

export const getToken = createAction(GET_TOKEN);

export const getTokenFail = createAction(GET_TOKEN_SUCCESS, (message: string) => ({
    type: GET_TOKEN_FAIL,
    payload: message
}));

export const getTokenSuccess = createAction(GET_TOKEN_FAIL, (res: TokenResponse) => ({
    type: GET_TOKEN_SUCCESS,
    payload: { access_token: res.access_token, username: res.username }
}));

interface TokenResponse {
    access_token: string,
    username: string
}