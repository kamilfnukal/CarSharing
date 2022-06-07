import React, { useEffect, useState } from 'react'
import ContinueButton from '../ContinueButton'
import { HOME_PATH } from '../../shared/const'
import Header from '../../shared/Header/Header';
import { ILoggedUser } from '../../../App';
import RED_CROSS from '../../images/red-cross.jpg';
import EXCL_MARK from '../../images/exclamation-mark.png';
import CAR from '../../images/welcome-car.jpg';
import './AfterLoginPage.css';
import { IRideView } from '../../RidesPage/RidesPage';
import axios from 'axios';
import RidePageRideView from '../../RidesPage/RidePageRideView';

export interface IAfterLoginPage {
    name: string
}

function AfterLoginPage(props: IAfterLoginPage) {

    const [showRide, setShowRide] = useState<boolean>(false);
    const [ride, setRide] = useState<IRideView>();

    const foundRide = (ride: any) => {
        const r: IRideView = {
            car: ride.car.brand,
            cityFrom: ride.cityFrom,
            cityTo: ride.cityTo,
            date: ride.dateTime.split("T")[0] + " " + ride.dateTime.split("T")[1],
            driverPhone: ride.driver.phoneNumber,
            rideId: ride.id
        }

        setRide(r);
        setShowRide(true);
    }

    useEffect(() => {

        console.log("Seatching for passenger for user:");
        console.log(props.name);


        axios.get("https://localhost:5001/get-username/" + props.name).then((response: any) => {

            axios.get("https://localhost:5001/passenger/" + response.data.id + "?pageNumber=1&pageSize=100").then((res: any) => {
                console.log(res.data);
                let index = 0;
    
                for (index; index < res.data.length; index++) {
                    const ride = res.data[index];
    
                    const rideDate = new Date(ride.dateTime);
                    const now = new Date(Date.now());
                    if (rideDate.getTime() > now.getTime()) {
                        continue;
                    }
                    if (index == 0) {
                        // No upcoming rides
                        return;
                    }
                    foundRide(res.data[index - 1]); 
                    return;  
                }
                if (index !== 0) {
                    foundRide(res.data[index - 1]);
                }            
            })
        })
        

    }, [])

    const rideView = () => {
        if (showRide) {
            return (
                <div className="welcome-container">
                    <div className="notification-container">
                        <img src={EXCL_MARK} width="100" height="100" className="notification-img"/>
                        <div className="notification-item">
                            {/* Todo add logic for notification */}
                            You have a ride soon!
                        </div>
                    </div>
                    <div className="notification-ride">
                        <RidePageRideView data={ride!} />
                    </div>
                    
                </div>
            )
        }
        return (
            <>
            <div className="welcome-container">
                <div className="notification-container">
                    <img src={RED_CROSS} width="100" height="100" className="notification-img"/>
                    <div className="notification-item">
                        {/* Todo add logic for notification */}
                        You have no upcoming ride
                    </div>
                </div>
                <div className="notification-ride">
                    <img src={CAR} width="400" height="300"/>
                </div>
            </div>
            </>
        )
    }

    return (
        <>
        <Header text="Welcome back!" />
        <div className="welcome-container">
            <div className="notification-container">
                <img src={RED_CROSS} width="100" height="100" className="notification-img"/>
                <div className="notification-item">
                    {/* Todo add logic for notification */}
                    You have 0 unviewed notifications
                </div>
            </div>
            <div className="notification-button">
                <ContinueButton path={HOME_PATH} label="Continue to app" />
            </div>
            
        </div>
        
        <div className="black-border" />
        
        {rideView()}
        </>
    )

}

export default AfterLoginPage;