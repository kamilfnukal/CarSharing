import { Button } from '@material-ui/core';
import React, { useState } from 'react'
import './ProfilePage.css'
import profilePicture from '../images/profile.jpg'
import ProfileRowView from './ProfileRowView';
import ProfilePageCarRegister from './ProfilePageCarRegisterForm';


function title(label: string, css: string) {
    const style = "profile-title profile-title__" + css;
    return (
        <div className={style} >
            {label}
        </div>
    )
}

function value(label: string, css: string) {
    const style = "profile-text profile-text__" + css;
    return (
        <div className={style} >
            {label}
        </div>
    )
}

export interface IProfileContent {
    handleClick: any,
    user: any,
    edit: boolean,
    setFullName: any,
    setPhoneNumber: any,
    setPicture: any,
    setCar: any,
    handleRegisterCarClick: any
}

function ProfileContent(props: IProfileContent) {

    const [registerCar, setRegisterCar] = useState<boolean>(false);
    

    const carRow = () => {

        if (!props.user.cars || props.user.cars.length == 0) {
            return (
                <Button onClick={() => setRegisterCar(true)}>
                    Register your first car!
                </Button>
            )
        }
        return (
            <>
            {value(props.user.cars[0].brand, "car")}
            <div className="profile-button__edit-car">
                <Button onClick={() => setRegisterCar(true)} >
                    EDIT CAR
                </Button>
            </div>
            </>
                
            
        )
    }

    const photoRow = () => {
        if (typeof props.user.picture !== "undefined") {
            return (
                <div className="profile-picture">
                    <img src={props.user.picture.url} width="160" height="160"/>
                </div>
            ) 
        }
        return (
            <div className="profile-picture">
                <img src={profilePicture} width="160" height="160"/>
            </div>
        )
    }

    if (registerCar) {
        return (
           <ProfilePageCarRegister setRegisterCar={setRegisterCar} handleRegisterCarClick={props.handleRegisterCarClick}/>
        )
    }

    return (
        <div className="profile-page-container">
            <div className="profile-container">
                {photoRow()}         
                <ProfileRowView class="username" isEdit={props.edit} placeHolder={props.edit? "URL" : props.user.username} setField={props.setPicture} title={props.edit ? "Picture" : "Username"} />
                <ProfileRowView class="fullname" isEdit={props.edit} placeHolder={props.user.fullname} setField={props.setFullName} title="Fullname" /> 
                <ProfileRowView class="age" isEdit={props.edit} placeHolder={props.edit? props.user.phoneNumber : props.user.age} setField={props.setPhoneNumber} title={props.edit ? "PhoneNumber" : "Age"} /> 
                {title("Car", "car")}
                {carRow()}
                
                <Button onClick={props.handleClick} className="profile-button">
                    {props.edit ? "Confirm" : "Edit"} {/* or Done */}
                </Button>    
            </div>
        </div>
    )

}

export default ProfileContent;