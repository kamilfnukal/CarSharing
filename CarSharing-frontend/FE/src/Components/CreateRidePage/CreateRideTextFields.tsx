import { Button, TextField } from '@material-ui/core';
import React from 'react'
import './CreateRidePage.css'


function textField(label: string, placeholder: string, style: string, setState: any) {
    return (
        <TextField
            label={label}
            placeholder={placeholder}
            className={style}
            onChange={(e: any) => setState(e.target.value)}
        />
        )
}

export interface ICreateRideTextFields {
    setCityFrom: any,
    setCityTo: any,
    setPrice: any,
    setDate: any,
    setNumberSeats: any,
    setCarBrand: any
}

function CreateRideTextFields(props: ICreateRideTextFields) {

    return (
            <>
                {textField("City", "From...", "form-field__from", props.setCityFrom)}
                {textField("City", "To...", "form-field__to", props.setCityTo)}
                {textField("CZK", "Price...", "form-field__price", props.setPrice)}
                {textField("Number of seats", "Number of seats", "form-field__seats", props.setNumberSeats)}
                {textField("Brand", "Brand", "form-field__car", props.setCarBrand)}
                {textField("Date", "24/1/2020 10:00", "form-field__date", props.setDate)}
            </>
    )

}

export default CreateRideTextFields;;