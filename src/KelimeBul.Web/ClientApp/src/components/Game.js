import React, { Component } from 'react';
import Loader from './Loader';

function makeRandomWord(length) {
    return fetch('https://kelimebul-api.herokuapp.com/api/v1/words/random?length=' + length)
        .then(result => result.json())
        .then((json) => {
            return json.word.toLocaleUpperCase('tr-TR');
        })
        .catch(error => {
            console.error(error);
        });

}

export class Game extends Component {
    constructor(props) {
        super(props);
        this.state = { randomWord: "", randomWordTemp:"", correctWords: [], loading: true }
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
    onKeyDown = (e) => {
        console.log("onkeydown" + e.keyCode)
        if (e.keyCode === 8) {
            e.preventDefault();
            const form = e.target.form;
            var index = Array.prototype.indexOf.call(form, e.target);
            if (index > 0)
                form.elements[index - 1].focus();
            if (form.elements[index].value == "") {
                if (index > 0)
                    form.elements[index - 1].value = "";
                form.elements[index].value = "";
            }
            else {
                form.elements[index].value = "";
            }
            

              
        }
    }

    handleKeyPress = (e) => {
     
        if (e.key === 'Enter') {
            var word = "";
            document.querySelectorAll('input').forEach(input => {
                word = word + input.value
            });
            console.log(word)
            fetch('https://kelimebul-api.herokuapp.com/api/v1/words/' + word.toLocaleLowerCase('tr-TR'))
                .then(response => response.json())
                .then(data => {
                    console.log(data.exist)
                    if (data.exist == true) {
                        this.setState({
                            correctWords: [...this.state.correctWords, word.toLocaleUpperCase('tr-TR')]
                        })
                    }
                })
            console.log(this.state.correctWords)
        } else {
            
            const form = e.target.form;
            const index = Array.prototype.indexOf.call(form, e.target);
            if (index < this.state.randomWord.length - 1)
                form.elements[index + 1].focus();
          

        }
    }

    render() {
        if (this.state.loading) return <Loader />;

        return (
            <div>
                <form autocomplete="off" onSubmit={e => e.preventDefault()}>
                    <div>
                        <div>
                            {this.state.randomWord.split("").map((value, index) => (
                                <span class="word-input" maxLength={1} > {value} </span>
                            ))}
                        </div>    
                    </div>                  
                        <div>
                            {this.state.randomWord.split("").map((value, index) => (
                                <input class="word-input" onkeyup="this.value = this.value.toLocaleUpperCase('tr-TR');" onKeyDown={this.onKeyDown} onKeyPress={this.handleKeyPress} maxLength={1}  />
                            ))}
                        </div>                 
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
