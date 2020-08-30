import React from 'react';
import Button from '@material-ui/core/Button';
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Link,
    NavLink
  } from "react-router-dom";
export default function Home(){
    return <div className="App">
      <header className="App-header">
        <Button variant="contained" color="primary"  to={{ pathname: "/game"}}  component={Link}>
          Oyuna Başla
        </Button>
      </header>
    </div>
}