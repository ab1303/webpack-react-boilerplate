import React from 'react';
import { Route } from 'react-router-dom';

import Home from '../Home';
import PaymentsBatch from '../PaymentsBatch';
import Environments from '../Environments';

import Layout from './Layout';

const Routes = () => {
  return (
    <Layout>
      <div>
        <Route exact path="/" component={Home} />
        <Route path="/environments" component={Environments} />
        <Route path="/batch" component={PaymentsBatch} />
      </div>
    </Layout>
  );
};

export default Routes;
