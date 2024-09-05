import React from "react";
import { Button, Form, FormControl, Nav, Navbar } from "react-bootstrap";
import { Link } from "react-router-dom";

const NavbarComponent = () => {
  

  
  return (
    <Navbar bg="dark" variant="dark" expand="lg">
      <div className="container">
        <Navbar.Brand as={Link} to="/">
          <img src="logo192.png" width="30" height="30" alt="logo" /> IPL
          App
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav " />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">
              Home
            </Nav.Link>
            <Nav.Link as={Link} to="/add-player">
              Add Player
            </Nav.Link>
            <Nav.Link as={Link} to="/fan-engagement">
              Fan Engagement
            </Nav.Link>
            <Nav.Link as={Link} to="/top-players">
              Top Players
            </Nav.Link>
            <Nav.Link as={Link} to="/match-details">
              Match Details
            </Nav.Link>
          </Nav>
          
        </Navbar.Collapse>
      </div>
    </Navbar>
  );
};

export default NavbarComponent;
