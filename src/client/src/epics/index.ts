import {combineEpics} from 'redux-observable';
import getTokenEpic from './getUserTokenEpic';

import 'rxjs';
import registrationAccountEpic from './registrationAccountEpic';
import getUserThemesEpic from './getUserThemes';
import getThemeFullInfo from './getThemeFullInfo';

export const rootEpic = combineEpics(getTokenEpic, registrationAccountEpic, getUserThemesEpic, getThemeFullInfo);
