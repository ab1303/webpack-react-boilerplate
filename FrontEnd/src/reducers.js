import { combineReducers } from 'redux';

import Settings from './components/Settings/reducer';

export default combineReducers({
  settings: Settings,
});
