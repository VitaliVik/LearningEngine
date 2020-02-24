import {
    GET_TOKEN,
    GET_TOKEN_SUCCESS,
    GET_TOKEN_FAIL
} from '../actions/getToken';

const initialState = {token: "", username: "", isLoading: false, error: ""};

export default function accounts(state: any = initialState, action: any):{token: string, username: string, isLoading: boolean, error: any} {
    
    switch (action.type) {
        case GET_TOKEN: 
            return {
                ...state,
                isLoading: true,
                error: null
            };


        case GET_TOKEN_SUCCESS: 
            return {
                isLoading: false,
                error: null,
                token: action.payload.token,
                username: action.payload.username
            };
        case GET_TOKEN_FAIL: {
            return {
                ...state,
                error: action.payload
            };
        }
        default:
            return state;

    }
}
