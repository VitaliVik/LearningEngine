
// import { GET_TOKEN_SUCCESS } from './../actions/getToken';
import { handleActions } from 'redux-actions';
// import {
//     GET_TOKEN,
//     GET_TOKEN_SUCCESS,
//     GET_TOKEN_FAIL
// } from '../actions/getToken';

const initialState = {accesss_token: "", username: "", isLoading: undefined, error: undefined};

export const accounts: any = handleActions({
    GET_TOKEN: (state: any, action: any) => ({
        isLoading: false,
        error: null,
    }),
    GET_TOKEN_FAIL: (state: any, action: any) => ({
        
    }),
    GET_TOKEN_SUCCESS: (state: any, action: any) => ({
        
    })
}, {accounts: {isLoading:false, error: null, access_token: ""}});

// export default function accounts(state: any = initialState, action: any):{access_token: string, username: string, isLoading: boolean, error: any} {
    
//     switch (action.type) {
//         // case GET_TOKEN: 
//         //     return {
//         //         ...state,
//         //         isLoading: true,
//         //         error: null
//         //     };


//         case GET_TOKEN_SUCCESS: 
//             return {
//                 isLoading: false,
//                 error: null,
//                 access_token: action.payload.access_token,
//                 username: action.payload.username
//             };
//         case GET_TOKEN_FAIL: {
//             return {
//                 ...state,
//                 error: action.payload
//             };
//         }
//         default:
//             return state;

//     }
// }
