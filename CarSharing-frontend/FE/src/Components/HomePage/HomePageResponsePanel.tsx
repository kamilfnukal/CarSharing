import { makeStyles, TextField } from '@material-ui/core';
import React from 'react';
import './HomePageResponsePanel.css'


const useStyles = makeStyles((theme) => ({
    root: {
      display: 'flex',
      flexWrap: 'wrap',
      justifyContent: 'center'
    },
    textField: { 
      marginLeft: theme.spacing(5),
      marginRight: theme.spacing(5),
      width: '25ch',
    },
  }));

export interface IHomePageResponsePanel {
    setMaximumPrice: any,
    setResponse: any,
}

function HomePageResponsePanel(props: IHomePageResponsePanel) {

    const classes = useStyles();

    console.log("panel triggered");

    return (
        <>
        <div className="homepage-response-panel-container">
            <div className="homepage-response-panel-backbutton">
                <button onClick={() => props.setResponse([])} className="homepage-response-panel-button button2" >
                    Back
                </button>
            </div>
            <div className="homepage-response-panel-price-container">
                <div className="homepage-response-panel-pricefield">
                    <TextField
                        label="Maximum price"
                        id="margin-none"
                        placeholder="100"
                        className={classes.textField}
                        onChange={(e: any) => props.setMaximumPrice(e.target.value)}
                    />
                </div>
            </div>
            
        </div>
        </>
    )

}

export default HomePageResponsePanel;