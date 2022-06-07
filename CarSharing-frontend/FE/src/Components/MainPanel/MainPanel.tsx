import react from 'react'
import React from 'react'
import { Link } from 'react-router-dom';
import { CREATE_RIDE_PATH, HOME_PATH, LOGIN_PATH, MY_RATINGS_PATH, MY_RIDES_PATH, PROFILE_PATH } from '../shared/const';
import './MainPanel.css'


function MainPanel() {

    function createItem(label: string, css: string, link: string) {
        const style = "main-panel-item " + css;
        return (
                <Link to={link} className={style}>
                    {label}
                </Link>
        )
    }

    return (
        <div className="main-panel-container">
            <div className="main-panel-home">
                {createItem("Home", "main-panel-homeItem", HOME_PATH)}
            </div>
            <div className="main-panel-options">   
                {createItem("Create ride", "main-panel-optionItem", CREATE_RIDE_PATH)}
                {createItem("My rides", "main-panel-optionItem", MY_RIDES_PATH)}
                {createItem("My ratings", "main-panel-optionItem", MY_RATINGS_PATH)}
                {createItem("Profile", "main-panel-optionItem", PROFILE_PATH)}
                {createItem("Log out", "main-panel-optionItem", LOGIN_PATH)}
            </div>
            
        </div>
    );

}

export default MainPanel;