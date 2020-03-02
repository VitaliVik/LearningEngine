import { handleAction } from 'redux-actions';

export const themes = handleAction("FETCH_THEME", 
    (state, action) => ({
        ...state,
        some: 1
}), {});

// export default async function themes(state:any, action:any) {
//     if (action.type === "FETCH_THEMES")
//     {
       
//         return {
//             ...state,
//             isLoading: true
//         }
//     }
//     return state;
// }