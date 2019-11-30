import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import SaveFiles from './components/SaveFiles';

export default () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/saveFiles/' component={SaveFiles} />
  </Layout>
);
