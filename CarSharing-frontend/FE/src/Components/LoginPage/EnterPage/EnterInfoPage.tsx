import React, { useState } from 'react'
import Header from '../../shared/Header/Header';
import EnterInfoPageForm, { IEnterInfoPageForm } from './EnterInfoPageForm';
import ENTRY_PIC from '../../images/entry-page-pic.jpg';
import { ILoggedUser } from '../../../App';
import ErrorPanel from '../../shared/ErrorPanel/ErrorPanel';

export interface IEnterInfoPage {
    user: ILoggedUser,
    setUser: any,
    password: string
}

function EnterInfoPage(props: IEnterInfoPage) {

    const [badInput, setBadInput] = useState<boolean>(false);

    return (
        <>
            <Header text="Enter first information about you" />
            <EnterInfoPageForm user={props.user} setUser={props.setUser} setBadInput={setBadInput} password={props.password} />
            <div className="black-border" />
            {badInput && <ErrorPanel text="Check you input please. Fullname must be in format NAME SURNAME, birt date in format MM/DD/YYYY" />}
        </>
    )
}

export default EnterInfoPage;