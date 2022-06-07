import React from 'react'
import HomePageMainPanel from '../../MainPanel/MainPanel';
import './Header.css'

export interface IHeader {
    text: string
}

function Header(props: IHeader) {

    return (
        <div className="header-container">
            <div className="header-text">
                {props.text}
            </div>
        </div>

    );

}

export default Header;