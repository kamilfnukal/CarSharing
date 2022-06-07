import { makeStyles, TextField } from '@material-ui/core'
import React from 'react'

const useStyles = makeStyles((theme) => ({
    root: {
      '& > *': {
        margin: theme.spacing(1),
        width: '20ch',
      },
    },
}));

function LoginFormFields(props: any) {

    const classes = useStyles();

    const field = (label: string, onChange: any) => {
        return (
            <form className={classes.root} noValidate autoComplete="off" onChange={onChange}>
                <TextField id="standard-basic" label={label} />
            </form>
        )
    }

    return (
        <div className="LoginForm-header">
            {props.isSignUp ? "SIGN UP" : "LOGIN"}
            {field("Name", (e: any) => props.setName(e.target.value))}
            {field("Password", (e: any) => props.setPassword(e.target.value))}
        </div>    
    )

    

}

export default LoginFormFields;