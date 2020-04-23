import {combineEpics} from 'redux-observable';
import getTokenEpic from './getUserTokenEpic';

import 'rxjs';
import registrationAccountEpic from './registrationAccountEpic';
import getUserTokenEpic from './getUserThemes';

export const rootEpic = combineEpics(getTokenEpic, registrationAccountEpic, getUserTokenEpic);
