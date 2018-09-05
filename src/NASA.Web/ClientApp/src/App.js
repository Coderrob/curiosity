import React from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import Home from "./components/Home";
import Rover from "./components/Rover";

export default () => (
  <Layout>
    <Route exact path="/" component={Home} />
    <Route
      path="/Curiosity/:earthDate?"
      render={props => <Rover name="Curiosity" {...props} />}
    />
    <Route
      path="/Opportunity/:earthDate?"
      render={props => <Rover name="Opportunity" {...props} />}
    />
    <Route
      path="/Spirit/:earthDate?"
      render={props => <Rover name="Spirit" {...props} />}
    />
  </Layout>
);
