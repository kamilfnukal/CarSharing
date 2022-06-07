import './HomePage.css'
import { IAllRides } from './HomePageForm';
import { DRIVER_PATH } from '../shared/const';
import React, { useEffect } from 'react';
import { Link } from 'react-router-dom';


export interface IHomePageRideView {
    rideData?: IAllRides,
    setDriverId: any,
    handleClick: any
}

export const showRideDate = (date: string) => { return date.split("T")[0] + " " + date.split("T")[1] };

function HomePageRideView(props: IHomePageRideView) {

    return (
        <div className="content-container">
            <div className="view-container">
                <div className="from_to-container">
                    <div className="view-text">
                        {props.rideData!.cityFrom}
                    </div>
                    <div className="arrow">
                        -
                    </div>
                    <div className="view-text">
                        {props.rideData!.cityTo}
                    </div>
                </div>
                <div className="price view-text">
                    Price: {props.rideData!.price}
                </div>
                <div className="seats view-text">
                    Available: {props.rideData!.availableSeats}
                </div>
                <div className="view-text">
                    {showRideDate(props.rideData!.dateTime)}
                </div>
                <div className="view-text">
                    {/* Link to driver page // does not work */}
                    <Link target={"_blank"} to={DRIVER_PATH} >Driver</Link>
                    {/* Driver: {props.rideData!.driverId} */}
                </div>
                <button onClick={() => props.handleClick(props.rideData!.id)} className="homepage-ride-view-button button2">
                    Get!
                </button>
                
            </div>
            
        </div>
    )
}

export default HomePageRideView;