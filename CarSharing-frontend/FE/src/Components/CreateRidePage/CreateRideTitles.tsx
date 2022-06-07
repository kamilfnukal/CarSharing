import { Button, TextField } from '@material-ui/core';
import React from 'react'
import './CreateRidePage.css'

function CreateRideTitles() {
    return (
        <>
        <div className="form-title__from page-text">
            From:
        </div>
        <div className="form-title__to page-text">
            To:
        </div>
        <div className="form-title__price page-text">
            Price:
        </div>
        <div className="form-title__seats page-text">
            How many seats you offer?
        </div>
        <div className="form-title__car page-text">
            Car brand: 
        </div>
        <div className="form-title__date page-text">
            Date:
        </div>
        </>
    )
}

export default CreateRideTitles;