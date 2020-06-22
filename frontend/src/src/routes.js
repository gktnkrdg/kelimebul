  
import React from 'react'
import { Route, HashRouter, Switch } from 'react-router-dom'
import Game from './components/Game'
import Home from './components/Home'

export default props => (
    <HashRouter>
        <Switch>
          <Route exact path='/' component={ Home } />
          <Route exact path='/game' component={ Game } />
        </Switch>
    </HashRouter>
  )