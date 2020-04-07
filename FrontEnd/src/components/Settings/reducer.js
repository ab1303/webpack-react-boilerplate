import { UPDATE_SETTINGS } from './actions';

function updateSettingsHandler(state, action) {
  const {
    apigeeBaseAddress,
    apigeeClientId,
    apigeeClientSecret,
  } = action.payload;
  return {
    ...state,
    apigeeBaseAddress,
    apigeeClientId,
    apigeeClientSecret,
  };
}

const actionHandlers = {
  [UPDATE_SETTINGS]: updateSettingsHandler,
};

function initialState() {
  return {
    apigeeBaseAddress: 'https://qa.api.dev.ofx.com',
    apigeeClientId: 'k3vExZM27LAo2hWJqs5SwATb8eDwkb5H',
    apigeeClientSecret: 'gYzPgBolIGBts6wF',
  };
}

export default function(state = initialState(), action) {
  const handler = actionHandlers[action.type];
  return handler ? handler(state, action) : state;
}
