import React from 'react';
import { Route, Switch } from 'react-router-dom';

import Home from '../Home';
import Layout from './Layout';

const Routes = () => {
  return (
    <Layout>
      <div>
        <Switch>
          <Route exact path="/" component={Home} />
        </Switch>
      </div>
    </Layout>
  );
};

export default Routes;
