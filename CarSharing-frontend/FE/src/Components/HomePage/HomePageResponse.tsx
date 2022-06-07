import axios from 'axios';
import { setupMaster } from 'cluster';
import React, { useState } from 'react'
import { ILoggedUser } from '../../App';
import LoginPage from '../LoginPage/LoginPage';
import { IAllRides } from './HomePageForm';
import HomePageResponsePanel from './HomePageResponsePanel';
import HomePageRideView from './HomePageRideView';

export interface IHomePageResponse {
    res?: Array<IAllRides>,
    setResponse: any,
    setDriverId: any,
    user: ILoggedUser,
    setUser: any
}

function HomePageResponse(props: IHomePageResponse) {

    const [maximumPrice, setMaximumPrice] = useState<string>("");

    const showResponse = () => {    

       if (maximumPrice !== "") {
            return props.res!.filter((ride: IAllRides) => {
                return ride.price < parseInt(maximumPrice!);
            }).map((ride: IAllRides) => {
                return <HomePageRideView rideData={ride} setDriverId={props.setDriverId} handleClick={handleClick}/>     
            }) 
       }

       return props.res!.map((ride: IAllRides) => {
           return <HomePageRideView rideData={ride} setDriverId={props.setDriverId} handleClick={handleClick}/>
       })
    }

    const handleClick = (rideId: number) => {
        const url = "https://localhost:5001/create-passenger";

        const body = {
            rideId: rideId,
            userId: props.user!.id
        };

        axios.post(url, body).then(() => {
            console.log("Created passenger");

            axios.get("https://localhost:5001/get-user/" + props.user!.id).then((res: any) => {
                console.log(res);
                const u = {
                    ...props.user!,
                    rides: res.data.rides
                }
                props.setUser({
                    ...props.user,
                    rides: res.data.rides
                })

                props.setResponse([]);
                return;

            }).catch(() => console.log("Failed to get user"));
        }).catch(() => {
            console.log("Failed to register ride");
        });
    }
       
    return (
        <>  
        {/* ResponsePanel */}
        <HomePageResponsePanel setMaximumPrice={setMaximumPrice} setResponse={props.setResponse} />
        {showResponse()}
        </>        
    )
}

export default HomePageResponse;