import React from 'react';
import { Route, Switch } from 'react-router-dom';

import Home from '../Home';
import PaymentsBatch from '../PaymentsBatch';
import Environments from '../Environments';

import Layout from './Layout';

const Routes = () => {
  return (
    <Layout>
      <div>
        <Switch>
          <Route exact path="/" component={Home} />
          <Route path="/environments" component={Environments} />
          <Route path="/batch" component={PaymentsBatch} />
        </Switch>
      </div>
    </Layout>
  );
};

export default Routes;
