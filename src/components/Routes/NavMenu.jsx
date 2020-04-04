import React from 'react';
import { NavLink, Link } from 'react-router-dom';

const NavMenu = () => (
  <div className="main-nav">
    <div className="navbar navbar-inverse">
      <div className="navbar-header">
        <button
          type="button"
          className="navbar-toggle"
          data-toggle="collapse"
          data-target=".navbar-collapse"
        >
          <span className="sr-only">Toggle navigation</span>
          <span className="icon-bar" />
          <span className="icon-bar" />
          <span className="icon-bar" />
        </button>
        <Link className="navbar-brand" to="/">
          Batch Payments Utility
        </Link>
      </div>
      <div className="clearfix" />
      <div className="navbar-collapse collapse">
        <ul className="nav navbar-nav">
          <li>
            <NavLink exact to="/" activeClassName="active">
              <span className="glyphicon glyphicon-home" /> Home
            </NavLink>
          </li>
          <li>
            <NavLink to="/batch" activeClassName="active">
              <span className="glyphicon glyphicon-th-list" /> Payments Batch
            </NavLink>
          </li>
          <li>
            <NavLink to="/environments" activeClassName="active">
              <span className="glyphicon glyphicon-education" /> Environments
            </NavLink>
          </li>
        </ul>
      </div>
    </div>
  </div>
);

export default NavMenu;
