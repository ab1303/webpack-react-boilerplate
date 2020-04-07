import ActionTyper from '../../_tools/ActionTyper';

export const { UPDATE_SETTINGS } = ActionTyper('SETTINGS_');

export function updateSettings(settings) {
  return {
    type: UPDATE_SETTINGS,
    payload: settings,
  };
}
