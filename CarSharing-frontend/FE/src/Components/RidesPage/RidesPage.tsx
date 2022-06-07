import Axios from 'axios';
import React, { useEffect, useState } from 'react'
import { IPage, IRide } from '../../App';
import NoUserErrorPage from '../shared/NoUserError/NoUserErrorPage';
import './RidePage.css'
import RidePageHeader from './RidePageHeader';
import RidePageRideView from './RidePageRideView';
import axios from 'axios';
import * as api from '../shared/api';


export interface IPassenger {
    id: number,
    username: string,
    rating?: number
}

export interface IRideView {
    cityFrom: string,
    cityTo: string,
    date: string,
    car: string,
    carBrand?: string,
    driverPhone?: string,
    rideId: number,
    passId?: number
}


function RidesPage(props: IPage) {

    // Loading user's rides (api calls) should be done after log in. Move this logic to HomePage with useEffect()
    const [driverRides, setDriverRides] = useState<Array<IRideView>>();
    const [passengerRides, setPassengerRides] = useState<Array<IRideView>>();

    const [error, setError] = useState<boolean>(false);

    useEffect(() => {

        if (!props.user) {
            setError(true);
            return;
        }

        api.getRidesForUser(props.user!, setPassengerRides, setDriverRides);

    }, [])

    const handleDeleteDriverRide = (rideId: number) => {

        axios.delete("https://localhost:5001/delete-ride/" + rideId).then((r: any) => {
            console.log("Ride deleted successfully");
            console.log("Updating rides for user");
            api.getRidesForUser(props.user!, setPassengerRides, setDriverRides);
        })
    }

    const handleDeletePassengerRide = (rideId: number) => {

        console.log(rideId);
        console.log(props.user!.id);
        console.log(passengerRides);

        const passengerId: number = passengerRides!.filter((ride: IRideView) => { return ride.rideId == rideId })[0].passId!;
        
        // TODO test after bug fixed on BE
        axios.delete("https://localhost:5001/delete-passenger/" + rideId + "/" + passengerId).then((r: any) => {
            console.log("pass ride deleted successfully");
            console.log("Updating pass rides for user");
            api.getRidesForUser(props.user!, setPassengerRides, setDriverRides);
        }).catch(() => console.log("Failed to delete pass ride"));
    }

    if (error) return <NoUserErrorPage />

    return (
        <>
        <div className="two-panel-container">   
            <div className="driver-container">
                <RidePageHeader text="Driver rides" />
                {driverRides?.map((ride: IRideView) => {
                    return (
                        <>
                            <RidePageRideView data={ride} />
                            <div className="ride-delete-button-container">
                                <button onClick={() => handleDeleteDriverRide(ride.rideId)} className="ride-delete-button button3" >
                                    DELETE
                                </button>
                            </div>
                        </>
                    )
                })}
            </div>
            <div className="passenger-container">
                <RidePageHeader text="Passenger rides" />
                {passengerRides?.map((ride: IRideView) => {
                    return (
                        <>
                            <RidePageRideView data={ride} />
                            <div className="ride-delete-button-container">
                                <button onClick={() => handleDeletePassengerRide(ride.rideId)} className="ride-delete-button button3" >
                                    DELETE
                                </button>
                            </div>
                        </>
                    )
                })}
            </div>
        </div>
        {/* new design */}
        <div className="homepage-black-border">
            <div>
                a
            </div>
        </div>
        </>
    )

}

export default RidesPage;