import React, { Component } from 'react';
import { Nav, NavItem, NavLink, Button } from 'reactstrap';
export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);

        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }
    render() {
        return (
            <div>
                <h1>KELİME BUL</h1>
                <Button onClick={() => this.props.history.replace('/Game') } color="success">Oyuna Başla</Button>
               
            </div>
        );
    }
}
