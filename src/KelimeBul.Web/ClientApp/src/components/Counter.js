import React, { Component } from 'react';

export class Counter extends Component {
    static displayName = Counter.name;

    constructor(props) {
        super(props);
        this.state = { currentCount: 0 };
        this.incrementCounter = this.incrementCounter.bind(this);
    }

    incrementCounter() {
        this.setState({
            currentCount: this.state.currentCount + 1
        });
    }

    render() {
        return (
            <form>
                <div>
                    <label>
                        KELİME
                        
                </label>
                </div>
                <input type="text" name="name" />
                <input type="submit" value="Submit" />
            </form>
        );
    }
}
