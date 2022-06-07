import React, { useEffect, useState } from 'react';
import { Card } from 'react-bootstrap';
import * as api from '../shared/api';
import AdminPageDataView from './AdminPageDataView';


export interface IAdminPage {

}

export interface IAdminPageUser {
    name: string,
    id: number
}

export interface IAdminPageRide {
    id: number,
    from: string,
    to: string
}

export interface IAdminPageCar {
    id: number
}

export interface IAdminPagePassenger {
    id: number,
    rideId: number
}

function AdminPage(props: IAdminPage) {

    const [users, setUsers] = useState<Array<IAdminPageUser>>([]);
    const [rides, setRides] = useState<Array<IAdminPageRide>>([]);
    const [cars, setCars] = useState<Array<IAdminPageUser>>([]);
    const [passengers, setPassengers] = useState<Array<IAdminPagePassenger>>([]);

    const [loaded, setLoaded] = useState<boolean>(false);
    const [selected, setSelected] = useState<number>(0);

    const [deleteId, setDeleteId] = useState<string>("");

    useEffect(() => {
        api.getAdminPageData(setUsers, setPassengers, setRides, setCars, setLoaded);
    }, [])

    console.log(deleteId);

    const handleClick = (n: number) => {
        setSelected(n);
    }

    const handleDeleteButton = () => {
        const id: number = parseInt(deleteId);
        if (selected == 0) {
            api.deleteUser(id);
        }
        if (selected == 1) {
            api.deleteRide(id);
        }
        if (selected == 2) {
            api.deleteCar(id);
        }
        if (selected == 3) {
            api.deletePassenger(id);
        }
    }

    if (!loaded)
        return <div> Loading... </div>
    
        
    return (
        <div>
            <button onClick={() => handleClick(0)} >
                Users
            </button>
            <button onClick={() => handleClick(1)} >
                Rides
            </button>
            <button onClick={() => handleClick(2)} >
                Cars
            </button>
            <button onClick={() => handleClick(3)} >
                Passengers
            </button>
            <input onChange={(e: any) => setDeleteId(e.target.value)} >
                
            </input>
            <button onClick={() => handleDeleteButton()} >
                Delete
            </button>

            {selected === 0 && users.map((user: IAdminPageUser) => {
                return <AdminPageDataView id={user.id} data={user.name}/>
            })}

            {selected === 1 && rides.map((ride: IAdminPageRide) => {
                return <AdminPageDataView id={ride.id} data={"From: " + ride.from + " To: " + ride.to}/>
            })}

            {selected === 2 && cars.map((car: IAdminPageCar) => {
                return <AdminPageDataView id={car.id} data={""}/>
            })}
            
            {selected === 3 && passengers.map((passenger: IAdminPagePassenger) => {
                return <AdminPageDataView id={passenger.id} data={"rideId: " + passenger.rideId}/>
            })}
        </div>
    )
        

}

export default AdminPage;