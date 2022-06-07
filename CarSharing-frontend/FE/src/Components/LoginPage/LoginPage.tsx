import React, { useEffect, useState } from 'react'
import LoginForm, { ILoginForm } from './LoginForm'
import { BrowserRouter, Route, Link } from 'react-router-dom'
import carsharingPicture from '../images/carsharing.jpg'; 
import './LoginPage.css'
import Header from '../shared/Header/Header';
import { ENTER_INFO_PATH } from '../shared/const';
import ContinueButton from './ContinueButton';
import AfterLoginPage from './AfterLoginPage/AfterLoginPage';
import { ILoggedUser } from '../../App';
import AdminPage from '../AdminPage/AdminPage';

export interface ILoginPage {
    setUser: any,
    user: any,
    setPassword: any
}

function LoginPage(props: ILoginPage) {

    const [validName, setValidName] = useState(false);
    const [successLogin, setSuccessLogin] = useState(false);

    const [name, setName] = useState<string>();

    useEffect(() => {
        const u: ILoggedUser = {
            age: -1,
            cars: [],
            fullname: "",
            id: -1,
            username: "",
            phoneNumber: "",
        }
        props.setUser(u);
    }, [])

    // handle admin logged in
    if (name === "admin") {
        return (
            <AdminPage />
        )
    }

    if (successLogin) {
        return (
            <AfterLoginPage name={name!}/>
        )
    }

    if (validName) {
        return (
            <>
                <Header text="Perfect! You have signed up" />
                <div className="continue-container">
                    <div className="continue-item">
                        Now we will need basic information about you
                    </div>
                    <ContinueButton label="Continue" path={ENTER_INFO_PATH} />
                </div>
                <div className="black-border" />
                
            </>
        )
    }

    return(
        <>
        <div className="LoginPage-container">
            <img src={carsharingPicture} className="picture"/>
            <LoginForm setUser={props.setUser} setValidName={setValidName} setSuccessLogin={setSuccessLogin} setName={setName} setPassword={props.setPassword} />
            
        </div>
        <div className="black-border" />
        </>
    )
    
    

}

export default LoginPage;