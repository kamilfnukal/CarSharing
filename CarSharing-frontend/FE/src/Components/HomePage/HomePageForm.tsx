import React, { useState } from 'react'
import 'bootstrap/dist/css/bootstrap.min.css';
import HomePageButton, { IHomePageButton } from './HomePageButton';
import HomePageTextFields from './HomePageTextFields';
import axios from 'axios';

export interface IHomePageForm {
    setResponse: any,
    setBadInput: any
}

export interface IAllRides {

    price: number,
    availableSeats: number,
    driverId: number,
    carBrand: any,
    cityFrom: string,
    cityTo: string,
    carid: number,
    dateTime: string, // format 1111-01-11T00:00:00
    id: number
}

function HomePageForm(props: IHomePageForm) {

    const [from, setFrom] = useState("");
    const [to, setTo] = useState("");
    const [date, setDate] = useState("");
    const [time, setTime] = useState("");

    const [selectedDate, setSelectedDate] = React.useState<Date | null>(
        new Date(Date.now()),
    );

    const checkInput = () => {
        if (from == "" || to == "" || time == "" || from.includes(" ") || to.includes(" ") || time.includes(" ") || !time.includes(":")) {
            props.setBadInput(true);
            return false;
        }
        return true;
    }    


    const handleClick = () => {

        if (!checkInput()) return;

        props.setBadInput(false);


        const tmp = (selectedDate!.getMonth() + 1);
        const month = tmp < 10 ? "0" + tmp : tmp;

        console.log(month);

        const apiDate = selectedDate!.getDate() + ":" + month + ":" + selectedDate?.getFullYear();
        console.log(apiDate);

        const url = "https://localhost:5001/search/" + from + "/" + to + "/" + apiDate + "_" + time + "?pageNumber=1&pageSize=100";

        // NOW WORKS AS IT SHOWS ALL RIDES FROM DB
        axios.get(url).then((response: any) => {

            const responseAllRides: Array<IAllRides> = response.data;
            console.log(responseAllRides);
            props.setResponse(responseAllRides);
            return;

        }).catch(e => console.log("Failed to get rides"));
    }

    console.log(selectedDate?.getDate());

    return (
        <>
        <HomePageTextFields setFrom={setFrom} setTo={setTo} setDate={setSelectedDate} setTime={setTime} date={selectedDate!}/>
        <HomePageButton handleClick={handleClick} />
        </>
    )

}

export default HomePageForm;