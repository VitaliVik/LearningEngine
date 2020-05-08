import { Observable } from 'rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';
import axios from 'axios';
import {getThemesFail, getFullInfoAboutThemeSuccess, fetchFullInfo} from '../actions/fetchTheme';
import { store } from '..';

const methodUrl = process.env.REACT_APP_SERVER_URL + "api/theme/";

export default function getThemeFullInfo(action$: any) {
    return action$
        .ofType(fetchFullInfo.type)
        .switchMap(async (action:any) => {
            const Authorization = "Bearer "+ store.getState().accounts.accessToken;
            const url = methodUrl + action.payload + "/fullInfo";
            return await axios.get(url, { headers: { Authorization } });
        })
        .map((res:any) => { 
            return getFullInfoAboutThemeSuccess(res.data.theme, res.data.isRoot)
        })
        .catch((error:any) => Observable.of(getThemesFail(error)));
}