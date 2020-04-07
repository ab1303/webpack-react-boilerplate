import React from 'react';
import { NavLink, Link } from 'react-router-dom';

const NavMenu = () => (
  <div className="main-nav">
    <div className="navbar navbar-dark d-block">
      <Link className="navbar-brand" to="/">
        Batch Payments Utility
      </Link>
      <div className="clearfix" />
      <div className="navbar-collapse mt-2">
        <ul className="navbar-nav">
          <li className="nav-item active">
            <NavLink exact to="/" activeClassName="active">
              <span className="oi oi-home" /> Home
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  </div>
);

export default NavMenu;
