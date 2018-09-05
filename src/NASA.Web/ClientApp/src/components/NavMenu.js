import React, { Component } from "react";
import { Link } from "react-router-dom";
import { Glyphicon, Nav, Navbar, NavItem } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import "./NavMenu.css";

class NavMenu extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={"/"}>NASA Rover Photos</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={"/"} exact>
              <NavItem eventKey={1}>
                <Glyphicon glyph="home" /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/curiosity"}>
              <NavItem eventKey={2}>
                <Glyphicon glyph="camera" /> Curiosity
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/opportunity"}>
              <NavItem eventKey={3}>
                <Glyphicon glyph="camera" /> Opportunity
              </NavItem>
            </LinkContainer>
            <LinkContainer to={"/spirit"}>
              <NavItem eventKey={4}>
                <Glyphicon glyph="camera" /> Spirit
              </NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}

export default NavMenu;
