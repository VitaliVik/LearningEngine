import {combineEpics} from 'redux-observable';
import getTokenEpic from './getUserTokenEpic';

import 'rxjs';
import registrationAccountEpic from './registrationAccountEpic';
import getUserThemesEpic from './getUserThemes';
import getUserSubThemesEpic from './getUserSubThemes';

export const rootEpic = combineEpics(getTokenEpic, registrationAccountEpic, getUserThemesEpic, getUserSubThemesEpic);
