import React, { Component } from 'react';
import Loader from './Loader';

function makeRandomWord(length) {

    return fetch('https://kelimebul-api.herokuapp.com/api/v1/words/random?length=' + length)
        .then(result => result.json())
        .then((json) => {
            return json.word.toUpperCase();;
        })
        .catch(error => {
            console.error(error);
        });

}

export class Game extends Component {


    constructor(props) {
        super(props);
        this.state = { value: "", randomWord: "", correctWords: [], loading: true }

        this.handleChange = this.handleChange.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);
        this.onKeyDown = this.onKeyDown.bind(this);
    }
    componentWillMount() {

        makeRandomWord(8).then(response => {
            this.setState({
                randomWord: response,
                loading: false
            });
        })
    }
    handleChange(event) {
        this.setState({ value: event.target.value });
    }
    onKeyDown=(e)=> {
    if (e.keyCode === 8) {

        const form = e.target.form;
        const index = Array.prototype.indexOf.call(form, e.target);
        if(index>0)
        form.elements[index - 1].focus();
    }
}

handleKeyPress = (e) => {
    console.log(e.key)
    if (e.key === 'Enter') {
        var word = this.state.value;
        fetch('https://kelimebul-api.herokuapp.com/api/v1/words/' + word.toLowerCase())
            .then(response => response.json())
            .then(data => {
                console.log(data.exist)
                if (data.exist == true) {
                    this.setState({
                        correctWords: [...this.state.correctWords, word.toUpperCase()]
                    })
                }
            });
        this.setState({
            value: ''
        });
        console.log(this.state.correctWords)
    } else {

        const form = e.target.form;
        const index = Array.prototype.indexOf.call(form, e.target);
        if (index < this.state.randomWord.length)
        form.elements[index + 1].focus();

    }
}

render() {
    if (this.state.loading) return <Loader />;

    return (

        <div>

            <form autocomplete="off" onSubmit={e => e.preventDefault()}>
                <div>

                    <h1 class="title"> {this.state.randomWord} </h1>
                </div>
                {
                    <div>
                        {this.state.randomWord.split("").map((value, index) => {
                            return <input class="word-input" onKeyDown={this.onKeyDown} onKeyPress={this.handleKeyPress} maxLength={1} onChange={this.handleChange} />
                        })}

                    </div>
                }
            </form>


            {this.state.correctWords.length > 0 &&
                <div>
                    <h2> Bulunan Kelimeler </h2>
                    <ul>
                        {this.state.correctWords.map(item => (
                            <li >{item}</li>
                        ))}
                    </ul>
                </div>
            }
        </div>
    );
}
}
