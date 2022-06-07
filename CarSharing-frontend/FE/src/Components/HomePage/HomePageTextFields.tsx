import { makeStyles, TextField } from '@material-ui/core'
import DateFnsUtils from '@date-io/date-fns';
import { MuiPickersUtilsProvider, KeyboardTimePicker, KeyboardDatePicker } from '@material-ui/pickers'
import React from 'react'

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

export interface IHomePageTextFields {
    // types: setState
    setFrom: any,
    setTo: any,
    setDate: any,
    setTime: any,
    date: Date
}

function HomePageTextFields(props: IHomePageTextFields) {

    const classes = useStyles();

    return (
        <div className={classes.root}>
            <div>
                <TextField
                    label="From"
                    id="margin-none"
                    placeholder="Join ride at ..."
                    className={classes.textField}
                    helperText="Example: Brno"
                    onChange={(e: any) => props.setFrom(e.target.value)}
                />
                <TextField
                    label="To"
                    id="margin-none"
                    placeholder="Want to travel to ..."
                    className={classes.textField}
                    helperText="Example: Praha"
                    onChange={(e: any) => props.setTo(e.target.value)}
                />
                <MuiPickersUtilsProvider utils={DateFnsUtils}>
                    <KeyboardDatePicker
                        disableToolbar
                        variant="inline"
                        format="MM/dd/yyyy"
                        margin="normal"
                        id="date-picker-inline"
                        className={classes.textField}
                        value={props.date}
                        onChange={(date) => props.setDate(date)}
                        KeyboardButtonProps={{
                            'aria-label': 'change date',
                        }}
                    />
                </MuiPickersUtilsProvider>
                <TextField
                    label="Time"
                    id="margin-none"
                    placeholder="When"
                    className={classes.textField}
                    helperText="10:00"
                    onChange={(e: any) => props.setTime(e.target.value)}
                />
            </div>
      </div>
    )
}

export default HomePageTextFields;