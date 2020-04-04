import { combineReducers } from 'redux';

import PaymentsBatch from './components/PaymentsBatch/reducer';
import Environments from './components/Environments/reducer';

export default combineReducers({
  environments: Environments,
  paymentsBatch: PaymentsBatch,
});
