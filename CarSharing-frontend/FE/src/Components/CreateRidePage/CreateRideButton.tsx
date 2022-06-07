import React from 'react'

export interface ICreateRideButtonProps {
    handleClick: any
}

function CreateRideButton(props: ICreateRideButtonProps) {

    return (
        <div className="form-button-container">
            <button className="form-button" onClick={props.handleClick}>
                Submit
            </button>
        </div>
    )
}

export default CreateRideButton;