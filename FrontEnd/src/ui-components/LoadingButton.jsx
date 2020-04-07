import React from 'react';
import PropTypes from 'prop-types';

const LoadingButton = ({ buttonStyle, onClick }) => (
  <button
    className={`btn ${buttonStyle} animated rotate margin-none zero-line-height`}
    onClick={onClick}
    type="button"
  >
    {' '}
    <span className="spinner" />
  </button>
);

LoadingButton.propTypes = {
  buttonStyle: PropTypes.string,
  onClick: PropTypes.func,
};

LoadingButton.defaultProps = {
  buttonStyle: 'btn-primary',
  onClick: () => {},
};

export default LoadingButton;
