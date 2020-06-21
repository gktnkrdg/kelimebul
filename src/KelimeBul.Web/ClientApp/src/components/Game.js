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
        this.state = { randomWord: "", randomWordTemp: "", correctWords: [], loading: true, seconds: 120, inputExist: false, topMessage: "" }
        this.handleKeyPress = this.handleKeyPress.bind(this);
        this.onKeyDown = this.onKeyDown.bind(this);
    }
    componentDidMount() {
        this.myInterval = setInterval(() => {
            this.setState(({ seconds }) => ({
                seconds: seconds - 1
            }))
        }, 1000)
    }

    componentWillUnmount() {
        clearInterval(this.myInterval)
    }

    componentWillMount() {
        this.state.topMessage = "Harflerden Kelime Üretin";
        makeRandomWord(8).then(response => {
            setTimeout(function () {
                this.setState({
                    randomWordTemp: response,
                    randomWord: response,
                    loading: false
                });
            }.bind(this), 1000)

        })
    }

    onKeyDown = (e) => {
        if (e.key === 'Backspace') {
            e.preventDefault()
            const form = e.target.form;
            var index = Array.prototype.indexOf.call(form, e.target);
            var result = "";
            if (index > 0) {
                form.elements[index - 1].focus();
                if (form.elements[index].value == "") {
                    result = this.state.randomWordTemp.replace("_", form.elements[index - 1].value)
                    form.elements[index - 1].value = "";
                }
                else {
                    result = this.state.randomWordTemp.replace("_", form.elements[index].value)
                }
            } else {
                if (form.elements[index].value != "") {
                    result = this.state.randomWordTemp.replace("_", form.elements[index].value)
                }
            }
            if (result != "")
                this.setState({
                    randomWordTemp: result
                })
            this.setState({
                topMessage:"*"
            })
            form.elements[index].value = "";
        }
    }

    handleKeyPress = (e) => {
        e.preventDefault();
        var inputKey = e.key.toLocaleUpperCase('tr-TR');
        if (inputKey === 'ENTER') {

            //getAllInput
            var word = "";
            document.querySelectorAll('input').forEach(input => {
                word = word + input.value
            });
            if (this.state.correctWords.includes(word)) {
                this.setState({
                    topMessage: "Bu kelimeyi daha önce buldunuz",
                    inputExist: false

                })
            }
            else {
                fetch('https://kelimebul-api.herokuapp.com/api/v1/words/' + word.toLocaleLowerCase('tr-TR'))
                    .then(response => response.json())
                    .then(data => {
                        if (data.exist == true) {
                            //document.querySelectorAll('input').forEach(input => {
                            //    input.value= ""
                            //});
                            this.setState({
                                correctWords: [...this.state.correctWords, word.toLocaleUpperCase('tr-TR')]
                            })
                        } else {
                            this.setState({
                                topMessage: "Girdiğiniz kelime geçerli değil",
                                inputExist: false

                            })
                        }
                    })
            }
            //checkwordexist

        }
        else {
            if (!this.state.inputExist) {
                this.setState({
                    inputExist: true,
                    topMessage: "-"
                })
            }
            if (this.state.randomWordTemp.indexOf(inputKey) > -1) {
                const result = this.state.randomWordTemp.replace(inputKey, "_")
                this.setState({
                    randomWordTemp: result
                })
                e.target.value = inputKey
                const form = e.target.form;
                const index = Array.prototype.indexOf.call(form, e.target);
                if (index < this.state.randomWord.length - 1)
                    form.elements[index + 1].focus();
            }
        }
    }

    render() {
        const { randomWordTemp, randomWord, correctWords, seconds, topMessage, loading } = this.state
        if (loading) return <div class="d-flex justify-content-center"><Loader /><span>Lütfen Bekleyiniz</span></div>
        return (
            <div>
                <div class="d-flex justify-content-center">
                    <h2> {topMessage} </h2>
                </div>
                <div class="d-flex justify-content-center">

                    <div class="d-flex justify-content-center">

                        <form autocomplete="off" onSubmit={e => e.preventDefault()}>
                            <div class="d-flex flex-row justify-content-center">

                                <div class="p-2">
                                    {randomWordTemp.split("").map((value, index) => (
                                        <span class="word" maxLength={1} > {value} </span>
                                    ))}

                                </div>
                                <div class="p-2">Kalan Süre : {seconds}</div>
                            </div>
                            <hr class="my-4"></hr>
                            <div>
                                {randomWord.split("").map((value, index) => (
                                    <input autoFocus={index == 0 ? true : undefined} class="word-input" onKeyDown={this.onKeyDown} onKeyPress={this.handleKeyPress} maxLength={1} />
                                ))}
                            </div>
                        </form>

                    </div>



                </div>
                <div>
                    {correctWords.length > 0 &&
                        <div>
                            <h4> Bulunan Kelimeler </h4>
                            <ul>
                                {correctWords.map(item => (
                                    <li >{item}</li>
                                ))}
                            </ul>
                        </div>
                    }
                </div>
            </div>
        );
    }
}
