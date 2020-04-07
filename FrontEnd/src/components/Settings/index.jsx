import { connect } from 'react-redux';

import Settings from './Settings';
import { updateSettings } from './actions';

const mapStateToProps = ({ settings }) => {
  console.log('Settings object', settings);
  return {
    ...settings,
  };
};

const mapDispatchToProps = {
  updateSettings,
};

export default connect(mapStateToProps, mapDispatchToProps)(Settings);
