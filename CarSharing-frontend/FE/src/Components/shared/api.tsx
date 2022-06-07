import { responsiveFontSizes } from '@material-ui/core';
import axios from 'axios';
import { truncateSync } from 'fs';
import { ICar, ILoggedUser, IPicture, IRide } from '../../App';
import { IAdminPage, IAdminPageCar, IAdminPagePassenger, IAdminPageRide, IAdminPageUser } from '../AdminPage/AdminPage';
import { IRideView } from '../RidesPage/RidesPage';

// LOGIN API
export function handleSignUp(name: string, password: string, setValidName: any, setUser: any, setBadInput: any) {
    const url: string = "https://localhost:5001/check-username/" + name;
    axios.get(url).then((response: any) => {
        console.log(response); // { data: boolean }

        if (!response.data) {
            // name is valid
            const newUser: ILoggedUser = {
                username: name,
                age: -1,
                fullname: "",
                cars: [],
                id: -1,
                rides: [],
                phoneNumber: ""
            }
            setUser(newUser);
            setValidName(true);
        } else {
            setBadInput(true);
        }
    }).catch(() => {
        console.log("Check username failed");
    })
}

function calculateAge(birthday: any) { // birthday is a date
    const y: string = birthday.split("-")[0];
    return 2021 - parseInt(y);
}

export function handleLogIn(name: string, password: string, setSuccessLogin: any, setUser: any) {
    const url = "https://localhost:5001/get-username/" + name;
    axios.get(url).then((response: any) => {

        axios.post("https://localhost:5001/login/", { userName: name, password: password }).then(() => {
            console.log(response);

            const cars: Array<ICar> = response.data.cars.length == 0 ? [] : [
                {
                    brand: response.data.cars[0].brand,
                    id: response.data.cars[0].id,
                    yearOfProduction: response.data.cars[0].yearOfProduction,
                    seats: response.data.cars[0].seats
                }
            ]
            console.log(calculateAge(response.data.birthDate));
    
            var u: ILoggedUser = {
                age: calculateAge(response.data.birthDate),
                id: response.data.id,
                fullname: response.data.name + " " + response.data.surname,
                phoneNumber: response.data.phoneNumber,
                username: response.data.userName,
                cars: cars,
                rides: response.data.rides
            }
    
            if (response.data.picture) {
                u = {
                    ...u,
                    picture: response.data.picture
                }
            }
    
            console.log("username from login page");
            console.log(u);
            setSuccessLogin(true);
            setUser(u);
        }).catch(() => console.log("Failed to log in"));
    })
}

export function registerUser(user: any) {
    console.log(user);
    const url = "https://localhost:5001/register";
    axios.post(url, user).catch(() => {
        console.log("Failed to sign user");
    })
}



// PROFILE CAR ADD AND EDIT

export function createCar(carForApi: any, pic: string, user: ILoggedUser, setProfileUser: any, setUser: any) {

    axios.post("https://localhost:5001/create-car", carForApi).then((res1: any) => {
        console.log("Car added...");

        axios.get("https://localhost:5001/get-user/" + user!.id).then((res: any) => {

            if (pic !== "") {

                const picForApi = {
                    url: pic,
                    carId: res.data.cars[0].id
                }

                axios.post("https://localhost:5001/create-picture", picForApi).then((res2: any) => {

                    const u: ILoggedUser = {
                        ...user!,
                         cars: [
                            {
                                ...carForApi,
                                id: res.data.cars[0].id,
                                pictures: [
                                    {
                                        url: pic,
                                        id: -1 // ID here will be always -1. No option to change car photo
                                    }
                                ]
                            }                         
                        ]
                    }
                    setProfileUser(u);
                    setUser(u);
                    return;

                }).catch(() => console.log("Failed to create picture"));
            } else {
                const u: ILoggedUser = {
                    ...user!,
                     cars: [
                        {
                            ...carForApi,
                            id: res.data.cars[0].id
                        }                         
                    ]
                }
            
                setProfileUser(u);
                setUser(u);
                
                console.log("current u:");
                console.log(u);
                return;
            }
        }).catch(() => "Failed to get user");
    }).catch(() => console.log("Failed to post car"));
}

export function deleteAndCreateCar(carForApi: any, pic: string, user: ILoggedUser, serProfileUser: any, setUser: any) {

    axios.delete("https://localhost:5001/delete-car/" + user!.cars[0].id).then((r: any) => {
        console.log("prev car deleted");
        createCar(carForApi, pic, user, serProfileUser, setUser);
    })
}


// RIDES PAGE GET DATA
export function getRidesForUser(user: ILoggedUser, setPassengerRides: any, setDriverRides: any) {

    axios.get("https://localhost:5001/passenger/" + user!.id + "?pageNumber=1&pageSize=100").then((res: any) => {
            console.log("PASSENGER");
            console.log(res.data);

            const pass = res.data;
            const pRides: Array<IRideView> = pass.map((ride: any) => {
                return {
                    cityFrom: ride.cityFrom,
                    cityTo: ride.cityTo,
                    date: ride.dateTime.split("T")[0] + " " + ride.dateTime.split("T")[1],
                    car: ride.car.brand, 
                    driverPhone: ride.driver.phoneNumber,
                    rideId: ride.id,
                    passId: ride.passengers.filter((passenger: any) => { return passenger.userId == user!.id })[0].id
                }
            })
            console.log(pRides);
            setPassengerRides(pRides);

            axios.get("https://localhost:5001/driver/" + user!.id + "?pageNumber=1&pageSize=100").then((res: any) => {
                console.log("DRIVER");
                console.log(res.data);

                const drive = res.data;
                const dRides: Array<IRideView> = drive.map((ride: IRide) => {
                    return {
                        cityFrom: ride.cityFrom,
                        cityTo: ride.cityTo,
                        date: ride.dateTime.split("T")[0] + " " + ride.dateTime.split("T")[1],
                        car: user!.cars[0].brand,
                        rideId: ride.id
                    }
                })

                setDriverRides(dRides);
                

            }).catch(() => console.log("Failed to load driver rides"));
        }).catch(() => console.log("Failed to log passenger rides"));
}

export function getAdminPageData(setUsers: any, setPassengers: any, setRides: any, setCars: any, setLoaded: any) {

    const userUrl: string = "https://localhost:5001/users";
    const passengerUrl: string = "https://localhost:5001/passengers";
    const rideUrl: string = "https://localhost:5001/rides";
    const carUrl: string = "https://localhost:5001/cars";

    axios.get(userUrl).then((userResponse: any) => {
        axios.get(passengerUrl).then((passengerResponse: any) => {
            axios.get(rideUrl).then((rideResponse: any) => {
                axios.get(carUrl).then((carResponse: any) => {
                    
                    const users: Array<IAdminPageUser> = userResponse.data.map((user: any) => {
                        return {
                            name: user.userName,
                            id: user.id
                        }
                    })

                    const passengers: Array<IAdminPagePassenger> = passengerResponse.data.map((passenger: any) => {
                        return {
                            id: passenger.id,
                            rideId: passenger.rideId
                        }
                    })

                    const rides: Array<IAdminPageRide> = rideResponse.data.map((ride: any) => {
                        return {
                            id: ride.id,
                            from: ride.cityFrom,
                            to: ride.cityTo,
                        }
                    })

                    const cars: Array<IAdminPageCar> = carResponse.data.map((car: any) => {
                        return {
                            id: car.id
                        }
                    })

                    setUsers(users);
                    setPassengers(passengers);
                    setRides(rides);
                    setCars(cars);
                    
                    setLoaded(true);
                })
            })
        })
    })
}

export function deleteUser(id: number) {
    const url: string = "https://localhost:5001/delete-user/" + id;

    axios.delete(url).then(() => {
        console.log("Successfulyly deleted");
    }).catch(() => {
        console.log("Faield to delete user");
    })
}

export function deleteRide(id: number) {
    const url: string = "https://localhost:5001/delete-ride/" + id;

    axios.delete(url).then(() => {
        console.log("Successfulyly deleted");
    }).catch(() => {
        console.log("Faield to delete ride");
    })
}

export function deleteCar(id: number) {
    const url: string = "https://localhost:5001/delete-car/" + id;

    axios.delete(url).then(() => {
        console.log("Successfulyly deleted");
    }).catch(() => {
        console.log("Faield to delete car");
    })
}

export function deletePassenger(id: number) {
    const url: string = "https://localhost:5001/delete-passenger/" + "1/" + id;

    axios.delete(url).then(() => {
        console.log("Successfulyly deleted");
    }).catch(() => {
        console.log("Faield to delete passenger");
    })
}