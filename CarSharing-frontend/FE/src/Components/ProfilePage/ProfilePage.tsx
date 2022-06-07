import React, { useEffect, useState } from 'react'
import './ProfilePage.css'
import axios from 'axios'
import ProfileContent from './ProfileContent';
import profilePicture from '../images/profile.jpg'
import { ICar, ILoggedUser, IPage, IPicture } from '../../App';
import NoUserErrorPage from '../shared/NoUserError/NoUserErrorPage';
import * as api from '../shared/api';

export interface IProfilePage {
    user?: ILoggedUser,
    setUser: any
}

function ProfilePage(props: IProfilePage) {

    const [isForm, setIsForm] = useState<boolean>(false);

    const [profileUser, setProfileUser] = useState<ILoggedUser>(props.user!);
    const [fullname, setFullName] = useState<string>("");
    const [phoneNumber, setPhoneNumber] = useState<string>("");
    const [car, setCar] = useState<Array<ICar>>([]);
    const [pic, setPic] = useState<string>("");

    const [error, setError] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(true);

    useEffect(() => {
        if (!props.user) {
            setError(true);
            return;
        }

        axios.get("https://localhost:5001/get-username/" + props.user!.username).then((res: any) => {

            setFullName(props.user!.fullname);
            setPhoneNumber(props.user!.phoneNumber);
            setProfileUser({
                ...props.user!,
                id: res.data.id
            })
            setLoading(false);
        })    
    }, []);
    
    const handleRegisterCarClick = (car: any, pic: string) => { 

        // if (!checkInput()) return;

        const carForApi = {
            brand: car.brand,
            seats: parseInt(car.seats),
            yearOfProduction: car.yearProduction,
            userId: profileUser!.id
        };

        console.log(carForApi);

        // Handle editing car
        if (props.user!.cars[0]) {
            console.log("delete and create car");
            api.deleteAndCreateCar(carForApi, pic, profileUser, setProfileUser, props.setUser);
        } else {
            api.createCar(carForApi, pic, profileUser, setProfileUser, props.setUser);
        }
    
    }

    // TODO REFACTOR AND MOVE TO api.tsx
    const handleClick = () => {
        console.log("Edit clicked...");

        var user: ILoggedUser = profileUser!;  

        if (!isForm) {
            setIsForm(true);
            return;
        }
        
        const body = {
            id: profileUser!.id, 
            PictureUrl: "",
            PhoneNumber: "",
            FullName: fullname
        }

        axios.put("https://localhost:5001/edit-fullname/" + "fullname", body).then((res1: any) => {
            console.log("Editing fullname...");

            axios.put("https://localhost:5001/edit-phone/" + phoneNumber, body).then((res2: any) => {

                if ((!profileUser!.picture && pic !== "") || (profileUser!.picture && profileUser!.picture.url !== pic)) {
                    const b = { "userId": profileUser!.id, "url": pic };
                    console.log("Editing picture...");

                    if (profileUser!.picture) {

                        axios.delete("https://localhost:5001/delete-picture/" + profileUser!.picture!.id).then((res3: any) => {
                            console.log("There was a pic, deleted");
                            axios.post("https://localhost:5001/create-picture", b).then((res4: any) => {
                                console.log("pic created");

                                user.fullname = fullname!;
                                user.phoneNumber = phoneNumber!;

                                axios.get("https://localhost:5001/get-user/" + profileUser!.id).then((res: any) => {
                                    console.log(res);
                                    user = {
                                        ...user,
                                        id: profileUser!.id,
                                        picture: res.data.picture
                                    }
                                    console.log("Successfuly updated picture of user:");

                                    console.log("LOCAL USER SAVED:");
                                    console.log(user);
                                
                                    setProfileUser(user);
                                    props.setUser(user);
                                    setIsForm(false);
                                    return;

                                }).catch(() => "Failed to get user");
                            }).catch(() => console.log("Failed to create picture"));   
                        }).catch(() => console.log("Failed to delete picture"));
                    }
                    else {
                        axios.post("https://localhost:5001/create-picture", b).then((res3: any) => {

                            console.log("pic created");

                            user.fullname = fullname!;
                            user.phoneNumber = phoneNumber!;

                            axios.get("https://localhost:5001/get-user/" + profileUser!.id).then((res: any) => {
                                console.log(res);
                                user = {
                                    ...user,
                                    id: profileUser!.id,
                                    picture: res.data.picture
                                }
                                console.log("Successfuly updated picture of user:");

                                console.log("LOCAL USER SAVED:");
                                console.log(user);
                            
                                setProfileUser(user);
                                props.setUser(user);
                                setIsForm(false);
                                return;

                            }).catch(() => "Failed to get user");

                        }).catch(() => console.log("Failed to create picture"));
                    }
                }

                else {
                    axios.get("https://localhost:5001/get-user/" + profileUser!.id).then((res: any) => {

                        user.fullname = fullname!;
                        user.phoneNumber = phoneNumber!;

                        console.log("PIC STAY");
                        console.log(user);
                    
                        setProfileUser(user);
                        props.setUser(user);
                        setIsForm(false);
                        return;

                    }).catch(() => "Failed to get user");
                }
            }).catch(e => console.log("Failed to update phone number"));

        }).catch(e => console.log("Fauled to update full name"));
    }

    // RENDERING CONTENT OF PAGE

    if (loading) return (
        <div>
            LOADING...
        </div>
    )
    

    if (error || fullname === "") return <NoUserErrorPage />


    if (!isForm) {
        console.log("User to be send");
        console.log(profileUser);
        return (
            <>
                <ProfileContent handleClick={handleClick}
                                user={profileUser!}
                                edit={false}
                                setPhoneNumber={setPhoneNumber}
                                setCar={setCar}
                                setFullName={setFullName}
                                setPicture={setPic}
                                handleRegisterCarClick={handleRegisterCarClick}
                /> 
            </>
        )
    }

    return (
        <>

            <ProfileContent handleClick={handleClick}
                            user={profileUser!}
                            edit={true}
                            setPhoneNumber={setPhoneNumber}
                            setCar={setCar}
                            setFullName={setFullName}
                            setPicture={setPic}
                            handleRegisterCarClick={handleRegisterCarClick}
                />
                        
        </>
    )

    

}


export default ProfilePage;