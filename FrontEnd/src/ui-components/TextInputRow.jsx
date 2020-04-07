import React from 'react';
import PropTypes from 'prop-types';

const TextInputRow = ({ id, labelText, value, onChange }) => {
  return (
    <div className="form-group row">
      <div className="col-sm-4">
        <label htmlFor={id} className="col-form-label">
          {labelText}
        </label>
      </div>

      <div className="col-sm-7">
        <input
          type="text"
          className="form-control"
          id={id}
          value={value}
          onChange={e => onChange(e.target.value)}
        />
      </div>
    </div>
  );
};

TextInputRow.propTypes = {
  id: PropTypes.string.isRequired,
  labelText: PropTypes.string.isRequired,
  value: PropTypes.string.isRequired,
  onChange: PropTypes.func,
};

TextInputRow.defaultProps = {
  onChange: () => {},
};

export default TextInputRow;
