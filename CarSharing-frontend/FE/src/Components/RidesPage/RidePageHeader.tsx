import React from 'react'
import "./RidePageHeader.css"


export interface IRidePageHeader {
    text: string
}

function RidePageHeader(props: IRidePageHeader) {

    return (
        <div className="rides-header-container">
            <div className="rides-header-text">
                {props.text}
            </div>
        </div>
    )
}

export default RidePageHeader;