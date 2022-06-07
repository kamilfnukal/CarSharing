import { makeStyles, TextField } from '@material-ui/core';
import React, { useState } from 'react'
import { Link } from 'react-router-dom';
import { HOME_PATH } from '../../shared/const';
import axios from 'axios'
import './EnterInfoPage.css'
import ContinueButton from '../ContinueButton';
import ENTRY_PIC from '../../images/entry-page-pic.jpg'
import { ILoggedUser } from '../../../App';
import USER_PIC from '../../images/profile.jpg'
import * as api from '../../shared/api';


const useStyles = makeStyles((theme) => ({
    root: {
      display: 'flex',
      flexWrap: 'wrap',
      justifyContent: 'center'
    },
    textField: { 
      marginLeft: theme.spacing(5),
      marginRight: theme.spacing(5),
      width: '25ch',
    },
  }));

export interface IEnterInfoPageForm {
    user: ILoggedUser,
    setUser: any,
    setBadInput: any,
    password: string
}
  
function EnterInfoPageForm(props: IEnterInfoPageForm) {

    const classes = useStyles();

    const [fullName, setFullName] = useState<string>("");
    const [phone, setPhone] = useState<string>("");
    const [birthDate, setBirthDate] = useState<string>("");

    const [valid, setValid] = useState<boolean>(false);

    const checkInput = () => {

        try {
            const tmp = birthDate.split("/");
            parseInt(tmp[0]);
            parseInt(tmp[1]);
            parseInt(tmp[2]);
        }
        catch {
            props.setBadInput(true);
            console.log("NOK");
            return false;
        }

        if (fullName == "" || phone == "" || birthDate == "" ||
            !fullName.includes(" ") || fullName.split(" ").length !== 2 ||
            birthDate.split("/").length !== 3) {
                props.setBadInput(true);
                console.log("NOK");
                return false;
        }

        console.log("OK");
        return true;
    }

    const handleClick = () => {

        if (!checkInput()) return;
        
        props.setBadInput(false);
        console.log(props.password);

        const userToApi = 
        {
            "userName": props.user.username,
            "name": fullName.split(" ")[0],
            "surname": fullName.split(" ")[1],
            "phoneNumber": phone,
            "birthDate": birthDate,
            "password": props.password
        }    
        api.registerUser(userToApi);

        const newUser: ILoggedUser = {
            age: 19, // TODO from birthdate
            fullname: fullName,
            cars: [],
            id: -1, // TODO call api to get valid id
            rides: [],
            phoneNumber: phone,
            username: props.user.username
        }
        props.setUser(newUser);
        setValid(true);
    }

    return (
        <>
            {!valid && <div className="enter-form-container">
                <TextField
                    label="Fullname"
                    id="margin-none"
                    placeholder="Kamil Fňukal"
                    className={classes.textField}
                    onChange={(e: any) => setFullName(e.target.value)}
                />
                <TextField
                    label="Phone number"
                    id="margin-none"
                    placeholder="123456789"
                    className={classes.textField}
                    onChange={(e: any) => setPhone(e.target.value)}
                />
                <TextField
                    label="Birth date"
                    id="margin-none"
                    placeholder="24/1/1999"
                    className={classes.textField}
                    onChange={(e: any) => setBirthDate(e.target.value)}
                />
            </div>}
            <div className="continue-container">

                {!valid &&  <button onClick={handleClick} className="button" >
                                Validate
                            </button>}
                {valid && <ContinueButton label="Enter application" path={HOME_PATH} /> }
            </div>
        </>
    )
}

export default EnterInfoPageForm;