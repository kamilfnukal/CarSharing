import { Button, TextField } from '@material-ui/core';
import axios from 'axios';
import React, { useState } from 'react'
import CreateRideButton from './CreateRideButton';
import CreateRideTextFields from './CreateRideTextFields';
import CreateRideTitles from './CreateRideTitles';
import './CreateRidePage.css'
import { ICar, ILoggedUser, IRide, IRideCreate } from '../../App';

export interface ICreateRideForm {
    handleClick: any,
    user: ILoggedUser | undefined,
    setBadInput: any
}

function CreateRideForm(props: ICreateRideForm) {

    const user = props.user!;

    const [cityFrom, setCityFrom] = useState("");
    const [cityTo, setCityTo] = useState("");
    const [carBrand, setCarBrand] = useState("");
    const [date, setDate] = useState("");
    const [numberSeats, setNumberSeats] = useState("");
    const [price, setPrice] = useState("");

    const checkInput = () => {
        console.log(user);
        try {
            parseInt(price);
            parseInt(numberSeats);
        }
        catch {
            props.setBadInput(true);
            return false;
        }

        if (cityFrom == "" || cityTo == "" || carBrand == "" || date == "" || numberSeats == "" || price == ""
            || carBrand !== user.cars[0].brand || parseInt(numberSeats) >= user.cars[0].seats) {
            // TODO validate date
            props.setBadInput(true);
            return false;
        }
        return true;
    }

    const handleClick = () => {

        if (!checkInput()) return;

        props.setBadInput(false);

        const timeApi = date.split(" ")[1];
        const dateApi = date.split(" ")[0];
        
        const url = "https://localhost:5001/create-ride";

        // Body for POST API
        const ride = {
            "DriverId": user.id, // logged user id
            "CarId": user.cars.filter((v: ICar) => v.brand === carBrand)[0].id, // logged user car with brand "carBrand"
            "CityFrom": cityFrom,
            "CityTo": cityTo,
            "CarBrand": carBrand,
            "Date": dateApi,
            "Time": timeApi,
            "AvailableSeats": parseInt(numberSeats),
            "Price": parseInt(price)
        };

        const r: IRide = {
            availableSeats: ride.AvailableSeats,
            carId: ride.CarId,
            cityFrom: ride.CityFrom,
            cityTo: ride.CityTo,
            dateTime: ride.Date + "T" + ride.Time,
            driverId: ride.DriverId,
            price: ride.Price,
            id: 10 // ?????
        }

        console.log(ride);
        
        axios.post(url, ride).then(res => {

            user.rides!.push(r);
            props.handleClick();

        }).catch(() => {
            console.log("Failed to post ride");
        });
    }

    return (
            <div className="form-container">
                <CreateRideTitles />
                <CreateRideTextFields 
                    setCityFrom={setCityFrom}
                    setCityTo={setCityTo}
                    setCarBrand={setCarBrand}
                    setDate={setDate}
                    setNumberSeats={setNumberSeats}
                    setPrice={setPrice}  
                />
                <CreateRideButton 
                    handleClick={handleClick}
                />
            </div>
    )
}

export default CreateRideForm;