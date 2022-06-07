import React from 'react'
import { Link } from 'react-router-dom'
import './NoUserErrorPage.css'

export interface NoUserErrorPage {

}

function NoUserErrorPage() {

    return (
        <>
        <div className="no-user-page">
            <div className="no-user-text">
                ERROR
            </div>
            <Link to="/login" >
                Go to login page
            </Link>
        </div>

        
        </>
    )

}

export default NoUserErrorPage;