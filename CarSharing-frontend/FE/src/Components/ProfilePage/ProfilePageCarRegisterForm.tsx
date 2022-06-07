import { makeStyles, TextField } from '@material-ui/core';
import React, { useState } from 'react' 
import ContinueButton from '../LoginPage/ContinueButton';
import CAR_PIC from '../images/car.jpg'
import { ICar } from '../../App';


export interface IProfilePageCarRegister {
    setRegisterCar: any,
    handleRegisterCarClick: any
}


const useStyles = makeStyles((theme) => ({
    root: {

    },
    textField: { 
      marginLeft: theme.spacing(5),
      marginRight: theme.spacing(5),
      width: '25ch',
    }
  }));

const row = (classes: any, placeholder: string, set: any) => {
    return (
        <TextField
            label={placeholder}
            id="margin-none"
            className={classes.textField}
            onChange={(e: any) => set(e.target.value)}
        />
    )
}

function ProfilePageCarRegister(props: IProfilePageCarRegister) {

    const classes = useStyles();


    const [brand, setBrand] = useState<string>("");
    const [seats, setSeats] = useState<string>("");  
    const [yearProduction, setYearProduction] = useState<string>();
    const [pic, setPic] = useState<string>("");

    const handleClick = () => {

        
        const car = {
            brand: brand,
            id: -1,
            yearProduction: parseInt(yearProduction!),
            seats: seats
        };
        props.handleRegisterCarClick(car, pic);
        props.setRegisterCar(false);
    }

    return (
        <>
        <div className="register-car-page">
            <div className="register-car-container">
                <div className="item-brand">
                    {row(classes, "Brand", setBrand)}
                </div>
                <div className="item-seats">
                    {row(classes, "Number of seats", setSeats)}
                </div>
                {row(classes, "Year of production", setYearProduction)}
                {row(classes, "Picture", setPic)}
                <button onClick={handleClick} className="continue-register-car-button">
                    Register
                </button>
            
            </div>
            <img src={CAR_PIC} height="400" width="600"/>
        </div>
        <div className="homepage-black-border" />        
        
        </>
    )

}

export default ProfilePageCarRegister;