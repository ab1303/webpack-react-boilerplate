import '../css/site.css';
import 'bootstrap/dist/css/bootstrap.css';

import React from 'react';
import ReactDOM from 'react-dom';
import { createStore, applyMiddleware, compose } from 'redux';
import { Provider } from 'react-redux';
import thunk from 'redux-thunk';
import Moment from 'moment';
// import { BrowserRouter as Router } from 'react-router-dom';

// import Routes from './components/Routes';
import App from './App.jsx';
import reducers from './reducers';

// Set Locale
Moment.locale(window.navigator.userLanguage || window.navigator.language);

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose; // eslint-disable-line

const middleware =
  process.env.NODE_ENV !== 'production'
    ? [require('redux-immutable-state-invariant').default(), thunk] // eslint-disable-line
    : [thunk];

console.log(`Running in Environment ${process.env.NODE_ENV}`); // eslint-disable-line

const enhancer = composeEnhancers(applyMiddleware(...middleware));

function renderApp() {
  const initialState = {};
  const spaStore = createStore(reducers, initialState, enhancer);
  ReactDOM.render(
    <Provider store={spaStore}>
      <App />
    </Provider>,
    document.getElementById('app'),
  );
}

renderApp();
