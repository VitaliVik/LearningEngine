import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import axios from 'axios';
import {fetchSubThemes, getThemesFail, getThemeSuccess} from '../actions/fetchTheme';
import { store } from '..';

const methodUrl = process.env.REACT_APP_SERVER_URL + "api/theme/";

export default function getUserSubThemesEpic(action$: any) {
    return action$
        .ofType(fetchSubThemes.type)
        .switchMap(async (action:any) => {
            const Authorization = "Bearer "+ store.getState().accounts.accessToken;
            const url = methodUrl + action.payload + "/subthemes";
            return await axios.get(url, { headers: { Authorization } });
        })
        .map((res:any) => { 
            return getThemeSuccess({themes : res.data.themes, isRoot: res.data.isRoot} )
        })
        .catch((error:any) => Observable.of(getThemesFail(error)));
}