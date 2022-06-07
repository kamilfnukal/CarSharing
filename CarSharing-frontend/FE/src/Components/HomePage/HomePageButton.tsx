import { IconButton } from '@material-ui/core'
import SearchIcon from '@material-ui/icons/Search';
import React from 'react'

export interface IHomePageButton {
    handleClick: any
}
    
function HomePageButton(props: IHomePageButton) {

    return (
        <div className="button-container">
            <IconButton aria-label="search" onClick={props.handleClick}>
                <SearchIcon style={{ fontSize: 50 }} />
            </IconButton>
        </div>
    )

}

export default HomePageButton;