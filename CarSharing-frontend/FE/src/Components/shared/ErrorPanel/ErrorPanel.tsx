import React from 'react';
import RED_CROSS from '../../images/red-cross.jpg'
import './ErrorPanel.css'

export interface IErrorPanel {
    text: string
}

function ErrorPanel(props: IErrorPanel) {

    return (
        <>
            <div className="error-panel-container">
                <img src={RED_CROSS} width="30" height="30" className="error-panel-image" />
                <div className="error-panel-text">
                    {props.text}
                </div>
            </div>
        </>

    )

}

export default ErrorPanel;