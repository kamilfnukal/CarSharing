import React, { useEffect, useState } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import HomePageForm, { IAllRides } from './HomePageForm';
import Header from '../shared/Header/Header';
import './HomePage.css'
import HomePageResponse from './HomePageResponse';
import { ILoggedUser, IPage } from '../../App';
import NoUserErrorPage from '../shared/NoUserError/NoUserErrorPage';
import axios from 'axios'
import ErrorPanel from '../shared/ErrorPanel/ErrorPanel';

export interface IDriver {
    username: string,
    age: number,
    overalRating: number,
    userPhoto?: string
}

export interface IHomePage {
    user: ILoggedUser,
    setUser: any,
    setDriverId: any
}

function HomePage(props: IHomePage) {

    const[response, setResponse] = useState<Array<IAllRides>>([]);

    const [error, setError] = useState<boolean>(false);
    const [badInput, setBadInput] = useState<boolean>(false);

    useEffect(() => {
        if (!props.user) {
            setError(true);
            return;
        }
        
        if (props.user!.id == -1) {
            axios.get("https://localhost:5001/get-username/" + props.user!.username).then((res: any) => {
                const u: ILoggedUser = {
                    ...props.user!,
                    id: res.data.id
                } 
                props.setUser(u);
            })
        }

    }, []);
    
    let headerText = "Search for rides!"

    if (error) return <NoUserErrorPage />

    console.log("RESPONSE SET");
    console.log(response);

    if (response.length) {
        console.log(response);
        headerText = "Rides from " + response[0].cityFrom + " to " + response[0].cityTo;
        return (
            <>
                <Header text={headerText} />        
                <HomePageResponse res={response} setResponse={setResponse} setDriverId={props.setDriverId} user={props.user!} setUser={props.setUser}/>
            </>
        )
    }
    return (    
        <>
            <Header text={headerText}/>
            <HomePageForm setResponse={setResponse} setBadInput={setBadInput}/>
            
            {/* new design */}
            <div className="homepage-black-border" />
            {badInput && <ErrorPanel text="Check your input please..."/>}
            
        </>
    )

}


export default HomePage;