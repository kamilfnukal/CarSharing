import React, { useEffect, useState } from 'react'
import Header from '../shared/Header/Header';
import CreateRideForm from './CreateRideForm';
import './CreateRidePage.css'
import OK_PIC from '../images/ok.jpg'
import { IPage } from '../../App';
import NoUserErrorPage from '../shared/NoUserError/NoUserErrorPage';
import ErrorPanel from '../shared/ErrorPanel/ErrorPanel';


function CreateRidePage(props: IPage) {

    const [isForm, setIsForm] = useState(true);
    const [error, setError] = useState<boolean>(false);
    const [noCarError, setNoCarError] = useState<boolean>(false);

    const [badInput, setBadInput] = useState<boolean>(false);

    useEffect(() => {
        if (!props.user) {
            setError(true);
        }
        else {
            console.log(props.user!);
            if (!props.user!.cars || props.user!.cars.length === 0) {
                setNoCarError(true);
            }
        }
    }, []);

    if (error) return <NoUserErrorPage />

    if (noCarError) return (
        <div className="no-car-error-container">
            <ErrorPanel text="You cannot create ride at the moment. First, you must register you car!" />
            <div className="black-border" />
        </div>
        
    )

    if (isForm) {
        return (
            <>
            <Header text="Share your ride!"/>
            <div className="page-container">
                <CreateRideForm handleClick={() => setIsForm(false)} user={props.user} setBadInput={setBadInput}/>
            </div>  
            <div className="black-border" />  
            {badInput && <ErrorPanel text="Check you input please. Car brand must be the same as shown in profile page. Date must be in format MM/DD/YYYY HH:MM" />}
            </>
        )
    }
    return (
        <>
        <Header text="Successfully created!"/>
        <div className="ok-picture-container">
            <img src={OK_PIC} width="400" height="400"/>
        </div>    
        </>
    )

}

export default CreateRidePage;