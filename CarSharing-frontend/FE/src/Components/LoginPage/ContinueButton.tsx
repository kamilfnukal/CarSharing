import React from 'react'
import { Link } from 'react-router-dom'

export interface IContinueButton {
    label: string,
    path: string,
    handleClick?: any
}

function ContinueButton(props: IContinueButton) {

    return (
        <Link to={props.path} className="continue-link" onClick={props.handleClick}>
            {props.label}
        </Link>  
    )
}

export default ContinueButton;