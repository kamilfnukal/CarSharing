import React, { useState } from 'react'
import './LoginForm.css'
import LoginFormButtons from './LoginFormButtons'
import LoginFormFields from './LoginFormFields'
import { Link, Redirect } from 'react-router-dom';
import * as api from '../shared/api'
import ErrorPanel from '../shared/ErrorPanel/ErrorPanel';

export interface ILoginForm {
    setUser: any,
    setValidName: any,
    setSuccessLogin: any,
    setName: any,
    setPassword: any
}

function LoginForm(props: ILoginForm) {

    const [name, setName] = useState("");
    const [password, setPassword] = useState("");
    const [isSignUp, setIsSignUp] = useState(true);

    const [badInput, setBadInput] = useState<boolean>(false);
    
    const handleSubmit = () => {

        if (isSignUp) {
            api.handleSignUp(name, password, props.setValidName, props.setUser, setBadInput);
            props.setPassword(password);
        }
        else {
            api.handleLogIn(name, password, props.setSuccessLogin, props.setUser)
            props.setName(name);
        }
    }
    
    return (
        <div className="login-container">
            <div className="LoginForm-container">           
                <LoginFormFields isSignUp={isSignUp} setName={setName} setPassword={setPassword} />
                <LoginFormButtons   buttonName={isSignUp ? "Already have account?" : "Do not have account?"} 
                                    submitOnClick={handleSubmit} 
                                    switchOnClick={() => setIsSignUp((v) => !v )}
                />
            </div>
            <div className="login-form-error">
                    {badInput && <ErrorPanel text=""/>}
            </div>
        </div>
    )
    
}

export default LoginForm;