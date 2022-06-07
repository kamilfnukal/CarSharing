import React from 'react';
import './LoginForm.css'

export interface ILoginFormButtons {
    submitOnClick: any,
    switchOnClick: any
    buttonName: string
}

function LoginFormButtons(props: ILoginFormButtons) {

    return (
        <div className="buttons-container">
            <button className="button switch-button" onClick={props.switchOnClick}>
                {props.buttonName}
            </button>
            <button className="button submit-button" onClick={props.submitOnClick}>
                Submit
            </button>
        </div>
    ) 
}

export default LoginFormButtons;