import React from 'react'
import './RidePageRideView.css'
import { IRide } from '../../App';
import { IRideView } from './RidesPage';
import { showRideDate } from '../HomePage/HomePageRideView';

const createTitle = (text: string) => {
    return (
        <div className="title-text">
            {text}    
        </div>
    )
}

const createValue = (value: any) => {
    return (
        <div className="value-text">
            {value}
        </div>
    )
}

const showRide = (props: IRidePageRideView) => {
    return (
        <div className="ride-container">
            <div className="titles-container">
                <div className="title-text">
                    From:    
                </div>
                <div className="title-text">
                    To:
                </div>
                <div className="title-text">
                    Date:
                </div>
                <div className="title-text">
                    Car:
                </div>
            </div>
            <div className="values-container">
                <div className="value-text">
                    {props.data.cityFrom}
                </div>
                <div className="value-text">
                    {props.data.cityTo}
                </div>
                <div className="value-text">
                    {showRideDate(props.data.date)}
                </div>
                <div className="value-text last-item">
                    {props.data.car}
                </div>
                {/* TODO show passengers and driver */}
            </div>           
        </div>
    )
}


export interface IRidePageRideView {
    data: IRideView
}

function RidePageRideView(props: IRidePageRideView) {

    let titles = [
        createTitle("From:"),
        createTitle("To:"),
        createTitle("Date:"),
        createTitle("Car:")
    ];

    let values = [
        createValue(props.data.cityFrom),
        createValue(props.data.cityTo),
        createValue(props.data.date),
        createValue(props.data.car),
    ]

    console.log("data");
    console.log(props.data);
    console.log("Values");
    console.log(values);

    if (props.data.driverPhone) {
        titles.push(createTitle("Driver number:"))
        values.push(createValue(props.data.driverPhone))
    }

    return (
        <>
        <div className="ride-container">
            <div className="title-container">
                {titles.map((title) => {
                    return title;
                })}
            </div>
            <div className="values-container">
                {values.map((value) => {
                    return value;
                })}
            </div>
        </div>

        </>
    )
}

export default RidePageRideView;