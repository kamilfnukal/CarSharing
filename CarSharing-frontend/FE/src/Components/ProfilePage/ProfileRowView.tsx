import { makeStyles, TextField } from '@material-ui/core';
import React from 'react'

export interface IProfileRowView {
    isEdit: boolean,
    placeHolder: any,
    title: string,
    class: string,
    setField: any
}

const useStyles = makeStyles((theme) => ({
    root: {

    },
    textField: { 
      marginLeft: theme.spacing(5),
      marginRight: theme.spacing(5),
      width: '15ch',
    }
  }));

function title(label: string, css: string) {
    const style = "profile-title profile-title__" + css;
    return (
        <div className={style} >
            {label}
        </div>
    )
}

function value(label: string, css: string) {
    const style = "profile-text profile-text__" + css;
    return (
        <div className={style} >
            {label}
        </div>
    )
}

function ProfileRowView(props: IProfileRowView) {


    const classes = useStyles();
    
    if (props.isEdit) {

        const valueClass = "profile-text profile-text__" + props.class;
        return (
            <>
                {title(props.title, props.class)}
                <div className={valueClass}>
                    <TextField
                        label={props.placeHolder}
                        id="margin-none"
                        placeholder="User name"
                        className={classes.textField}
                        onChange={(e: any) => props.setField(e.target.value)}
                    />
                </div>
                
            </>
        )
    }

    return (
        <>
            {title(props.title, props.class)}
            {value(props.placeHolder, props.class)}
        </>

    )
}

export default ProfileRowView;