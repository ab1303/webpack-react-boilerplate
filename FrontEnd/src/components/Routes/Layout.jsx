import React from 'react';
import PropTypes from 'prop-types';
import NavMenu from './NavMenu';

const Layout = props => {
  const { children } = props;
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-sm-3">
          <NavMenu />
        </div>
        <div className="col-sm-9">{children}</div>
      </div>
    </div>
  );
};

Layout.propTypes = {
  children: PropTypes.node,
};

Layout.defaultProps = {
  children: null,
};

export default Layout;
